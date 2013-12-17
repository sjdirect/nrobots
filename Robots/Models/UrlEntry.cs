using System;

namespace Robots.Model
{
    public abstract class UrlEntry : Entry
    {
        protected UrlEntry(EntryType type)
            : base(type)
        { }

        public bool Inverted { get; set; }
        
        public Uri Url { get; set; }
    }
}