


using Global.Http.Services;

Client client = new Client("http://localhost:5184/test");

Dictionary<string, Type> keys = new Dictionary<string, Type>()
{
    {"human", typeof(Humano)},
    {"prueba", typeof(Prueba)},
};


var lista = await client.Get<Hello, Persona>(keys,"type");















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