using System;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class FieldsChangedEventArgs : EventArgs
    {
        public FieldsChangedEventArgs(Tuple<string, string>[] fields)
        {
            Fields = fields;
        }

        public Tuple<string, string>[] Fields { get; }
    }
}