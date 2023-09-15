using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MyDemo.Core.Data.Entity;
using MyDemo.Core.Middleware;
using MyDemo.Core.Repositories;
using MyDemo.Core.Repositories.Interfaces;
using MyDemo.Core.Services;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

var filterExpr = "@Properties['SourceContext'] like 'MyDemo%'";

// builder.Host.UseSerilog((context, logConfig) => logConfig
//     .ReadFrom.Configuration(context.Configuration)
//     .MinimumLevel.Debug()
//     .Filter.ByIncludingOnly(filterExpr)
//     .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Debug)
//             .WriteTo.File(new JsonFormatter(), "./logs/debug-logs-.json",
//         rollingInterval: RollingInterval.Day)
//             )
//     .WriteTo.Logger(l => l.Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
//             .WriteTo.File(new JsonFormatter(), "./logs/error-logs-.json",
//         rollingInterval: RollingInterval.Day)
//             )
//     .WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug)
// );

builder.Host.UseSerilog((context, logConfig) => logConfig
        .ReadFrom.Configuration(context.Configuration));


// Add Authentication services & middlewares
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
        ValidAudience = builder.Configuration["JwtSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.
                GetBytes(builder.Configuration["JwtSettings:SecurityKey"]))
    };
});

builder.Services.AddScoped<JwtService>();

// Add services to the container.
builder.Services.AddRepositories();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
     // add JWT Authentication
    var securityScheme = new OpenApiSecurityScheme
    {
        Name = "JWT Authentication",
        Description = "Enter JWT Bearer token **_only_**",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer", // must be lower case
        BearerFormat = "JWT",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };
    options.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {securityScheme, new string[] { }}
    });

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "MyDemo API",
        Description = "My Demo API by Winson from CoderBlog.in",
        TermsOfService = new Uri("https://www.coderblog.in"),
        Contact = new OpenApiContact
        {
            Name = "Contact",
            Url = new Uri("https://www.coderblog.in")
        },
        License = new OpenApiLicense
        {
            Name = "Example License",
            Url = new Uri("https://example.com/license")
        }
    });
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("Mssql"));
    //options.UseSqlite(builder.Configuration.GetConnectionString("Sqlite"));
}
);

//allow cross-origin access for the api
builder.Services.AddCors(o => o.AddPolicy("AllowCrosite", policy =>
{
    policy.AllowAnyHeader()
        .AllowAnyMethod()
        .SetIsOriginAllowed(origin => true) // allow any origin
        .AllowCredentials();
}));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowCrosite");

app.UseRequestLogMiddleware();

app.UseErrorHandlingMiddlewareMiddleware();


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
