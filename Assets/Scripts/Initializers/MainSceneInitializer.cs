using Core;
using Dictionary;
using States;
using UnityEngine;

namespace Initializers
{
    public class MainSceneInitializer: SceneInitializer
    {
        [SerializeField] private Config config;
        
        public override void Initialize(IContainer container)
        {
            container.AddInstance(new WordDictionary());
            container.AddInstance(config);
            ProjectContext.Start<LaunchState>();
        }
    }
}