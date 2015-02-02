using System;
using System.Globalization;

namespace Robots.Model
{
    public abstract class Entry
    {
        private const string USER_AGENT_KEYWORD = "user-agent:";
        private const string DISALLOW_KEYWORD = "disallow:";
        private const string ALLOW_KEYWORD = "allow:";
        private const string CRAWL_DELAY_KEYWORD = "crawl-delay:";
        private const string SITEMAP_KEYWORD = "sitemap:";

        protected Entry(EntryType type)
        {
            Type = type;
        }

        public EntryType Type { get; private set; }

        public string Comment { get; set; }

        public bool HasComment { get { return !string.IsNullOrEmpty(Comment); } }

        public static Entry CreateEntry(EntryType type)
        {
            switch (type)
            {
                case EntryType.Allow:
                    return new AllowEntry();
                case EntryType.Comment:
                    return new CommentEntry();
                case EntryType.Disallow:
                    return new DisallowEntry();
                case EntryType.UserAgent:
                    return new UserAgentEntry();
                case EntryType.CrawlDelay:
                    return new CrawlDelayEntry();
                case EntryType.Sitemap:
                    return new SitemapEntry();
            }

            throw new InvalidOperationException();
        }

        public static bool TryParse(Uri baseUri, string entryText, out Entry entry)
        {
            if (baseUri == null)
                throw new ArgumentNullException("baseUri");


            entry = null;

            if (string.IsNullOrEmpty(entryText))
                return false;

            EntryType type = EntryType.Invalid;
            string comment = string.Empty;

            int commentPosition = entryText.IndexOf('#');
            if (commentPosition == 0)
            {
                type = EntryType.Comment;
            }
            if (commentPosition >= 0 && 
                (commentPosition == 0 || entryText[commentPosition - 1] == ' '))
            {
                comment = entryText.Substring(commentPosition + 1);
                entryText = entryText.Substring(0, commentPosition);
            }

            if (string.IsNullOrEmpty(entryText))
            {
                entry = CreateEntry(type);
                entry.Comment = comment;
                return true;
            }
            try
            {
                if (entryText.StartsWith(USER_AGENT_KEYWORD, true, CultureInfo.InvariantCulture))
                {
                    type = EntryType.UserAgent;
                    entry = CreateEntry(type);
                    entry.Comment = comment;
                    string userAgent = entryText.Substring(USER_AGENT_KEYWORD.Length).Trim().TrimEnd('?');;

                    if(userAgent == null || string.IsNullOrEmpty(userAgent.Trim()))
                    {
                        entry = null;
                        return false;                         
                    }
                    else
                    {
                         ((UserAgentEntry) entry).UserAgent = userAgent;
                    }
                }
                else if (entryText.StartsWith(DISALLOW_KEYWORD, true, CultureInfo.InvariantCulture))
                {
                    type = EntryType.Disallow;
                    bool inverted = entryText.EndsWith("$");
                    string value = entryText.Substring(DISALLOW_KEYWORD.Length).Trim().TrimEnd('?');

                    Uri url = null;
                    if (!string.IsNullOrEmpty(value) && Uri.TryCreate(baseUri, value, out url))
                    {
                        entry = CreateEntry(type);
                        entry.Comment = comment;
                        ((UrlEntry)entry).Url = url;
                        ((UrlEntry)entry).Inverted = inverted;
                    }
                }
                else if (entryText.StartsWith(ALLOW_KEYWORD, true, CultureInfo.InvariantCulture))
                {
                    type = EntryType.Allow;
                    bool inverted = entryText.EndsWith("$");
                    string value = entryText.Substring(ALLOW_KEYWORD.Length).Trim().TrimEnd('?');

                    Uri url;
                    if (Uri.TryCreate(baseUri, value, out url))
                    {
                        entry = CreateEntry(type);
                        entry.Comment = comment;
                        ((UrlEntry)entry).Url = url;
                        ((UrlEntry)entry).Inverted = inverted;
                    }
                }
                else if (entryText.StartsWith(CRAWL_DELAY_KEYWORD, true, CultureInfo.InvariantCulture))
                {
                    type = EntryType.CrawlDelay;
                    string value = entryText.Substring(CRAWL_DELAY_KEYWORD.Length).Trim().TrimEnd('?');

                    entry = CreateEntry(type);
                    entry.Comment = comment;

                    try
                    {
                        ((CrawlDelayEntry)entry).CrawlDelay = Convert.ToInt32(value);
                    }
                    catch
                    {
                        ((CrawlDelayEntry)entry).CrawlDelay = 0;
                    }
                }
                else if (entryText.StartsWith(SITEMAP_KEYWORD, true, CultureInfo.InvariantCulture))
                {
                    type = EntryType.Sitemap;
                    string value = entryText.Substring(SITEMAP_KEYWORD.Length).Trim().TrimEnd('?');

                    entry = CreateEntry(type);
                    entry.Comment = comment;

                    try
                    {
                        ((SitemapEntry)entry).SitemapUrl = value;
                    }
                    catch
                    {
                        ((SitemapEntry)entry).SitemapUrl = "";
                    }
                }
                else
                {
                    entry = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                entry = null;
                return false;
            }
            if (entry == null)
                return false;
            return true;
        }
    }
}