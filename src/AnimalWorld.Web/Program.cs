using AnimalWorld.Core;
using AnimalWorld.Core.Apis;
using AnimalWorld.Core.Dtos.Animals;
using AnimalWorld.Core.Dtos.Users;
using AnimalWorld.Core.Settings;
using AnimalWorld.Data;
using AnimalWorld.Data.Models.Animals;
using AnimalWorld.Data.Models.Users;
using AnimalWorld.Data.Models.Zoos;
using AnimalWorld.Web.Helpers;
using AnimalWorld.Web.Hubs;
using AnimalWorld.Web.Jobs;
using AnimalWorld.Web.Mappers.Animals;
using AnimalWorld.Web.Mappers.Interfaces;
using AnimalWorld.Web.Mappers.Interfaces.CustomMappers;
using AnimalWorld.Web.Mappers.Users;
using AnimalWorld.Web.Mappers.Zoos;
using AnimalWorld.Web.Models.Animals;
using AnimalWorld.Web.Models.Home;
using AnimalWorld.Web.Models.Users;
using AnimalWorld.Web.Models.Zoos;
using Quartz;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthentication(AuthConstants.AUTH_KEY)
    .AddCookie(AuthConstants.AUTH_KEY, options =>
    {
        options.LoginPath = "/Auth/Login";
    });

builder.Services.AddAnimalWorldData(builder.Configuration.GetConnectionString("DefaultDbConnection"));
builder.Services.AddAnimalWorldCore();
builder.Services.AddScoped<IMapper<CredentialsViewModel, CredentialsDto>, AuthMapper>();
builder.Services.AddScoped<IReverseMapper<UserData, UserProfileViewModel>, UserProfileMapper>();
builder.Services.AddScoped<IAnimalFamilyMapper, AnimalFamilyMapper>();
builder.Services.AddScoped<IReverseMapper<PromotionData, PromotionViewModel>, PromotionMapper>();
builder.Services.AddScoped<IZooMapper, ZooMapper>();
builder.Services.AddScoped<IAnimalSpeciesMapper, AnimalSpeciesMapper>();
builder.Services.AddScoped<IMapper<RandomAnimalDto, RandomAnimalViewModel>, RandomAnimalMapper>();
builder.Services.AddScoped<IMapper<TicketData, TicketViewModel>, TicketMapper>();
builder.Services.AddScoped<IMapper<CommentData, CommentViewModel>, CommentMapper>();
builder.Services.AddScoped<IImageUploadHelper, ImageUploadHelper>();

builder.Services.AddHttpClient<RandomAnimalApi>(x =>
{
    x.BaseAddress = new Uri("https://api.some-random-api.com");
});

builder.Services.AddQuartz(q =>
{
    var jobKey = new JobKey("ZooPromotion");
    q.AddJob<PromotionsCheckJob>(opts => opts.WithIdentity(jobKey));
    q.AddTrigger(opts => opts
        .ForJob(jobKey)
        .WithCronSchedule("0 0 9-20 ? * *"));
});

builder.Services.AddQuartzHostedService(options =>
{
    options.WaitForJobsToComplete = true;
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

app.MapHub<ZoosHub>("/my-hub/zoos");
app.MapHub<PromotionsHub>("/my-hub/promotions");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();