using Microsoft.Bing.Webmaster;
using Google.Apis.Webmasters.v3;
using Google.Apis.Webmasters.v3.Data;

class Program
{
    static void Main(string[] args)
    {
        // Bing Webmaster API
        var bingWebmasterClient = new BingWebmasterClient();
        bingWebmasterClient.ApiKey = "Your Bing Webmaster API Key";
        var sites = bingWebmasterClient.Sites.Get();
        Console.WriteLine("Sites in Bing Webmaster account:");
        foreach (var site in sites)
        {
            Console.WriteLine(site.Url);
        }
        var indexedPages = bingWebmasterClient.Sites.GetIndexedPages();
        Console.WriteLine("Indexed pages:");
        foreach (var indexedPage in indexedPages)
        {
            Console.WriteLine(indexedPage.Url);
        }
        var nonIndexedPages = bingWebmasterClient.Sites.GetNonIndexedPages();
        Console.WriteLine("Non-indexed pages:");
        foreach (var nonIndexedPage in nonIndexedPages)
        {
            Console.WriteLine(nonIndexedPage.Url);
        }
        var crawlErrors = bingWebmasterClient.Sites.GetCrawlErrors();
        Console.WriteLine("Crawl errors:");
        foreach (var crawlError in crawlErrors)
        {
            Console.WriteLine(crawlError.Url);
        }

        // Google Search Console API
        var webmastersService = new WebmastersService();
        webmastersService.Key = "Your Google Search Console API Key";
        var sitesListResponse = webmastersService.Sites.List().Execute();
        Console.WriteLine("Sites in Google Search Console account:");
        foreach (var site in sitesListResponse.SiteEntry)
        {
            Console.WriteLine(site.SiteUrl);
        }
        var urlcrawlerrorssamplesList = webmastersService.Urlcrawlerrorssamples.List("site-url", "platform:web").Execute();
        Console.WriteLine("Crawl errors:");
        foreach (var crawlError in urlcrawlerrorssamplesList.UrlCrawlErrorSample)
        {
            Console.WriteLine(crawlError.PageUrl);
        }

        // Submitting URL to both engines
        // Bing Webmaster
        bingWebmasterClient.Sites.SubmitUrl("site-url", "http://example.com/page1");

        // Google Search Console
        webmastersService.Urlcrawlerrorssamples.MarkAsFixed("site-url", "web", "http://example.com/page1").Execute();
    }
}