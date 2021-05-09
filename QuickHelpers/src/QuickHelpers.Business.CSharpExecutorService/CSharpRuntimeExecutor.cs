using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using QuickHelpers.Business.CSharpExecutorService.Exceptions;

namespace QuickHelpers.Business.CSharpExecutorService
{
    public class CSharpRuntimeExecutor
    {
        private const int MaxExecutingMilliSeconds = 5000;

        private readonly string[] _stopWordsInUserCode =
        {
            "System.IO",
            "System.Reflection",
            "System.Web",
            "System.Net",
            "System.Threading",
            "Console.WriteLine",
            "Console.Write"
        };

        public async Task<IReadOnlyCollection<string>> ExecuteAsync(string code)
        {
            if (code is null)
                return new[] {"Code is null"};

            try
            {
                ValidateWordsInCode(code);

                var createDllResult = CreateDll(code);
                var executingResult = await ExecuteCodeAsync(createDllResult);

                return new[] {executingResult};
            }
            catch (Exception e)
            {
                return e.Message.Split(Environment.NewLine);
            }
        }

        private async Task<string> ExecuteCodeAsync(Stream dll)
        {
            var assemblyContextName = Guid.NewGuid().ToString();
            var assemblyContext = new AssemblyLoadContext(assemblyContextName, true);

            try
            {
                dll.Position = 0;
                var targetAssembly = assemblyContext.LoadFromStream(dll);
                var targetModule = targetAssembly.GetModules().Single();
                var targetType = targetModule.GetType("SampleNamespace.SampleClass");

                const BindingFlags fieldsFlag = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
                targetType?.GetField("_valueToReturn", fieldsFlag)?.SetValue(targetType, null);

                var targetMethod = targetType?.GetMethod("SampleMethod");

                var completed = ExecuteWithTimeLimit(TimeSpan.FromMilliseconds(MaxExecutingMilliSeconds),
                    () => { targetMethod?.Invoke(targetType, null); });

                if (!completed)
                    throw new TimeoutException($"Time limit to execute code - {MaxExecutingMilliSeconds}");

                var retval = targetType?.GetField("_valueToReturn", fieldsFlag)?.GetValue(targetType)?.ToString();
                return retval;
            }
            finally
            {
                assemblyContext.Unload();
                await dll.DisposeAsync();
            }
        }

        private bool ExecuteWithTimeLimit(TimeSpan timeSpan, Action codeBlock)
        {
            var task = Task.Run(codeBlock);
            task.Wait(timeSpan);

            return task.IsCompleted;
        }

        private void ValidateWordsInCode(string userCode)
        {
            var invalidWordsInCode = _stopWordsInUserCode
                .Where(userCode.Contains)
                .Select(s => ($"{s} is stop word"))
                .ToArray();

            if (!invalidWordsInCode.Any())
                return;

            var errors = string.Join(Environment.NewLine, invalidWordsInCode);
            throw new StopWordException(errors);
        }

        private Stream CreateDll(string userCode)
        {
            var sampleCs = GetSampleClass();
            sampleCs = sampleCs.Replace("//CodeHere", userCode);

            var (emitResult, dll) = Compile(sampleCs);

            if (emitResult.Success)
                return dll;

            var emitErrors = emitResult
                .Diagnostics
                .Where(type =>
                    type.DefaultSeverity == DiagnosticSeverity.Error
                ).Select(s => s.ToString().Split(":").Last());

            var emitStrErrors = string.Join(Environment.NewLine, emitErrors);
            throw new CompilationException(emitStrErrors);
        }

        private static string GetSampleClass()
        {
            var executableLocation = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            if (executableLocation is null)
                throw new ArgumentNullException(nameof(executableLocation));

            var fileLocation = Path.Combine(executableLocation, "Storage\\SampleClass.cs");
            var sampleCs = File.ReadAllText(fileLocation);
            return sampleCs;
        }

        private (EmitResult Result, Stream Dll) Compile(string source)
        {
            var parsedSyntaxTree = CompilerSettings.Parse(source,
                CSharpParseOptions.Default.WithLanguageVersion(LanguageVersion.CSharp9)
            );

            var dllName = $"{Guid.NewGuid()}.dll";
            var compilation = CSharpCompilation.Create(dllName, new[] {parsedSyntaxTree},
                CompilerSettings.DefaultReferences, CompilerSettings.DefaultCompilationOptions);

            var steam = new MemoryStream();
            var result = compilation.Emit(steam);

            return result.Success
                ? (result, steam)
                : (result, null);
        }
    }
}