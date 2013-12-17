using System;
using System.Collections.Generic;
using System.Linq;

namespace Robots.Model
{
    public class UserAgentEntry : Entry
    {
        public UserAgentEntry()
            : base(EntryType.UserAgent)
        {}

        public string UserAgent { get; set; }

        private readonly List<Entry> _entries = new List<Entry>();

        public IEnumerable<Entry> Entries
        {
            get { return _entries; }
        }

        public IEnumerable<DisallowEntry> DisallowEntries
        {
            get {
                return from entry in _entries
                       where entry.Type == EntryType.Disallow
                       select entry as DisallowEntry; 
            }
        }

        public IEnumerable<AllowEntry> AllowEntries
        {
            get
            {
                return from entry in _entries
                       where entry.Type == EntryType.Allow 
                       select entry as AllowEntry;
            }
        }

        public CrawlDelayEntry CrawlDelayEntry 
        {
            get
            {
                return _entries.Find(e => e.Type == EntryType.CrawlDelay) as CrawlDelayEntry;
            }
        }

        public SitemapEntry SitemapEntry
        {
            get
            {
                return _entries.Find(e => e.Type == EntryType.Sitemap) as SitemapEntry;
            }
        }

        public void AddEntry(Entry entry)
        {
            if (entry == null)
                throw new ArgumentNullException("entry");

            _entries.Add(entry);
        }
    }
}