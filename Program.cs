using Microsoft.EntityFrameworkCore;
using TaskManagement_API.Service.IService;
using TaskManagement_API.Service;
using TaskManagement_API.Models;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


builder.Services.AddCors(options =>
{
	options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});

builder.Services.AddControllers();


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = @"Data Source = LENOVO\SQLEXPRESS; Initial Catalog = TaskManagement_DB; Persist Security Info=True; User ID = sa; Password = 123;
Encrypt = False; 
Trust Server Certificate=True";

//var connection = @"Data Source=LENOVO\SQLEXPRESS;Initial Catalog=TaskManagement_DB;uid=sa;pwd=123;";

builder.Services.AddDbContext<TaskManagementDbContext>(options => options.UseSqlServer(connection));



builder.Services.AddScoped<ILoginServices, LoginService>();
builder.Services.AddScoped<IDepartmentService, DepartmentService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<ITeamService, TeamService>();
builder.Services.AddScoped<IRightsCreaterService, RightsCreaterService>();
builder.Services.AddScoped<IUserCreationService, UserCreationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseCors("CORSPolicy");
app.UseRouting();

app.UseEndpoints(endpoints =>
{
	endpoints.MapControllers();
});


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
