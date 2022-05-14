using System.Text;
using System.Xml.Linq;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Text;

namespace SourceCodeGenerators
{
    [Generator]
    public class CUISpecificationGenerator : ISourceGenerator
    {
        public void Execute(GeneratorExecutionContext context)
        {
            var sourceBuilder = new StringBuilder(@"
using System;
namespace HelloWorldTest
{
    public static class HelloWorld
    {   
        public static void Say()
            {
                Console.WriteLine(""Hello"");
            }
    }
}");

            context.AddSource("HelloWorldGenerator", SourceText.From(sourceBuilder.ToString()));
        }

        public void Initialize(GeneratorInitializationContext context)
        {
        }
    }
}