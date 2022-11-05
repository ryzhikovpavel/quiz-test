using UnityEngine;

namespace Core
{
    public abstract class SceneInitializer: MonoBehaviour
    {
        public abstract void Initialize(IContainer container);
    }
}