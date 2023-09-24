using UnityEngine;

namespace NTC.Global.System
{
    public sealed class ColliderOperations : MonoBehaviour
    {
        private const string EnableCollidersMethodTitle = "Enable Colliders";
        private const string DisableCollidersMethodTitle = "Disable Colliders";
        private const string DestroyCollidersMethodTitle = "Destroy Colliders";
        

        [ContextMenu(EnableCollidersMethodTitle)]
        private void EnableColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();

            foreach (var entity in colliders)
            {
                entity.enabled = true;
            }
        }

        [ContextMenu(DisableCollidersMethodTitle)]
        private void DisableColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();

            foreach (var entity in colliders)
            {
                entity.enabled = false;
            }
        }
    
        [ContextMenu(DestroyCollidersMethodTitle)]
        private void DestroyColliders()
        {
            var colliders = GetComponentsInChildren<Collider>();

            foreach (var entity in colliders)
            {
                DestroyImmediate(entity);
            }
        }
    }
}