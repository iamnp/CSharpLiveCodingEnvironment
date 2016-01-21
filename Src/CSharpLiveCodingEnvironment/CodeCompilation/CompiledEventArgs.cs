using System;

namespace CSharpLiveCodingEnvironment.CodeCompilation
{
    internal class CompiledEventArgs : EventArgs
    {
        public CompiledEventArgs(CompiledData compiledData)
        {
            CompiledData = compiledData;
        }

        public CompiledData CompiledData { get; }
    }
}