using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HexTecGames.Basics
{
    [System.Serializable]
    public class Spawner<T> where T : Component
    {
        public T Prefab
        {
            get
            {
                return prefab;
            }
            set
            {
                prefab = value;
            }
        }
        [SerializeField] private T prefab = default;

        public Transform Parent
        {
            get
            {
                return parent;
            }
            set
            {
                parent = value;
            }
        }
        [SerializeField] private Transform parent = default;

        private readonly List<T> behaviours = new List<T>();


        public T Spawn()
        {
            if (prefab == null)
            {
                Debug.LogWarning("Prefab is not assigned!");
                return null;
            }
            if (behaviours.Any(x => x.gameObject.activeSelf == false))
            {
                T behaviour = behaviours.Find(x => x.gameObject.activeSelf == false);
                behaviour.gameObject.SetActive(true);
                return behaviour;
            }
            else
            {
                T behaviour = Object.Instantiate(prefab, parent);
                behaviours.Add(behaviour);
                return behaviour;
            }
        }

        public List<T> GetBehaviours(bool activeOnly = true)
        {
            List<T> results = new List<T>();
            foreach (var behaviour in behaviours)
            {
                if (!activeOnly || behaviour.gameObject.activeInHierarchy)
                {
                    results.Add(behaviour);
                }
            }
            return results;
        }

        public void ConsumeInstances(List<T> list)
        {
            behaviours.AddRange(list);
        }

        public void DeactivateAll()
        {
            foreach (var behaviour in behaviours)
            {
                behaviour.gameObject.SetActive(false);
            }
        }
    }
}