using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase1_Interaction;
using GUI_Generator_UseCase1_Interaction.Generator;
using GUI_Generator_UseCase1_Interaction.Helpers;
using GUI_Generator_UseCase1_InteractionHelpers;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Models.UseCases.IncludingUserInteraction.UseCase1;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
builder.Services.AddSingleton<ISpecificationElementVisitor<LoginModel>, DefaultElementVisitor>();
builder.Services.AddSingleton<IXMLSpecificationConverter<LoginModel>, XmlConverter>();
builder.Services.AddSingleton<IAdaptiveInterfaceGenerator<LoginModel>, AdaptiveInterfaceGenerator>();

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

await builder.Build().RunAsync();
