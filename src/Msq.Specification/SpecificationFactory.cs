namespace Msq.Specification
{
    public static class SpecificationFactory
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
