namespace Robots.Model
{
    public class CrawlDelayEntry : Entry
    {
        public CrawlDelayEntry()
            : base(EntryType.CrawlDelay)
        {}

        public int CrawlDelay { get; set; }
    }
}
