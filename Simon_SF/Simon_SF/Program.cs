using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Simon_SF.Client.Pages;
using Simon_SF.Components;
using Simon_SF.Components.Account;
using Simon_SF.Data;
using MudBlazor.Services;
using Blazor.SIMONStore.Repositories;
using Microsoft.Extensions.FileProviders;
using Microsoft.Data.SqlClient;
using System.Data;
using Blazor.SIMONStore.Client.Services;

using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProductService = Blazor.SIMONStore.Client.Services.ProductService;
using Repository;
using Microsoft.OpenApi.Models;
using Shared;
using System.Reflection;
using QuestPDF.Infrastructure;


QuestPDF.Settings.License = LicenseType.Community; // Agrega esta línea
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddMudServices();
// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();
builder.Services.Configure<GlobalSettings>(builder.Configuration.GetSection("GlobalSettings"));
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<IdentityUserAccessor>();
builder.Services.AddScoped<IdentityRedirectManager>();
builder.Services.AddScoped<AuthenticationStateProvider, PersistingRevalidatingAuthenticationStateProvider>();

builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = IdentityConstants.ApplicationScheme;
        options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
    })
    .AddIdentityCookies();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
builder.Services.AddSingleton<IDbConnection>((sp) => new SqlConnection(connectionString));

builder.Services.AddIdentityCore<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddSignInManager()
    .AddDefaultTokenProviders();
builder.Services.AddScoped<PdfRecibo>();
builder.Services.AddScoped<PdfService>();
builder.Services.AddSingleton<IEmailSender<ApplicationUser>, IdentityNoOpEmailSender>();
//Injectar Los Repositorios al Contenedor de Dependencias...de ProductCategoryRepository
builder.Services.AddScoped<IProductCategoryRepository, ProductCategoryRepository>();

//Injectar Los Repositorios al Contenedor de Dependencias...de ProductRepository
builder.Services.AddScoped<IProductRepository, ProductRepository>();

//Injectar Los Repositorios al Contenedor de Dependencias...de OrderRepository
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
//Injectar Los Repositorios al Contenedor de Dependencias...de OrderRepository
builder.Services.AddScoped<IPaymentOrderRepository, PaymentOrderRepository>();

//Injectar Los Repositorios al Contenedor de Dependencias...de ClientRepository
builder.Services.AddScoped<IClientRepository, ClientRepository>();

//Injectar Los Repositorios al Contenedor de Dependencias...de OrderProductRepository
builder.Services.AddScoped<IOrderProductRepository, OrderProductRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ISellerRepository, SellerRepository>();
builder.Services.AddScoped<ICostumerRepository, CostumerRepository>();
builder.Services.AddScoped<IBankRepository, BankRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

//builder.Services.AddSwaggerGen();
//Para Adminit XML y personalizar la Documentacion API
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API SIMON SF", Version = "v1" });

    // Ruta del archivo XML
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseUri").Value!)
}

 );
//Se debe agregar el servicio de ProductService
builder.Services.AddScoped<Blazor.SIMONStore.Client.Services.IProductService, ProductService>();

//Se debe agregar el servicio de ProductCategoryService
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

//Se debe agregar el servicio de ProductService


//Se debe agregar el servicio de OrderService
builder.Services.AddScoped<IOrderService, OrderService>();

//Se debe agregar el servicio de PaymentOrderService
builder.Services.AddScoped<IPaymentOrderService, PaymentOrderService>();

//Se debe agregar el servicio de OrderService
builder.Services.AddScoped<IClientService, ClientService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICostumerService, CostumerService>();
builder.Services.AddScoped<IBankService, BankService>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<ISellerService, SellerService>();
builder.Services.AddScoped<SweetAlertService>();  // Registrar el servicio en el contenedor de DI



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseMigrationsEndPoint();
    // Swagger middlewares
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // Change this to access Swagger at /swagger
    });
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
        c.RoutePrefix = "swagger"; // Change this to access Swagger at /swagger
    });
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"ImagenesProductos")),
    RequestPath = new PathString("/ImagenesProductos")
});
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(Simon_SF.Client._Imports).Assembly);

// Add additional endpoints required by the Identity /Account Razor components.
app.MapAdditionalIdentityEndpoints();

app.MapControllers();
app.Run();
