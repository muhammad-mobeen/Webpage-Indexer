using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.SearchConsole.v1;
using Google.Apis.SearchConsole.v1.Data;
using Google.Apis.Services;
using Google.Apis.Requests;

namespace Search_Console_API
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var credentials = OAuth2Authenticator.Authenticate("google_oauth_credentials.json", new [] {SearchConsoleService.Scope.Webmasters});
            
            using (var service = new SearchConsoleService(new BaseClientService.Initializer() { HttpClientInitializer = credentials }))
            {
                var websiteList = service.Sites.List().Execute();
                foreach (var site in websiteList.SiteEntry)
                {
                    Console.WriteLine(site.SiteUrl + " | Fetching keywords:");
                    var keywords = service.Searchanalytics.Query(new SearchAnalyticsQueryRequest()
                    {
                        StartDate = "2021-01-01",
                        EndDate = "2021-10-01",
                        Dimensions = new List<string>() {"QUERY"}
                    }, site.SiteUrl).Execute();

                    if (keywords.Rows != null)
                        foreach (var keyword in keywords.Rows) 
                            foreach (string k in keyword.Keys)
                                Console.WriteLine(k + " | Impressions: " + keyword.Impressions + " | Clicks: " + keyword.Clicks + " | Ctr: " + keyword.Ctr + " | Position: " + keyword.Position);
                    Console.WriteLine();
                }
            }

            Console.ReadLine();
        }
    }
}
