namespace CSharpLiveCodingEnvironment.CodeEditing
{
    /// <summary>
    ///     Holds info about position in text.
    /// </summary>
    internal class Position
    {
        public int Column;
        public int Line;

        /// <summary>
        ///     Returns a copy of current Position instance.
        /// </summary>
        public Position Copy()
        {
            return new Position {Line = Line, Column = Column};
        }
    }
}