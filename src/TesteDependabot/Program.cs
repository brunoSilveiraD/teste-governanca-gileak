using Newtonsoft.Json;

var payload = new
{
    Project = "DependabotLab",
    Status = "ok"
};

Console.WriteLine(JsonConvert.SerializeObject(payload));
