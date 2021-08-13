using Microsoft.OpenApi.Models;
using RepositoryDapper.Models;
using RepositoryDapper.Repositories;
using RepositoryDapper.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "MinimalAPIs", Version = "v1" });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));
}

app.MapGet("/", (Func<string>)(() => "Hello World! afafaf"));

app.MapGet("/Todo", async HttpContent =>
{
    await HttpContent.Response.WriteAsync("Olá Anderson!");
});

#region Crud 


app.MapGet("/Uf", () =>
    {
        var connection = DataBaseService.GetConnectionFactory();

        var repository = new Repository<Estado>(connection);
        return Results.Ok(repository.Read());
    });

app.MapGet("/Uf/{id}", (string id) =>
{
    var connection = DataBaseService.GetConnectionFactory();

    var repository = new Repository<Estado>(connection);
    return Results.Ok(repository.Read(id));
});

app.MapPut("/Uf/{id}", (string id, Estado estado) =>
{
    if (id != estado.UF)
        return Results.BadRequest();

    var connection = DataBaseService.GetConnectionFactory();
    var repository = new Repository<Estado>(connection);

    repository.Update(estado);

    return Results.Ok();
});

app.MapPost("/Uf", (Estado estado) =>
{
    if (estado == null)
        return Results.BadRequest();

    var connection = DataBaseService.GetConnectionFactory();
    var repository = new Repository<Estado>(connection);

    repository.Create(estado);

    return Results.Ok();
});

app.MapDelete("/Uf/{id}", (string id) =>
{
    if (string.IsNullOrEmpty(id))
        return Results.BadRequest();

    var connection = DataBaseService.GetConnectionFactory();
    var repository = new Repository<Estado>(connection);

    var estadoDelete = repository.Read(id);
    repository.Delete(estadoDelete);

    return Results.Ok();
});


#endregion

await app.RunAsync();
