using AnimalWorld.Core;
using AnimalWorld.Core.Apis;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Data;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Web.Mappers;
using AnimalWorld.Web.Mappers.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Mappers.Users;
using AnimalWorld.Web.Models.Animals;
using AnimalWorld.Web.Models.Home;
using AnimalWorld.Web.Models.Users;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication("QGHyrFBGxnQR")
    .AddCookie("QGHyrFBGxnQR", options =>
    {
        options.LoginPath = "/Auth/Login";
    });

builder.Services.AddAnimalWorldData(builder.Configuration.GetConnectionString("DefaultDbConnection"));
builder.Services.AddAnimalWorldCore();
builder.Services.AddScoped<IMapper<CredentialsViewModel, CredentialsDto>, AuthMapper>();
builder.Services.AddScoped<IReverseMapper<UserData, UserProfileViewModel>, UserProfileMapper>();
builder.Services.AddScoped<IReverseMapper<AnimalFamilyData, AnimalFamilyViewModel>, AnimalFamilyMapper>();
builder.Services.AddScoped<IMapper<AnimalSpeciesData, AnimalSpeciesViewModel>, AnimalSpeciesMapper>();
builder.Services.AddScoped<IMapper<RandomAnimalDto, RandomAnimalViewModel>, RandomAnimalMapper>();

builder.Services.AddHttpClient<RandomAnimalApi>(x =>
{
    x.BaseAddress = new Uri("https://api.some-random-api.com");
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();