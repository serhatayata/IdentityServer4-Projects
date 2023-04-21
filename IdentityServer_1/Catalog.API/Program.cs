#region SERVICES
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
    {
        options.Authority = "https://localhost:5001"; // IdentityServer
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("CatalogRead", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("catalog_readpermission");
    });

    options.AddPolicy("CatalogWrite", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("catalog_writepermission");
    });

    options.AddPolicy("CatalogFull", policy =>
    {
        policy.RequireAuthenticatedUser();
        policy.RequireClaim("catalog_writepermission", "catalog_readpermission");
    });
});
#endregion

var app = builder.Build();

#region PIPELINE
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
#endregion

app.Run();
