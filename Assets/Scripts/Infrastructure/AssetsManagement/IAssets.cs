using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string namePrefab);
        GameObject Instantiate(string namePrefab, Vector3 position);
        GameObject Instantiate(string namePrefab, Quaternion quaternion, Vector3 position, Transform parent);

        T GetObjectForType<T>(string path) where T : Object;

        GameObject InstantiateNonZenject(string namePrefab);
        GameObject InstantiateNonZenject(string namePrefab, Vector3 at);
    }   
}