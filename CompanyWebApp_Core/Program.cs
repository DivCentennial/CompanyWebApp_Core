using CompanyWebApp_Core;
using CompanyWebApp_Core.Models;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

DataLayer dl = new DataLayer();

// Get all departments
app.MapGet("/departments", () =>
{
    return dl.GetDepts();
});

// Get single department
app.MapGet("/departments/{id}", (int id) =>
{
    var d = dl.GetDept(id);
    return d is not null ? Results.Ok(d) : Results.NotFound();
});

// Create new department
app.MapPost("/departments", (Dept dpt) =>
{
    dl.CreateDept(dpt);
    return Results.Created($"/departments/{dpt.Department_ID}", dpt);
});

// Update department
app.MapPut("/departments/{id}", (int id, Dept dpt) =>
{
    var existing = dl.GetDept(id);
    if (existing is null) return Results.NotFound();
    dpt.Department_ID = id;
    dl.UpdateDept(dpt);
    return Results.Ok(dpt);
});

// Delete department
app.MapDelete("/departments/{id}", (int id) =>
{
    var existing = dl.GetDept(id);
    if (existing is null) return Results.NotFound();
    dl.DeleteDept(id);
    return Results.NoContent();
});

app.Run();
