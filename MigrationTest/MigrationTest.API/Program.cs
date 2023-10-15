using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SD.LLBLGen.Pro.LinqSupportClasses;
using SD.LLBLGen.Pro.ORMSupportClasses;
using SD.LLBLGen.Pro.DQE;
using SD.LLBLGen.Pro.DQE.PostgreSql;
using Npgsql;
using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MigrationTest.API.Models;
using MigrationTest.API;
using System.Threading.Tasks;
using MigrationTest.DatabaseSpecific;
using MigrationTest.Linq;
using MigrationTest.EntityClasses;

var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("Postgres");
RuntimeConfiguration.AddConnectionString("Postgres", builder.Configuration.GetConnectionString("Postgres"));
RuntimeConfiguration.ConfigureDQE<PostgreSqlDQEConfiguration>(c =>
{
    c.AddDbProviderFactory(typeof(NpgsqlFactory));
});
// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapPost("/add-student", async ([FromBody] Student student) =>
{
    using (var adapter = new DataAccessAdapter(connectionString))
    {
        try
        {
            StudentEntity studentEntity = new()
            {
                Name = student.Name,
                Email = student.Email,
                Id = student.Id
            };
            await adapter.SaveEntityAsync(studentEntity, true);
            return Results.Ok();
        }
        catch (Exception ex)
        {
            return Results.BadRequest(ex.Message);
        }
    }
});
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
