using FluentValidation.AspNetCore;
using MakeSurpriseProject.Bussiness;
using MakeSurpriseProject.Contexts;
using MakeSurpriseProject.DataAccess;
using MakeSurpriseProject.Services;
using MakeSurpriseProject.Validators;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        builder => builder.AllowAnyOrigin()
                          .AllowAnyMethod()
                          .AllowAnyHeader());
});
builder.Services.AddDbContext<MakeSurpriseDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<SpecialDayCalenderDal>();
builder.Services.AddScoped<ProfileDal>(); 
builder.Services.AddScoped<AddressManager>();
builder.Services.AddScoped<ShoppingManager>();
builder.Services.AddScoped<EfShoppingDal>();
builder.Services.AddScoped<EfAddressDal>();
builder.Services.AddScoped<CargoTrackingManager>();
builder.Services.AddScoped<EfCargoTrackingDal>();
builder.Services.AddScoped<AuthManager>();
builder.Services.AddScoped<ProfileManager>();
builder.Services.AddScoped<EfUserProfileDal>();
builder.Services.AddScoped<SpecialDayCalendarManager>();
builder.Services.AddScoped<MailManager>();
builder.Services.AddScoped<UserInfoManager>();
builder.Services.AddScoped<EfUserInfoDal>();
builder.Services.AddScoped<EfCargoTrackingDal>();
builder.Services.AddScoped<ProfileManager>();
builder.Services.AddMemoryCache();
builder.Services.AddControllers()
    .AddFluentValidation(fv =>
    {
        fv.RegisterValidatorsFromAssemblyContaining<RegisterRequestValidator>();
        fv.RegisterValidatorsFromAssemblyContaining<LoginRequestValidator>();
    });
//builder.Services.AddValidatorsFromAssemblyContaining<RegisterRequestValidator>();
builder.Services.AddEndpointsApiExplorer();
//builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseCors("AllowAllOrigins");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints => endpoints.MapDefaultControllerRoute());

app.UseAuthorization();
app.MapControllers();

app.Run();
