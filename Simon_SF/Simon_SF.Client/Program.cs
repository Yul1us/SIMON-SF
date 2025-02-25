using Blazor.SIMONStore.Client.Services;
using Simon_SF.Client;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var cultureInfo = new System.Globalization.CultureInfo("es-ES");
System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddMudServices();
builder.Services.AddMudServices();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped(sp => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
}

 );

builder.Services.AddSingleton<AuthenticationStateProvider, PersistentAuthenticationStateProvider>();
//Se debe agregar el servicio de ProductCategoryService
builder.Services.AddScoped<IProductCategoryService, ProductCategoryService>();

//Se debe agregar el servicio de ProductService
builder.Services.AddScoped<IProductService, ProductService>();

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


await builder.Build().RunAsync();
