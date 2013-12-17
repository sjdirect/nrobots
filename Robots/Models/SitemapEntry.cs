namespace Robots.Model
{
    public class SitemapEntry : Entry
    {
        public SitemapEntry()
            : base(EntryType.Sitemap)
        {}

        public string SitemapUrl { get; set; }
    }
}
