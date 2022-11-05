using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Core
{
    [PublicAPI]
    [Serializable]
    public class ComponentPool<T>: IEnumerable<T> where T : Component
    {
        private struct ComponentPoolEnumerator: IEnumerator<T>
        {
            private List<T> _items;
            private int _index;
            public ComponentPoolEnumerator(List<T> items)
            {
                _items = items;
                Current = null;
                _index = -1;
            }

            public bool MoveNext()
            {
                _index++;
                if (_index < _items.Count)
                {
                    Current = _items[_index];
                    if (Current == null || Current.gameObject.activeSelf == false)
                        return MoveNext();
                    return true;
                }

                Current = null;
                return false;
            }

            public void Reset()
            {
                Current = null;
                _index = -1;
            }

            public T Current { get; private set; }

            object IEnumerator.Current => Current;

            public void Dispose()
            {
                Current = null;
                _items = null;
            }
        }
        
        [SerializeField] private Transform root;
        [SerializeField] private T prefab;
        private readonly List<T> _items = new List<T>();

        public Transform Root => root;
        public T Prefab => prefab;

        public void ReleaseAll()
        {
            foreach (T t in _items)
            {
                if (t == null) continue;
                t.gameObject.SetActive(false);
                if (root != null)
                    t.transform.SetParent(root);
            }
        }

        public T Get()
        {
            for (int i = 0; i < _items.Count; i++)
            {
                if (_items[i] == null)
                {
                    _items[i] = Create();
                    return _items[i];
                }

                if (_items[i].gameObject.activeSelf) continue;
                _items[i].gameObject.SetActive(true);
                return _items[i];
            }

            T item = Create();
            _items.Add(item);
            return item;
        }

        private T Create()
        {
            T go = Object.Instantiate(prefab);
            Transform t = go.transform;
            if (root != null)
                t.SetParent(root);
                
            t.localPosition = Vector3.zero;
            t.localScale = Vector3.one;
            go.gameObject.SetActive(true);
            return go;
        }

        public IEnumerator<T> GetEnumerator()
            => new ComponentPoolEnumerator(_items);

        IEnumerator IEnumerable.GetEnumerator()
            => GetEnumerator();
    }
}