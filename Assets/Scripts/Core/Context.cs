using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

namespace Core
{
    [PublicAPI]
    public class Context
    {
        public State Current { get; private set; }

        public bool TryResolve<T>(out T instance)
            => ProjectContext.TryResolve(out instance);

        public T Resolve<T>()
        {
            if (TryResolve(out T instance)) return instance;
            throw new Exception($"{typeof(T).Name} not resolved");
        }

        public void ChangeTo(State state)
        {
            Current?.Exit();
            Current = state;
            state.Enter();
        }

        public void Exit()
        {
            Current?.Exit();
            Current = null;
        }
    }
}