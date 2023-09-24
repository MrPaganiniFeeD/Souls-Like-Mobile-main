﻿using Infrastructure.Services;
using UnityEngine;

namespace Infrastructure.AssetsManagement
{
    public interface IAssets : IService
    {
        GameObject Instantiate(string namePrefab);
        GameObject Instantiate(string namePrefab, Vector3 position);

        T GetObjectForType<T>(string path) where T : Object;

        GameObject InstantiateNonZenject(string namePrefab);
        GameObject InstantiateNonZenject(string namePrefab, Vector3 at);
    }   
}