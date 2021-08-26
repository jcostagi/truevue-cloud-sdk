namespace TrueVUE.Cloud.SDK.Common.Interfaces
{
    public interface ILazyDependency<T>
    {
        T Value { get; }
    }
}
