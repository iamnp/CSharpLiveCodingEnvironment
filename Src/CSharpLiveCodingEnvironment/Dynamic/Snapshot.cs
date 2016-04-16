using System.Collections.Generic;

namespace CSharpLiveCodingEnvironment.Dynamic
{
    /// <summary>
    ///     Holds info about game state snapshot.
    /// </summary>
    internal class Snapshot
    {
        public double Dt;
        public Dictionary<char, bool> Input;
        public Dictionary<string, object> State;
    }
}