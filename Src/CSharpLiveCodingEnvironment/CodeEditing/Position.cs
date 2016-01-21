namespace CSharpLiveCodingEnvironment.CodeEditing
{
    internal class Position
    {
        public int Column;
        public int Line;

        public Position Copy()
        {
            return new Position {Line = Line, Column = Column};
        }
    }
}