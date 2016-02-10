using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Media;
using Microsoft.CSharp;

namespace CSharpLiveCodingEnvironment.CodeCompilation
{
    internal class CodeCompiler
    {
        private readonly CSharpCodeProvider _compiler = new CSharpCodeProvider();
        private readonly CompilerParameters _compilerParameters;
        private readonly object _locker = new object();
        private readonly SynchronizationContext _synchronizationContext;
        private Task _compilingTask;
        private string _pendingCode;
        private bool _saveState;

        public string[] Footer =
        {
            "[AttributeUsage(AttributeTargets.Field)]",
            "public class StateField : Attribute {}",
            "[AttributeUsage(AttributeTargets.Method)]",
            "public class InitMethod : Attribute {}",
            "[AttributeUsage(AttributeTargets.Method)]",
            "public class DrawMethod : Attribute {}",
            "[AttributeUsage(AttributeTargets.Method)]",
            "public class TickMethod : Attribute {}",
            "[AttributeUsage(AttributeTargets.Method)]",
            "public class DrawTrackMethod : Attribute {}",
            "[AttributeUsage(AttributeTargets.Class)]",
            "public class MainClass : Attribute {}"
        };

        public string[] Header =
        {
            "using System.Windows.Media;",
            "using System.Windows;",
            "using System.Windows.Media.Imaging;",
            "using System.Collections.Generic;",
            "using System;"
        };

        public CodeCompiler(SynchronizationContext synchronizationContext)
        {
            _synchronizationContext = synchronizationContext;

            _compilerParameters = new CompilerParameters
            {
                GenerateExecutable = false,
                GenerateInMemory = true,
                IncludeDebugInformation = false
            };
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            foreach (var assembly in assemblies)
            {
                var name = assembly.GetName().Name;
                if (name == "PresentationFramework"
                    || name == "WindowsBase"
                    || name == "PresentationCore")
                {
                    _compilerParameters.ReferencedAssemblies.Add(assembly.Location);
                }
            }
        }

        public event CompiledHandler Compiled;
        public event CompilationErrorHandler CompilationError;

        public void CompileGameClass(string code, bool saveState)
        {
            lock (_locker)
            {
                if (_compilingTask == null)
                    _compilingTask = Task.Factory.StartNew(() => Compile(code, saveState));
                else
                {
                    _pendingCode = code;
                    _saveState = saveState;
                }
            }
        }

        private void Compile(string code, bool saveState)
        {
            var linesOffset = Header.Length;

            var compiledSuccessfully = false;
            var compilationErrorOnLine = -1;
            string compilationErrorText = null;

            string finalCode = $"{string.Join("\n", Header)}\n{code}\n{string.Join("\n", Footer)}";

            var results = _compiler.CompileAssemblyFromSource(_compilerParameters,
                finalCode);

            if (results.Errors.Count == 0)
            {
                var asem = results.CompiledAssembly;

                var types = asem.GetTypes();
                Type type = null;
                for (var i = 0; i < types.Length; ++i)
                {
                    var attrs = types[i].CustomAttributes;
                    foreach (var attr in attrs)
                    {
                        if (attr.AttributeType.Name == "MainClass")
                        {
                            type = types[i];
                            break;
                        }
                    }
                }

                if (type != null)
                {
                    var inst = Activator.CreateInstance(type);
                    var fs = type.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    var compiledData = new CompiledData
                    {
                        Instance = inst,
                        Fields = fs,
                        NeedToSaveState = saveState,
                        DrawTrackDelegates = new List<Action<DrawingContext>>()
                    };

                    var methods = type.GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
                    for (var i = 0; i < methods.Length; ++i)
                    {
                        foreach (var attr in methods[i].CustomAttributes)
                        {
                            if (attr.AttributeType.Name == "InitMethod" && IsInitMethodSignature(methods[i]))
                                compiledData.InitDelegate =
                                    (Action) Delegate.CreateDelegate(typeof (Action), inst, methods[i]);
                            if (attr.AttributeType.Name == "DrawMethod" && IsDrawMethodSignature(methods[i]))
                                compiledData.DrawDelegate =
                                    (Action<DrawingContext>)
                                        Delegate.CreateDelegate(typeof (Action<DrawingContext>), inst, methods[i]);
                            if (attr.AttributeType.Name == "DrawTrackMethod" && IsDrawTrackMethodSignature(methods[i]))
                                compiledData.DrawTrackDelegates.Add((Action<DrawingContext>)
                                    Delegate.CreateDelegate(typeof (Action<DrawingContext>), inst, methods[i]));
                            if (attr.AttributeType.Name == "TickMethod" && IsTickMethodSignature(methods[i]))
                                compiledData.TickDelegate =
                                    (Action<double, Dictionary<char, bool>>)
                                        Delegate.CreateDelegate(typeof (Action<double, Dictionary<char, bool>>), inst,
                                            methods[i]);
                        }
                    }

                    compiledSuccessfully = true;
                    if (Compiled != null)
                    {
                        var tmpCompiled = Compiled;
                        _synchronizationContext.Send(
                            o => tmpCompiled(this, new CompiledEventArgs(compiledData)), null);
                    }
                }
            }
            else
            {
                compilationErrorOnLine = results.Errors[0].Line - linesOffset;
                compilationErrorText = results.Errors[0].ErrorText;
            }

            if (!compiledSuccessfully && CompilationError != null)
            {
                _synchronizationContext.Send(
                    o =>
                        CompilationError(this,
                            new CompilationErrorEventArgs(compilationErrorOnLine, compilationErrorText)), null);
            }

            lock (_locker)
            {
                _compilingTask = null;
                if (_pendingCode != null)
                {
                    var tmp = string.Copy(_pendingCode);
                    _compilingTask = Task.Factory.StartNew(() => Compile(tmp, _saveState));
                    _pendingCode = null;
                }
            }
        }

        private bool IsInitMethodSignature(MethodInfo m)
        {
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 0;
        }

        private bool IsDrawMethodSignature(MethodInfo m)
        {
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 1 && p[0].ParameterType == typeof (DrawingContext);
        }

        private bool IsDrawTrackMethodSignature(MethodInfo m)
        {
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 1 && p[0].ParameterType == typeof (DrawingContext);
        }

        private bool IsTickMethodSignature(MethodInfo m)
        {
            var p = m.GetParameters();
            return m.ReturnType == typeof (void) && p.Length == 2 && p[0].ParameterType == typeof (double)
                   && p[1].ParameterType == typeof (Dictionary<char, bool>);
        }
    }
}