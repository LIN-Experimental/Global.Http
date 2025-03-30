
using Global.Http.Services;
using LIN.Types.Developer.Models;
using LIN.Types.Responses;
using Newtonsoft.Json;

string aa = """"
{
  "Model": {
    "Id": 4,
    "Name": "Mi Proyecto",
    "Creation": "2025-02-23T20:55:16.5575478",
    "Status": 1,
    "Type": "default",
    "IsProvisioned": true,
    "VisibleKeys": true,
    "VisibleRules": true,
    "Owners": [],
    "FirewallRules": [
      {
        "Id": 2,
        "Name": "Al",
        "StartIp": "0.0.0.0",
        "EndIp": "1.1.1.1",
        "Status": 1,
        "ProjectId": 4,
        "Project": null
      }
    ],
    "Keys": [],
    "Transactions": [],
    "Billing": {
        "Id": 1,
      "Name": "Cuenta de facturación de Alexander Giraldo",
      "Type": 0,
      "State": 1,
      "Balance": 400,
      "Transactions": []
    },
    "BillingId": 0
  },
  "Errors": [],
  "Response": 1,
  "Message": "",
  "Token": "",
  "AlternativeObject": null
}
"""";


Dictionary<string, Type> types = new()
        {
            {"DEFAULT", typeof(ProjectDataModel) },
            {"postgre.db", typeof(LIN.Types.Developer.Projects.PostgreSQLProject) },
            {"bucket", typeof(LIN.Types.Developer.Projects.BucketProject) },
        };


var settings = new JsonSerializerSettings
{
    Converters = [new PolimorfismConverter<ReadOneResponse<ProjectDataModel>>(types, typeof(ProjectDataModel), "Type")]
};

var result = JsonConvert.DeserializeObject<ReadOneResponse<ProjectDataModel>>(aa, settings);


Console.ReadLine();