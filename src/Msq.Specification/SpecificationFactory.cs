namespace Msq.Specification
{
    public static class Specification
    {
        public static AllSpecification<T> MatchAll<T>() where T: class
        {
            return new AllSpecification<T>();
        }
        public static NoneSpecification<T> MatchNone<T>() where T : class
        {
            return new NoneSpecification<T>();
        }
    }
}
