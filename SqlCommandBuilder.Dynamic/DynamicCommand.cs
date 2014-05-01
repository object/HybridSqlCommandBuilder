namespace SqlCommandBuilder.Dynamic
{
    public class DynamicCommand
    {
        public static dynamic Expression
        {
            get { return new DynamicCommandExpression(); }
        }
    }
}