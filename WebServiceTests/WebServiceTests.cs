using System.Net;
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




    // Helper methods
    async Task<(JsonObject?, HttpStatusCode)> GetObject(string url)
    {
        var client = new HttpClient();
        var response = client.GetAsync(url).Result;
        var data = await response.Content.ReadAsStringAsync();
        return (JsonSerializer.Deserialize<JsonObject>(data), response.StatusCode);
    }
}
