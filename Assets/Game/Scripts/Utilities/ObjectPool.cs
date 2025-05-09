using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;
using Wargon.DI;
using Wargon.ezs;
using Wargon.ezs.Unity;

namespace Wargon.TestGame
{
    public class ObjectPool
    {
        private readonly Dictionary<int, object> pools = new();
        private EntityViewMap entityViewMap;

        private ObjectPool<GameObject> GetPool(GameObject prefab, int initialSize = 100, int maxSize = 200)
        {
            var instanceID = prefab.GetInstanceID();

            if (!pools.TryGetValue(instanceID, out var poolObj))
            {
                var newRoot = new GameObject($"{prefab.name}_{instanceID} POOL");
                var pool = new ObjectPool<GameObject>(
                    () =>
                    {
                        var go = Object.Instantiate(prefab, newRoot.transform);

                        if (go.TryGetComponent(out MonoEntity monoEntity))
                        {
                            ref var e = ref monoEntity.ConvertToEntity();
                            e.Add(new PrefabKey { Value = instanceID });
                            entityViewMap.AddEntity(go.GetInstanceID(), e);
                        }

                        go.SetActive(false);
                        return go;
                    },
                    obj => obj.SetActive(true),
                    obj => obj.SetActive(false),
                    obj => Object.Destroy(obj.gameObject),
                    true,
                    initialSize,
                    maxSize
                );

                pools[instanceID] = pool;
                return pool;
            }

            return (ObjectPool<GameObject>)poolObj;
        }

        private ObjectPool<T> GetPool<T>(T prefab, int initialSize = 100, int maxSize = 200) where T : Component
        {
            var instanceID = prefab.GetInstanceID();

            if (!pools.TryGetValue(instanceID, out var poolObj))
            {
                var newRoot = new GameObject($"{prefab.name}_{instanceID} POOL");
                var pool = new ObjectPool<T>(
                    () =>
                    {
                        var obj = Object.Instantiate(prefab, newRoot.transform);
                        Injector.Resolve(obj);
                        if (obj.TryGetComponent(out MonoEntity monoEntity))
                        {
                            ref var e = ref monoEntity.ConvertToEntity();
                            e.Add(new PrefabKey { Value = instanceID });
                            entityViewMap.AddEntity(obj.gameObject.GetInstanceID(), e);
                        }

                        obj.gameObject.SetActive(false);
                        return obj;
                    },
                    obj => obj.gameObject.SetActive(true),
                    obj => obj.gameObject.SetActive(false),
                    obj => Object.Destroy(obj.gameObject),
                    false,
                    initialSize,
                    maxSize
                );

                pools[instanceID] = pool;
                return pool;
            }

            return (ObjectPool<T>)poolObj;
        }

        public T Spawn<T>(T prefab, Vector3 position, Quaternion rotation, Transform parent = null) where T : Component
        {
            var pool = GetPool(prefab);
            var obj = pool.Get();
            obj.transform.SetPositionAndRotation(position, rotation);
            if (entityViewMap.TryGetEntity(obj.gameObject.GetInstanceID(), out var e))
            {
                e.Add<PooledEvent>();
                e.Remove<Inactive>();
            }
            return obj;
        }

        public GameObject Spawn(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent = null)
        {
            var pool = GetPool(prefab);
            var obj = pool.Get();
            obj.transform.SetPositionAndRotation(position, rotation);
            if (entityViewMap.TryGetEntity(obj.GetInstanceID(), out var e))
            {
                e.Add<PooledEvent>();
                e.Remove<Inactive>();
            }
            return obj;
        }

        public void Release<T>(T obj, int key) where T : Component
        {
            if (pools.TryGetValue(key, out var poolObj))
            {
                var pool = poolObj as ObjectPool<T>;
                pool?.Release(obj);
            }
            else
            {
                Debug.LogWarning($"No pool found for instance ID: {key}");
                Object.Destroy(obj.gameObject);
            }
        }

        public void Release(GameObject obj, int key)
        {
            if (pools.TryGetValue(key, out var poolObj))
            {
                var pool = poolObj as ObjectPool<GameObject>;
                pool?.Release(obj);
            }
            else
            {
                Debug.LogWarning($"No pool found for instance ID: {key}");
                Object.Destroy(obj);
            }
        }
    }

    [EcsComponent]
    public struct PrefabKey
    {
        public int Value;
    }
}