using System;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;

namespace DefaultNamespace.ModelData
{
    [Serializable]
    public class ModelInfo : IModelInfo
    {
        [SerializeField] private bool _isNacktModelUnload = false;
        [SerializeField] private string[] _modelsName;
        [SerializeField] private LocationWeaponInHandType _modelInHand;

        public bool IsNacktModelUnload => _isNacktModelUnload;
        public string[] Names => _modelsName;
        public LocationWeaponInHandType ModelInHand => _modelInHand;
    }
}