using System;
using System.Collections.Generic;
using UnityEngine;

namespace Core
{
    public class ProjectContext: IDisposable
    {
        private static ProjectContext _instance;
        private readonly Context _context = new Context();
        private readonly List<IContainer> _containers = new List<IContainer>();

        public static void AddContainer(IContainer context)
        {
            if (_instance is null) return;
            _instance._containers.Add(context);
        }

        public static void RemoveContainer(IContainer context)
        {
            if (_instance is null || _instance._containers.Contains(context) == false) return;
            _instance._containers.Remove(context);
        }

        public static void Start<T>() where T:State
        {
            _instance._context.ChangeTo((T)Activator.CreateInstance(typeof(T), _instance._context));
        }

        public void Dispose()
        {
            _instance = null;
        }

        internal static bool TryResolve<T>(out T instance)
        {
            foreach (IContainer scene in _instance._containers)
            {
                if (scene.TryResolve(out instance)) return true;
            }

            instance = default;
            return false;
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void Initialize()
        {
            _instance = new ProjectContext();
        }

        private ProjectContext()
        {
            Application.quitting += Dispose;
        }
    }
}