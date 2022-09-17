using ErrorHandlingAsp.NetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//uygulama bazýnda CustomHandleExceptionFilterAttribute kullanma
builder.Services.AddMvc(options =>
{
    options.Filters.Add(new CustomHandleExceptionFilterAttribute() { ErrorPage = "Error1"});
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    //1. yol
    //app.UseStatusCodePages("text/plain", "Bir hata var. Durum Kodu: {0}");

    //2.yol
    app.UseStatusCodePages(context =>
    {
        context.Run(async page =>
        {
            page.Response.StatusCode = 500;
            page.Response.ContentType = "text/html";
            await page.Response.WriteAsync($"<html> <head> <h1> Hata var: {page.Response.StatusCode}</h1> </head> </html>");
        });
    });

    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();