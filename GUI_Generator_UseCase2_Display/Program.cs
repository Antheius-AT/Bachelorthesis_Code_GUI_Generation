using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase1_Display.Generator;
using GUI_Generator_UseCase1_Display.Helpers;
using GUI_Generator_UseCase2_Display;
using GUI_Generator_UseCase2_Display.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.UseCases.DisplayOnly.UseCase2;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddSingleton<ISpecificationElementVisitor<PersonalDetails>, DefaultElementVisitor>();
builder.Services.AddSingleton<IXMLSpecificationConverter<PersonalDetails>, XmlConverter>();
builder.Services.AddSingleton<IAdaptiveInterfaceGenerator<PersonalDetails>, AdaptiveInterfaceGenerator>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
