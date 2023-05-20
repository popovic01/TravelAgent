using Microsoft.EntityFrameworkCore;
using Stripe;
using TravelAgent.AppDbContext;
using TravelAgent.Helpers;
using TravelAgent.Services.Implementations;
using TravelAgent.Services.Interfaces;
using TravelAgent.Utility;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddCors(options =>
{
    options.AddPolicy("EnableCORS", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")
));
//mapping values from appsettings.json to properties in StripeSettings class
//we are configuring StripeSettings class
builder.Services.Configure<StripeSettings>(builder.Configuration.GetSection("StripeSettings"));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IOfferService, OfferService>();
builder.Services.AddScoped<IOfferRequestService, OfferRequestService>();
builder.Services.AddScoped<IReservationService, ReservationService>();
builder.Services.AddScoped<ILocationService, LocationService>();
builder.Services.AddScoped<ITagService, TagService>();
builder.Services.AddScoped<ITransportationTypeService, TransportationTypeService>();
builder.Services.AddScoped<IOfferTypeService, OfferTypeService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthHelper, AuthHelper>();
builder.Services.AddScoped<ICommonHelper, CommonHelper>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("EnableCORS");

app.UseRouting();

//stripe config of api key at global level
//using : we get key name
StripeConfiguration.ApiKey = builder.Configuration.GetSection("StripeSettings:SecretKey").Get<string>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
