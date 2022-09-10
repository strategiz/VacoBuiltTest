using System.Reflection;
using WebApiTest.BAL.Implementations;
using WebApiTest.BAL.Interfaces;
using WebApiTest.DAL.Implementations;
using WebApiTest.DAL.Interfaces;
using WebApiTest.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
    //options.Filters.Add(typeof(ApiKeyActionFilter));
});

builder.Services.AddLogging(options =>
{
    options.AddConfiguration(builder.Configuration.GetSection("Logging"))
        .AddConsole()
        .AddDebug();
});


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    // using System.Reflection; to generate swagger documention from comments
    var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
});

//Dependency injection
builder.Services.AddScoped<IUserServices, UserServices>();
builder.Services.AddScoped<IPostServices, PostServices>();

builder.Services.AddScoped<IUserDA, UserSimpleDAL>();
builder.Services.AddScoped<IPostDA, PostSimpleDAL>();

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
