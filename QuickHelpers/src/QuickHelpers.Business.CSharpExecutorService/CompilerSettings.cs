using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Text;

namespace QuickHelpers.Business.CSharpExecutorService
{
    public static class CompilerSettings
    {
        public static readonly IEnumerable<string> DefaultNamespaces =
            new[]
            {
                "System",
                "System.Linq",
                "System.Collections.Generic",
                "System.Threading.Tasks"
            };

        public static readonly IEnumerable<MetadataReference> DefaultReferences =
            new[]
            {
                MetadataReference.CreateFromFile(typeof(object).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Linq.Enumerable).Assembly.Location),
                MetadataReference.CreateFromFile(typeof(System.Collections.Queue).Assembly.Location)
            };

        public static readonly CSharpCompilationOptions DefaultCompilationOptions =
            new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary)
                .WithOverflowChecks(true)
                .WithOptimizationLevel(OptimizationLevel.Release)
                .WithUsings(DefaultNamespaces);

        public static SyntaxTree Parse(string text, CSharpParseOptions options = null)
        {
            var stringText = SourceText.From(text, Encoding.UTF8);
            return SyntaxFactory.ParseSyntaxTree(stringText, options);
        }

        public static string SampleClass = @"
using System;
using System.Linq;

namespace SampleNamespace
{
    static class SampleClass
    {
        private static object _valueToReturn;
        
        public static void SampleMethod()
        {
            //CodeHere
        }

        private static void Return<T>(T result)
        {
            _valueToReturn = result;
        }

        private static void Output<T>(T result)
        {
            _valueToReturn = result;
        }

        private static void Out<T>(T result)
        {
            _valueToReturn = result;
        }
    }
}
";
    }
}