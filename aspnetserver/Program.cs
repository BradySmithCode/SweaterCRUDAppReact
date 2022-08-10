using aspnetserver.Data;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", 
        builder =>
        {
            builder.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:3000", "https://appname.azurestaticapps.net");
        });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(swaggerGenOptions =>
{
    swaggerGenOptions.SwaggerDoc("v1", new OpenApiInfo { Title = "Sweater Storage", Version = "v1" });
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(swaggerUIOptions =>
{
    swaggerUIOptions.DocumentTitle = "Sweater Storage";
    swaggerUIOptions.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API serving a very simple Post model.");
    swaggerUIOptions.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();

app.UseCors("CORSPolicy");

app.MapGet("/get-all-sweaters", async () => await SweatersRepository.GetSweatersAsync())
    .WithTags("Sweaters Endpoints");

app.MapGet("/get-sweaters-by-id/{sweaterId}", async (int sweaterId) =>
{
    Sweater sweaterToReturn = await SweatersRepository.GetSweaterByIdAsync(sweaterId);

    if (sweaterToReturn != null)
    {
        return Results.Ok(sweaterToReturn);
    }
    else
    {
        return Results.BadRequest();
    }
})
    .WithTags("Sweaters Endpoints");

app.MapPost("/create-sweater", async (Sweater sweaterToCreate) =>
{ 
    bool createSuccessful = await SweatersRepository.CreateSweaterAsync(sweaterToCreate);

    if (createSuccessful)
    {
        return Results.Ok("Create Successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Sweaters Endpoints");

app.MapPut("/update-sweater", async (Sweater sweaterToUpdate) =>
{
    bool updateSuccessful = await SweatersRepository.UpdateSweaterAsync(sweaterToUpdate);

    if (updateSuccessful)
    {
        return Results.Ok("Update Successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Sweaters Endpoints");

app.MapDelete("/delete-sweater-by-id/{sweaterId}", async (int sweaterId) =>
{
    bool deleteSuccessful = await SweatersRepository.DeleteSweaterAsync(sweaterId);

    if (deleteSuccessful)
    {
        return Results.Ok("Delete Successful");
    }
    else
    {
        return Results.BadRequest();
    }
}).WithTags("Sweaters Endpoints");


app.Run();
