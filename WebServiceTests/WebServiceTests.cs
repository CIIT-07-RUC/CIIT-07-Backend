using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Newtonsoft.Json.Linq;

namespace WebServiceTests;

public class UnitTest1
{

    private const string UsersApi = "http://localhost:5275/api/users";

    // User tests

    [Fact] 
    public async Task ApiUsers_GetValidUser_OkResponseAndUserInstance()
    {
        var (user, statusCode) = await GetObject($"{UsersApi}/64");

        string userJsonString = user.ToString(); 
        JToken token = JObject.Parse(userJsonString);

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal("test@test.com", token.SelectToken("email"));
    }

    [Fact]
    public async Task ApiUsers_GetInValidUser_NotFoundAndNullAsInstance()
    {
        var (user, statusCode) = await GetObject($"{UsersApi}/999");

        string userJsonString = user.ToString();
        JToken token = JObject.Parse(userJsonString);

        Assert.Equal(HttpStatusCode.NotFound, statusCode);
        Assert.Null(token.SelectToken("email"));
    }



    [Fact]
    public async Task ApiUsers_LoginValidUser_OkResponseAndJsonWebToken()
    {
        var loginInstance = new
        {
            Email = "test@test.com",
            Password = "test123"
        };
        var (user, statusCode) = await PostData(UsersApi + "/login", loginInstance);
        string userJsonString = user.ToString();
        JToken token = JObject.Parse(userJsonString);

        Assert.Equal(HttpStatusCode.OK, statusCode);
        Assert.Equal(64, token.SelectToken("id"));

    }



    // Helper methods
    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }

    async Task<(JsonObject?, HttpStatusCode)> PostData(string url, object content)
    {
        var client = new HttpClient();
        var requestContent = new StringContent(
            JsonSerializer.Serialize(content),
            Encoding.UTF8,
            "application/json");
        var response = await client.PostAsync(url, requestContent);
        Console.WriteLine("RESPONSE"); 
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }
}
