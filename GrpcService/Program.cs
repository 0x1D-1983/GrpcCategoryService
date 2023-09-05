using GrpcService.Models;
using GrpcService.Services;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ICategoryService, CategoryService>();

builder.Services.AddDbContext<AppDbContext>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());


builder.WebHost.ConfigureKestrel(options =>
{
    options.ListenAnyIP(1080, o => o.Protocols = HttpProtocols.Http2);
    options.AllowAlternateSchemes = true;
});

builder.Services.AddGrpc();
//builder.Services.AddGrpcHealthChecks();
builder.Services.AddGrpcHealthChecks(o =>
    o.Services.Map("", r => r.Tags.Contains("grpc")))
    .AddCheck("my test", () => HealthCheckResult.Healthy(), new[] { "grpc", "live", "ready" });

// !! This was just a check to investigate why the certificate was not loaded from the mount
//builder.WebHost.UseKestrel(options => options.ConfigureEndpoints());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    // run DB migrations
    using (var scope = app.Services.CreateScope())
    {
        var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        db.Database.Migrate();
    }
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapGrpcService<CategoryGrpcService>();
//app.MapGrpcService<GrpcHealthCheckService>();
app.MapGrpcHealthChecksService();

app.Run();
