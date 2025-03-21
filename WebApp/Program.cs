using Business.Services;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();

//builder.Services.AddScoped<IClientService, ClientService>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Register}/{id?}")
    .WithStaticAssets();

app.Run();
