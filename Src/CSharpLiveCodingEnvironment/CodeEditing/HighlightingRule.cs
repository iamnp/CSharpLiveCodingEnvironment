using System.Drawing;
using System.Text.RegularExpressions;

namespace CSharpLiveCodingEnvironment.CodeEditing
{
    internal class HighlightingRule
    {
        public Color Color;
        public Regex Pattern;
    }
}