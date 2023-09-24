using System;
using System.Linq;
using DefaultNamespace.Logic;
using PlasticGui.WebApi.Responses;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

    [CustomEditor(typeof(UniqueId))]
    public class UniqueIdEditor : UnityEditor.Editor
    {
        private void OnEnable()
        {
            UniqueId uniqueId = (UniqueId)target;

            if (IsPrefab(uniqueId))
                return;

            if (string.IsNullOrEmpty(uniqueId.Id))
                GenerateId(uniqueId);
            else
            {
               UniqueId[] uniqueIds = FindObjectsOfType<UniqueId>();
               
               if(uniqueIds.Any(other => other != uniqueId && other.Id == uniqueId.Id))
                   GenerateId(uniqueId);
            }
        }

        private bool IsPrefab(UniqueId uniqueId) => 
            uniqueId.gameObject.scene.rootCount == 0;

        private void GenerateId(UniqueId uniqueId)
        {
            uniqueId.Id = $"{Guid.NewGuid().ToString()}_{Guid.NewGuid().ToString()}";

            if (Application.isPlaying == false)
            {
                EditorUtility.SetDirty(uniqueId);
                EditorSceneManager.MarkSceneDirty(uniqueId.gameObject.scene);
            }
        }
    }