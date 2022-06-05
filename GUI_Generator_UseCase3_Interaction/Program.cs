using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase3_Interaction;
using GUI_Generator_UseCase3_Interaction.Generator;
using GUI_Generator_UseCase3_Interaction.Helpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.UseCases.IncludingUserInteraction.UseCase3;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddSingleton<ISpecificationElementVisitor<EditToolBox>, DefaultElementVisitor>();
builder.Services.AddSingleton<IXMLSpecificationConverter<EditToolBox>, XmlConverter>();
builder.Services.AddSingleton<IAdaptiveInterfaceGenerator<EditToolBox>, AdaptiveInterfaceGenerator>();

await builder.Build().RunAsync();
