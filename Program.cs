using System.Collections.Generic;
using System.IO;
using System.Text;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Webmasters.v3;
using Google.Apis.Webmasters.v3.Data;

using System.Collections.Generic;
var credentialsPath = "./service-key-filename.json";
var searchConsoleUrl = "sc-domain:yourdomain.com";

var stream = new FileStream(credentialsPath, FileMode.Open);

var credentials = GoogleCredential.FromStream(stream);

if (credentials.IsCreateScopedRequired)
{
    credentials = credentials.CreateScoped(new string[] { WebmastersService.Scope.Webmasters });
}

var service = new WebmastersService(new BaseClientService.Initializer()
{
    HttpClientInitializer = credentials,
    ApplicationName = "Console App"
});

DateTime startDate = DateTime.Now.AddDays(-501);
DateTime endDate = DateTime.Now;

List dimensionList = new List();
dimensionList.Add("query");
            
var request = new SearchAnalyticsQueryRequest();

request.StartDate = startDate.ToString("yyyy-MM-dd");
request.EndDate = endDate.ToString("yyyy-MM-dd");
request.Dimensions = dimensionList;

var response = service.Searchanalytics.Query(request, searchConsoleUrl).Execute();

StringBuilder sb = new StringBuilder();

sb.AppendLine("Query,Clicks,Impressions,CTR,AvgPosition");

if(response.Rows != null)
{
    foreach(var row in response.Rows)
    {
        sb.Append(row.Keys[0]);
        sb.Append(",");
        sb.Append(row.Clicks);
        sb.Append(",");
        sb.Append(row.Impressions);
        sb.Append(",");
        sb.Append(row.Ctr);
        sb.Append(",");
        sb.Append(row.Position);
        sb.AppendLine();
    }
}

File.WriteAllText("queries.csv", sb.ToString());