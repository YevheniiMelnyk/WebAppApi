using Microsoft.EntityFrameworkCore;
using WebApplication_API;
using WebApplication_API.Data;
using WebApplication_API.Repository;
using WebApplication_API.Repository.IRepository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddAutoMapper(typeof(MappingConfig));

builder.Services.AddScoped<IHotelRepository, HotelRepository>();
builder.Services.AddControllers(option => {
    //option.ReturnHttpNotAcceptable = true;
}).AddNewtonsoftJson().AddXmlDataContractSerializerFormatters(); // we can get response with xml and json formats
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


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
