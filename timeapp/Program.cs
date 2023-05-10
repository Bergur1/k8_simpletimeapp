using System.Net;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddHealthChecks();
var app = builder.Build();
app.MapHealthChecks("/healthz");
app.MapGet("/", () =>
{
    //var hostname = Dns.GetHostName();
    var timeFromSystem = DateTime.Now;
    //var jsonString = JsonSerializer.Serialize(hostname);
    var jsonString = JsonSerializer.Serialize(timeFromSystem);
    return jsonString;
});

app.Run();
