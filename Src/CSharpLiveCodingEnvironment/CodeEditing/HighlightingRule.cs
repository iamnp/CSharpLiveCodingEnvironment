using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpLiveCodingEnvironment.CodeEditing
{
    /// <summary>
    ///     Holds info about highlighting rule.
    /// </summary>
    internal class HighlightingRule
    {
        public Color Color;
        public Regex Pattern;
    }
}