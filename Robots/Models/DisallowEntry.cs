using System;

namespace Robots.Model
{
    public class DisallowEntry : UrlEntry
    {
        public DisallowEntry()
            : base(EntryType.Disallow)
        { }
    }
}