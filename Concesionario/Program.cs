using Concesionario.Abstractions;
using Concesionario.Application;
using Concesionario.DataAccess;
using Concesionario.Entities.MicrosoftIdentity;
using Concesionario.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DbDataAccess>(options =>
{
	options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"),
		options => options.MigrationsAssembly("Concesionario.WebApi"));
	options.UseLazyLoadingProxies();
});
builder.Services.AddIdentity<User, Role>(options => options.SignIn.RequireConfirmedAccount = true).
	AddDefaultTokenProviders().
	AddEntityFrameworkStores<DbDataAccess>().
	AddSignInManager<SignInManager<User>>().
	AddRoleManager<RoleManager<Role>>().
	AddUserManager<UserManager<User>>();



builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped(typeof(IApplication<>), typeof(Application<>));
builder.Services.AddScoped(typeof(IDbContext<>), typeof(DbContext<>));

var app = builder.Build();

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
