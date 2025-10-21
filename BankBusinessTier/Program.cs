using RestSharp;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<RestClient>(client =>
    new RestClient(new RestClientOptions
    {
        BaseUrl = new Uri("http://localhost:5193"),
        Timeout = TimeSpan.FromSeconds(2)
    })
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        //policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        policy.WithOrigins("https://localhost:7096", "http://localhost:5103")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

app.UseCors("AllowFrontend");

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();


app.Run();
