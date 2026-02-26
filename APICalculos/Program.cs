using APICalculos.Application.Interfaces;
using APICalculos.Application.Services;
using APICalculos.Infrastructure.Data;
using APICalculos.Infrastructure.Repositories;
using APICalculos.Infrastructure.UnitOfWork;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// ================= CONFIG =================
builder.Configuration.AddJsonFile("appsettings.json");
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// ================= JWT =================
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];
var jwtAudience = builder.Configuration["Jwt:Audience"];
var keyBytes = Encoding.UTF8.GetBytes(jwtKey);

// ================= SERVICES =================
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APICalculos", Version = "v1" });

    // Configuración de JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header usando el esquema Bearer. \r\n\r\nIngrese 'Bearer' [espacio] y luego su token.\r\n\r\nEjemplo: \"Bearer 12345abcdef\"",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
builder.Services.AddAutoMapper(typeof(Program));

// Repositorios y servicios
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<IClientRepository, ClientRepository>();
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IPaymentTypeRepository, PaymentTypeRepository>();
builder.Services.AddScoped<IPaymentTypeService, PaymentTypeService>();
builder.Services.AddScoped<IServiceCategoriesRepository, ServiceCategoriesRepository>();
builder.Services.AddScoped<IServiceCategoriesService, ServiceCategoriesService>();
builder.Services.AddScoped<IExpenseTypeRepository, ExpenseTypeRepository>();
builder.Services.AddScoped<IExpenseTypeService, ExpenseTypeService>();
builder.Services.AddScoped<IServiceTypeRepository, ServiceTypeRepository>();
builder.Services.AddScoped<IServiceTypeService, ServiceTypeService>();
builder.Services.AddScoped<ICustomerHistoryRepository, CustomerHistoryRepository>();
builder.Services.AddScoped<ICustomerHistoryService, CustomerHistoryService>();
builder.Services.AddScoped<IExpensesRepository, ExpensesRepository>();
builder.Services.AddScoped<IExpenseService, ExpenseService>();
builder.Services.AddScoped<ISaleDetailRepository, SaleDetailRepository>();
builder.Services.AddScoped<ISaleDetailService, SaleDetailService>();
builder.Services.AddScoped<ISaleRepository, SaleRepository>();
builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IFinancialReportRepository, FinancialReportRepository>();
builder.Services.AddScoped<IFinancialReportService, FinancialReportService>();
builder.Services.AddScoped<IStoreRepository, StoreRepository>();
builder.Services.AddScoped<IStoreService, StoreService>();

// ================= DB CONTEXT =================
builder.Services.AddDbContext<MyDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

// ================= CORS =================
builder.Services.AddCors(options =>
{
    options.AddPolicy("NuevoPolitica", app =>
    {
        app.AllowAnyOrigin()
           .AllowAnyHeader()
           .AllowAnyMethod();
    });
});

// ================= JWT AUTH =================
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyBytes),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// ================= PIPELINE =================
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("NuevoPolitica");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();
