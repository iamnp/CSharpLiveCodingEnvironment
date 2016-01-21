using System.Collections.Generic;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    internal class Snapshot
    {
        public double Dt;
        public Dictionary<char, bool> Input;
        public Dictionary<string, object> State;
    }
}