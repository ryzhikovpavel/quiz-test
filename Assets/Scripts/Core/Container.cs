using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    internal class Container: IContainer
    {
        private readonly Dictionary<Type, object> _objects = new Dictionary<Type, object>();

        bool IContainer.TryResolve<T>(out T instance)
        {
            if (_objects.TryGetValue(typeof(T), out var obj))
            {
                instance = (T)obj;
                return true;
            }

            instance = default;
            return false;
        }

        void IContainer.AddInstance(object instance, params Type[] types)
        {
            foreach (Type type in types)
            {
                if (_objects.ContainsKey(type))
                {
                    Debug.LogError($"{type.Name} is already added with {_objects[type]} object");
                    return;
                }
                _objects[type] = instance;
            }
        }
    }
}