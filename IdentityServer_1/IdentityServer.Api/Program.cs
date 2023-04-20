using IdentityServer.Api.Configurations;

#region Services
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
#region IdentityServer
builder.Services.AddIdentityServer()
                .AddDeveloperSigningCredential()   //This is for dev only scenarios when you don’t have a certificate to use.
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients);
#endregion
#endregion

var app = builder.Build();

#region PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseIdentityServer();

app.MapControllers();
#endregion

app.Run();
