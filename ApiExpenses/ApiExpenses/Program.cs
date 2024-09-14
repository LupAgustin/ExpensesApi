using ApiExpenses.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ApiExpenses.Repositories.Interface;
using ApiExpenses.Services.Interface;
using ApiExpenses.Repositories.Implementation;
using ApiExpenses.Services.Implementation;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization"
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});
builder.Services.AddDbContext<ApiExpensesContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MyConnection")));
builder.Services.AddAuthentication("Bearer").AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:Secret").Value!))
    };
});
builder.Services.AddScoped<ILoginRepository, LoginRepositoryImpl>();
builder.Services.AddScoped<ILoginService, LoginServiceImpl>();
builder.Services.AddScoped<IUserRepository, UserRepositoryImpl>();
builder.Services.AddScoped<IUserService, UserServiceImpl>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepositoryImpl>();
builder.Services.AddScoped<ICategoryService, CategoryServiceImpl>();
builder.Services.AddScoped<ISignUpRepository, SignUpRepositoryImpl>();
builder.Services.AddScoped<ISignUpService, SignUpServiceImpl>();
builder.Services.AddScoped<IExpenseRepository, ExpenseRepositoryImpl>();
builder.Services.AddScoped<IExpenseService, ExpenseServiceImpl>();
builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors(builder =>
    builder.WithOrigins("https://localhost:7152")
           .AllowAnyMethod()
           .AllowAnyHeader());
app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
