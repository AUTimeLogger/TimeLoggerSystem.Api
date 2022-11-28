using AUTimeManagement.Api.Management.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.AddServices();

var app = builder.Build();

await app.UseDbSeedAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers().RequireAuthorization();

app.Run();
