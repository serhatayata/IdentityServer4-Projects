using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using static IdentityModel.OidcConstants;

var client = new HttpClient();
var disco = await client.GetDiscoveryDocumentAsync("https://localhost:5001");

var tokenResponse = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
{
    Address = disco.TokenEndpoint,

    ClientId = "clientFirst",
    ClientSecret = "clientfirstsecret",
    Scope = "catalog_writepermission"
});

client.SetBearerToken(tokenResponse.AccessToken);

var response = await client.GetAsync("https://localhost:5002/api/catalog");
if (!response.IsSuccessStatusCode)
{
    Console.WriteLine(response.StatusCode);
}
else
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(JArray.Parse(content));
}

Console.WriteLine(tokenResponse.Json);
Console.ReadKey();