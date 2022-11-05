using Core;
using UnityEngine;

namespace Views
{
    internal class ViewsSceneInitializer : SceneInitializer
    {
        public override void Initialize(IContainer container)
        {
            var views = GetComponentsInChildren<BaseView>(true);
            foreach (var view in views)
            {
                view.gameObject.SetActive(false);
                container.AddInstance(view, view.GetType());
            }
        }
    }
}