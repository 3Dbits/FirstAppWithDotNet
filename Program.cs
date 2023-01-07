using FirstDemoCS.Context;
using FirstDemoCS.Data;
using FirstDemoCS.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<TestContext>(options =>
            options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// To fill DB with some data on creation
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TestContext>();
    context.Database.EnsureCreated();
    // context.Database.EnsureDeleted();
    DbInitializer.Initialize(context);
}

app.MapGet("/test", async (TestContext db) =>
    await db.Tests.ToListAsync());

app.MapGet("/test/{id}", async (int id, TestContext db) =>
    await db.Tests.FindAsync(id)
        is Test test
            ? Results.Ok(test)
            : Results.NotFound());

app.MapPost("/test", async (Test test, TestContext db) =>
{
    db.Tests.Add(test);
    await db.SaveChangesAsync();

    return Results.Created($"/todoitems/{test.Id}", test);
});

app.MapPut("/test/{id}", async (int id, Test inputTest, TestContext db) =>
{
    var test = await db.Tests.FindAsync(id);

    if (test is null) return Results.NotFound();

    test.Date = DateTimeExtensions.SetKindUtc(inputTest.Date);
    test.Summary = inputTest.Summary;
    test.PinCode = inputTest.PinCode;

    await db.SaveChangesAsync();

    return Results.NoContent();
});

app.MapDelete("/test/{id}", async (int id, TestContext db) =>
{
    if (await db.Tests.FindAsync(id) is Test test)
    {
        db.Tests.Remove(test);
        await db.SaveChangesAsync();
        return Results.Ok(test);
    }

    return Results.NotFound();
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
