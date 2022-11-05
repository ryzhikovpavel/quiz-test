using System;
using System.Threading;
using JetBrains.Annotations;

namespace Core
{
    [PublicAPI]
    public abstract class State: IDisposable
    {
        protected readonly CancellationTokenSource Cancellation = new CancellationTokenSource();
        public readonly Context Context;
        public abstract void Enter();
        public abstract void Exit();

        public State(Context context)
        {
            Context = context;
        }
        
        public void Dispose()
        {
            Cancellation.Cancel();
            Cancellation?.Dispose();
        }
    }
}