using UnitConverter.Service;
using UnitConverter.WebAPI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddSingleton<Units>();

builder.Services.AddScoped<IUnitConverterService, TimeUnitConverterService>();
builder.Services.AddScoped<IUnitConverterService, MassUnitConverterService>();
builder.Services.AddScoped<IUnitConverterService, LengthUnitConverterService>();

//builder.Services.AddSingleton<IUnitConverterService, LengthUnitConverterService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

builder.Services.AddCors();

app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

app.UseHttpsRedirection();





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
