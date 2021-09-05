namespace BDD
{
    public static class Context
    {
        public static Given<T> Given<T>(T context) => new Given<T>(context);
    }
}