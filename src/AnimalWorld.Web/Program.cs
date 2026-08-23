using AnimalWorld.Core;
using AnimalWorld.Data;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Web.Mappers;
using AnimalWorld.Web.Mappers.Animals;
using AnimalWorld.Web.Models.Animals;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

builder.Services.AddAnimalWorldData(builder.Configuration.GetConnectionString("DefaultDbConnection"));
builder.Services.AddAnimalWorldCore();
builder.Services.AddScoped<IMapper<AnimalFamilyData, AnimalFamilyViewModel>, AnimalFamilyMapper>();
builder.Services.AddScoped<IMapper<AnimalSpeciesData, AnimalSpeciesViewModel>, AnimalSpeciesMapper>();

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