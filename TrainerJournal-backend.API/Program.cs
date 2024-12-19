using TrainerJournal_backend.API;
using TrainerJournal_backend.API.Extensions;
using TrainerJournal_backend.Application;
using TrainerJournal_backend.Application.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.Converters.Add(new TimeOnlyJsonConverter());
    });;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGenWithJwtSecurity();

var configuration = builder.Configuration;
var postgresDbConnection = configuration.GetConnectionString("PostgresDbConnection")!;
var jwtOptions = configuration.GetSection("JwtOptions").Get<JwtOptions>();

builder.Services.AddCustomCors();
builder.Services.AddDb(postgresDbConnection);
builder.Services.AddJwtAuth(jwtOptions);

builder.Services.AddApplicationLayer();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("FrontendPolicy");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();