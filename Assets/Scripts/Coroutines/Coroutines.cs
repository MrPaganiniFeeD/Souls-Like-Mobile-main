using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public sealed class Coroutines : MonoBehaviour
{
    private static Coroutines m_instance;

    private static Coroutines _instance
    {
        get
        {
            Debug.Log("Instatiate coroutines");
            if (m_instance == null)
            {
                var gameObject = new GameObject("[COROUTINE SERVICES]");
                m_instance = gameObject.AddComponent<Coroutines>();
                DontDestroyOnLoad(gameObject);
            }

            return m_instance;
        }
    }

    public static Coroutine StartCoroutines(IEnumerator enumerator)
    {
        Debug.Log("StartCoroutines");
        return _instance.StartCoroutine(enumerator);
    }

    public static void StopCoroutines(Coroutine coroutine)
    {
        if (_instance == null)
            return;
        
        _instance.StopCoroutine(coroutine);
    }
}
