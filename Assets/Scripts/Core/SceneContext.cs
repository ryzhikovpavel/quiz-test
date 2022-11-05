using UnityEngine;

namespace Core
{
    internal class SceneContext : MonoBehaviour
    {
        [SerializeField] private SceneInitializer[] initializers;
        private readonly IContainer _container = new Container();

        private void Awake()
        {
            ProjectContext.AddContainer(_container);
            foreach (SceneInitializer initializer in initializers)
            {
                initializer.Initialize(_container);
            }
        }

        private void OnDestroy()
        {
            ProjectContext.RemoveContainer(_container);
        }
    }
}