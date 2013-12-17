using System;
using System.IO;
using System.Collections.Generic;
using Robots.Model;

namespace Robots
{
    public class Robots : IRobots
    {
        #region Private Fields

        private readonly List<Entry> _entries = new List<Entry>();

        private Uri _baseUri;

        #endregion

        #region Accessors

        public Uri BaseUri
        {
            get { return _baseUri; }
            set
            {
                if (value == _baseUri)
                    return;

                _baseUri = value;
            }
        }
        #endregion

        #region Load methods

        /// <summary>
        /// Load the robots.txt from a web resource
        /// </summary>
        /// <param name="robotsUri">The Uri of the web resource</param>
        public void Load(Uri robotsUri)
        {
            var uriBuilder = new UriBuilder(robotsUri) { Path = "/robots.txt" };
            var req = (System.Net.HttpWebRequest)System.Net.WebRequest.Create(uriBuilder.Uri);
            
            using (var webresponse = req.GetResponse())
            using (var responseStream = webresponse.GetResponseStream())
            {
                string baseUrl = webresponse.ResponseUri.GetLeftPart(UriPartial.Authority);
                Load(responseStream, baseUrl);
            }
        }

        /// <summary>
        /// Loads the robots.txt from a stream. 
        /// </summary>
        /// <param name="stream">The input stream. May not be null.</param>
        /// <param name="baseUrl"></param>
        public void Load(Stream stream, string baseUrl)
        {
            Uri baseUri;
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out baseUri))
                throw new ArgumentException("Invalid Url", "baseUrl");
            
            Load(new StreamReader(stream), baseUri);
        }

        /// <summary>
        /// Loads the robots.txt from a stream. 
        /// </summary>
        /// <param name="stream">The input stream. May not be null.</param>
        /// <param name="baseUri"></param>
        public void Load(Stream stream, Uri baseUri)
        {
            Load(new StreamReader(stream), baseUri);
        }

        /// <summary>
        /// Loads the robots.txt from the specified string. 
        /// </summary>
        /// <param name="fileContent">String containing the robots.txt to load. May not be null</param>
        /// <param name="baseUrl"></param>
        public void LoadContent(string fileContent, string baseUrl)
        {
            Uri baseUri;
            if (!Uri.TryCreate(baseUrl, UriKind.Absolute, out baseUri))
                throw new ArgumentException("Invalid Url", "baseUrl");

            LoadContent(fileContent, new Uri(baseUrl));
        }

        /// <summary>
        /// Loads the robots.txt from the specified string. 
        /// </summary>
        /// <param name="fileContent">String containing the robots.txt to load. May not be null</param>
        /// <param name="baseUri"></param>
        public void LoadContent(string fileContent, Uri baseUri)
        {
            if (fileContent == null)
                throw new ArgumentNullException("fileContent");
            var sr = new StringReader(fileContent);
            try
            {
                Load(sr, baseUri);
            }
            finally
            {
                sr.Close();
            }
        }

        /// <summary>
        /// Loads the robots.txt from the specified TextReader. 
        /// </summary>
        /// <param name="reader">The TextReader used to feed the robots.txt data into the document. May not be null.</param>
        /// <param name="baseUri"></param>
        public void Load(TextReader reader, Uri baseUri)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");

            _baseUri = baseUri;

            UserAgentEntry lastAgent = null;
            do
            {
                string line = reader.ReadLine();
                if (line == null)
                    break;

                if (string.IsNullOrEmpty(line))
                    continue;

                Entry entry;
                if (!Entry.TryParse(_baseUri, line, out entry))
                    continue;

                if (entry.Type == EntryType.UserAgent)
                {
                    lastAgent = (UserAgentEntry) entry;
                    _entries.Add(entry);
                }
                else if (entry.Type == EntryType.Comment)
                {
                    _entries.Add(entry);
                }
                else if (lastAgent != null)
                {
                    lastAgent.AddEntry(entry);
                }
                else
                {
                    continue;
                }
            } while (true);

        }

        #endregion

        #region Methods: Allow

        public bool Allowed(string url)
        {
            return Allowed(new Uri(url, UriKind.RelativeOrAbsolute), UserAgents.AllAgents);
        }

        public bool Allowed(string url, string userAgent)
        {
            return Allowed(new Uri(url, UriKind.RelativeOrAbsolute), userAgent);
        }

        public bool Allowed(Uri uri)
        {
            return Allowed(uri, UserAgents.AllAgents);
        }

        public bool Allowed(Uri uri, string userAgent)
        {
            if (_entries.Count == 0)
                return true;

            if (!uri.IsAbsoluteUri)
                uri = new Uri(_baseUri, uri);

            if (uri.Scheme != _baseUri.Scheme || uri.Authority != _baseUri.Authority)
                return false;

            if (uri.LocalPath == "/robots.txt")
                return false;

            var userAgentEntry = FindUserAgentEntry(userAgent);
            if (userAgentEntry == null)
                return true;


            string[] uriParts = uri.LocalPath.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var disallowEntry in userAgentEntry.DisallowEntries)
            {
                bool result;
                if (CheckDisallowedEntry(disallowEntry, uriParts, out result))
                {
                    if (CheckExplicitlyAllowed(userAgentEntry, uri))
                        return true;

                    return result;
                }
            }

            return true;
        }

        #endregion

        #region Private methods

        private UserAgentEntry FindUserAgentEntry(string userAgent)
        {
            UserAgentEntry allAgentsEntry = null;
            foreach (var entry in _entries)
            {
                var userAgentEntry = entry as UserAgentEntry;
                if (userAgentEntry == null || userAgentEntry.Type != EntryType.UserAgent)
                    continue;

                if (string.Compare(userAgentEntry.UserAgent, userAgent, true) == 0)
                    return userAgentEntry;
                if (string.Compare(userAgentEntry.UserAgent, UserAgents.AllAgents, true) == 0)
                    allAgentsEntry = userAgentEntry;
            }

            return allAgentsEntry;
        }

        private bool CheckExplicitlyAllowed(UserAgentEntry userAgentEntry, Uri uri)
        {
            if (uri.Scheme != _baseUri.Scheme || uri.Authority != _baseUri.Authority)
                return false;

            string[] uriParts = uri.LocalPath.Split(new[] {'/'}, StringSplitOptions.RemoveEmptyEntries);
            foreach (var allowEntry in userAgentEntry.AllowEntries)
            {
                bool result;
                if (CheckAllowedEntry(allowEntry, uriParts, out result))
                {
                    return result;
                }
            }

            return false;
        }

        private static bool CheckAllowedEntry(AllowEntry entry, string[] uriParts, out bool allow)
        {
            allow = true;
            string[] robotInstructionUriParts = entry.Url.LocalPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (robotInstructionUriParts.Length > uriParts.Length)
                return false;

            bool mismatch = false;
            for (int i = 0; i < Math.Min(robotInstructionUriParts.Length, uriParts.Length); i++)
            {
                if (string.Compare(uriParts[i], robotInstructionUriParts[i], true) != 0)
                {
                    mismatch = true;
                    break;
                }
            }
            if (mismatch)
            {
                return false;
            }

            return true;
        }

        private static bool CheckDisallowedEntry(DisallowEntry entry, string[] uriParts, out bool allow)
        {
            allow = true;
            string[] robotInstructionUriParts = entry.Url.LocalPath.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

            if (robotInstructionUriParts.Length > uriParts.Length)
                return false;

            int end = Math.Min(robotInstructionUriParts.Length, uriParts.Length);
            bool mismatch = false;
            for (int i = 0; i < end; i++)
            {
                int index1 = i, index2 = i;
                if (entry.Inverted)
                {
                    index1 = robotInstructionUriParts.Length - i - 1;
                    index2 = uriParts.Length - i - 1;
                }
                if (IsMismatch(robotInstructionUriParts[index1], uriParts[index2]))
                {
                    mismatch = true;
                    break;
                }
            }
            if (!mismatch)
            {
                allow = false;
                return true;
            }

            return false;
        }

        private static bool IsMismatch(string regsiteredPart, string testedPart)
        {
            if (string.Compare("*", regsiteredPart, true) == 0)
                return false;
            if (string.Compare(testedPart, regsiteredPart, true) == 0)
                return false;
            if (testedPart.StartsWith(regsiteredPart, StringComparison.InvariantCultureIgnoreCase))
                return false;

            return true;
        }

        #endregion

        internal void AddEntry(Entry entry)
        {
            if (entry ==  null)
                throw new ArgumentNullException("entry");
            if (entry.Type != EntryType.Comment && entry.Type != EntryType.UserAgent)
                throw new ArgumentException("Only Comments and User-Agent entries can be added", "entry");

            _entries.Add(entry);
        }
    }
}