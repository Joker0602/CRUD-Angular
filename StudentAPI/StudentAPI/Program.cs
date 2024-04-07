using AutoMapper;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using StudentAPI.Data;
using StudentAPI.Data.Repository.StudentRepository;
using System.Net;
using System.Reflection;
var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Services.AddDbContext<StudentDbContext>(x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentString"));
        x.UseLazyLoadingProxies();
    });
}
else
{
    builder.Services.AddDbContext<StudentDbContext>(x =>
    {
        x.UseSqlServer(builder.Configuration.GetConnectionString("DevelopmentString"));
        x.UseLazyLoadingProxies();
    });
}

builder.Services.AddCors();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// builder.Services.AddControllers();
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
);


var distPath = Path.Combine(Directory.GetCurrentDirectory());
if (!Directory.Exists(distPath))
{
    Directory.CreateDirectory(distPath);
}

builder.Services.AddScoped<IStudentRepo, StudentRepo>();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Student", Version = "v1" });
});

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Student"));
}
else
{
    app.UseExceptionHandler(builder =>
    {
        builder.Run(async context =>
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = context.Features.Get<IExceptionHandlerFeature>();
            if (error != null)
            {
                // context.Response.AddApplicationError(error.Error.Message);
                await context.Response.WriteAsync(error.Error.Message);
            }
        });
    });
    app.UseHsts();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
);
app.UseDefaultFiles();
app.UseStaticFiles();

app.MapControllers();

// Image Upload

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");

app.UseStaticFiles();


AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

var scope = app.Services.CreateScope();

var services = scope.ServiceProvider;
try
{
    var context = services.GetRequiredService<StudentDbContext>();
    context.Database.Migrate();
    // DataSeederoneit.OneItSetting(context);
}
catch (System.Exception ex)
{
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occured during migration!");
}

await app.RunAsync();


