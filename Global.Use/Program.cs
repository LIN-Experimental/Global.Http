


using Global.Http.Services;
using LIN.Types.Developer.Models;
using LIN.Types.Responses;

Client client = new Client("https://localhost:7020/Resources/all");

client.AddHeader("token", "eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJodHRwOi8vc2NoZW1hcy5taWNyb3NvZnQuY29tL3dzLzIwMDgvMDYvaWRlbnRpdHkvY2xhaW1zL3ByaW1hcnlzaWQiOiIxIiwiaHR0cDovL3NjaGVtYXMubWljcm9zb2Z0LmNvbS93cy8yMDA4LzA2L2lkZW50aXR5L2NsYWltcy91c2VyZGF0YSI6IjEiLCJleHAiOjE3MzIwODcxMDN9.TIreHCFXbcTjfSLIoXBwFLehGBb0gpWzQ4NKIiL2_XvLNVC2mNsVxc-1_w-7AcGnodNr7e2Wka2JAJiDoeKUNg");

Dictionary<string, Type> types = new()
        {
            {"regular", typeof(ProjectDataModel) },
            {"postgre.db", typeof(LIN.Types.Developer.Projects.PostgreSQLProject) },
        };

// Resultado.
var Content = await client.Get<ReadAllResponse<ProjectDataModel>, ProjectDataModel>(types, "Type");















var x2x = "";

class Persona
{
    public string Type { get; set; }
}

class Humano : Persona
{
    public new string Type { get; set; }
}

class Prueba : Persona
{
    public new string Type { get; set; }
}

class Hello
{
    public List<Persona> pp { get; set; }
}