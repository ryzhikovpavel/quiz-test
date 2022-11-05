using System;
using JetBrains.Annotations;

namespace Core
{
    [PublicAPI]
    public interface IContainer
    {
        internal bool TryResolve<T>(out T instance);

        public void AddInstance(object instance, params Type[] types);
        public void AddInstance<T>(T instance)
        {
            AddInstance(instance, typeof(T));
        }
        
        public void AddInstance<T, TInterface>(T instance)
        {
            AddInstance(instance, typeof(T), typeof(TInterface));
        }
        
        public void AddInstance<T, TInterface1, TInterface2>(T instance)
        {
            AddInstance(instance, typeof(T), typeof(TInterface1), typeof(TInterface2));
        }
    }
}