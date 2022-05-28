using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase1_Display;
using GUI_Generator_UseCase1_Display.Generator;
using GUI_Generator_UseCase1_Display.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.UseCases.DisplayOnly.UseCase1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ISpecificationElementVisitor<SensorData>, DefaultElementVisitor>();
builder.Services.AddSingleton<IXMLSpecificationConverter<SensorData>, XmlConverter>();
builder.Services.AddSingleton<IAdaptiveInterfaceGenerator<SensorData>, AdaptiveInterfaceGenerator>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
