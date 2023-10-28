using Infrastructure.DI;
using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public class AssetsProvider : IAssets
    {
        public GameObject Instantiate(string namePrefab)
        {
            GameObject prefab = FindPrefab(namePrefab);
            return DiContainerSceneRef.Container.InstantiatePrefab(prefab);
        }

        public GameObject Instantiate(string namePrefab, Vector3 position)
        {
            GameObject prefab = FindPrefab(namePrefab);
            return DiContainerSceneRef.Container.InstantiatePrefab(prefab, position, Quaternion.identity, null);
        }

        public GameObject Instantiate(string namePrefab, Quaternion quaternion, Vector3 position, Transform parent)
        {
            GameObject prefab = FindPrefab(namePrefab);
            return DiContainerSceneRef.Container.InstantiatePrefab(prefab, parent);
        }

        public GameObject Instantiate(string namePrefab, GameObject parent)
        {
            throw new System.NotImplementedException();
        }

        public T GetObjectForType<T>(string path) where T : Object => 
            GetLoadedObject<T>(path);

        public GameObject InstantiateNonZenject(string namePrefabs)
        {
            GameObject prefab = FindPrefab(namePrefabs);;
            return Object.Instantiate(prefab);
        }

        public GameObject InstantiateNonZenject(string namePrefabs, Vector3 at)
        {
            GameObject prefab = FindPrefab(namePrefabs);;
            return Object.Instantiate(prefab, at, Quaternion.identity, null);
        }

        private static GameObject FindPrefab(string namePrefabs) => 
            Resources.Load<GameObject>(namePrefabs);

        private static T GetLoadedObject<T>(string path) where T : Object => 
            Resources.Load<T>(path);
    }
}