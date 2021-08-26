using System;
using TrueVUE.Cloud.SDK.Common.Interfaces;

namespace TrueVUE.Cloud.SDK.Common
{
    public class LazyDependency<T> : Lazy<T>, ILazyDependency<T>
    {
        public LazyDependency(Func<T> valueFactory) : base(valueFactory)
        {
        }
    }
}
