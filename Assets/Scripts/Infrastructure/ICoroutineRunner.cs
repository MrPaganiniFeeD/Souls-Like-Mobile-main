using System.Collections;
using UnityEngine;

namespace DefaultNamespace.Infrastructure
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator coroutine);
    }
}