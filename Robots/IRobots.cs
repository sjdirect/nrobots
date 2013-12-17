using System;
using System.IO;
using System.Collections.Generic;

namespace Robots
{
    public interface IRobots
    {
        Uri BaseUri { get; set; }

        /// <summary>
        /// Load the robots.txt from a web resource
        /// </summary>
        /// <param name="robotsUri">The Uri of the web resource</param>
        void Load(Uri robotsUri);

        /// <summary>
        /// Loads the robots.txt from a stream. 
        /// </summary>
        /// <param name="stream">The input stream. May not be null.</param>
        /// <param name="baseUrl"></param>
        void Load(Stream stream, string baseUrl);

        /// <summary>
        /// Loads the robots.txt from a stream. 
        /// </summary>
        /// <param name="stream">The input stream. May not be null.</param>
        /// <param name="baseUri"></param>
        void Load(Stream stream, Uri baseUri);

        /// <summary>
        /// Loads the robots.txt from the specified string. 
        /// </summary>
        /// <param name="fileContent">String containing the robots.txt to load. May not be null</param>
        /// <param name="baseUrl"></param>
        void LoadContent(string fileContent, string baseUrl);

        /// <summary>
        /// Loads the robots.txt from the specified string. 
        /// </summary>
        /// <param name="fileContent">String containing the robots.txt to load. May not be null</param>
        /// <param name="baseUri"></param>
        void LoadContent(string fileContent, Uri baseUri);

        /// <summary>
        /// Loads the robots.txt from the specified TextReader. 
        /// </summary>
        /// <param name="reader">The TextReader used to feed the robots.txt data into the document. May not be null.</param>
        /// <param name="baseUri"></param>
        void Load(TextReader reader, Uri baseUri);

        bool Allowed(string url);
        bool Allowed(string url, string userAgent);
        bool Allowed(Uri uri);
        bool Allowed(Uri uri, string userAgent);

        int GetCrawlDelay();
        int GetCrawlDelay(string userAgent);

        IList<string> GetSitemapUrls();
    }
}