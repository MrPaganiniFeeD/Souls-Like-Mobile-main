using System;
using Infrastructure.Services.Inventory;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;
using Zenject;

namespace Hero.Weapon
{
    public class WeaponPrefabInstaller : MonoBehaviour, IWeaponPrefabInstaller
    {
        [SerializeField] private InstallPrefabInHand _leftHand;
        [SerializeField] private InstallPrefabInHand _rightHand;
        [SerializeField] private InstallPrefabInHand _dualHand;

        private WeaponSlot _weaponSlot;

        [Inject]
        public void Construct(IInventoryService inventoryService)
        {
            _weaponSlot = inventoryService.InventoryEquipped.WeaponSlot;
            _weaponSlot.WeaponEquipped += OnWeaponEquip;
            _weaponSlot.WeaponUnequipped += OnWeaponUnequip;
        }

        public void Start() => 
            UpdateModel();

        public void LoadLastModel()
        {
            _leftHand.LoadLastEnableWeaponModel();
            _rightHand.LoadLastEnableWeaponModel();
        }
        public void UnloadAllModel()
        {
            _leftHand.UnLoadWeaponModel();
            _rightHand.UnLoadWeaponModel();
        }
        private void UpdateModel()
        {
            UpdateHand(_weaponSlot.LeftHandWeapon, LocationWeaponInHandType.LeftHand);
            UpdateHand(_weaponSlot.RightHandWeapon, LocationWeaponInHandType.RightHand);
            UpdateHand(_weaponSlot.TwoHandWeaponItem, LocationWeaponInHandType.TwoHand);
        }

        private void UpdateHand(WeaponItem item, LocationWeaponInHandType locationWeaponInHandType)
        {
            if (item == null) return;
            switch (locationWeaponInHandType)
            {
                case LocationWeaponInHandType.LeftHand:
                    LoadWeaponOnLeftHand(item);
                    break;
                case LocationWeaponInHandType.RightHand:
                    LoadWeaponOnRightHand(item);
                    break;
                case LocationWeaponInHandType.TwoHand:
                    LoadTwoHandWeapon(item);
                    break;
            }
        } 
        

        private void OnWeaponUnequip(WeaponEventInfo eventInfo)
        {
            var location = eventInfo.LocationWeaponInHandType;
            switch (location)
            {
                case LocationWeaponInHandType.LeftHand:
                    _leftHand.UnLoadWeaponModel();
                    break;
                case LocationWeaponInHandType.RightHand:
                    _rightHand.UnLoadWeaponModel();
                    break;
                case LocationWeaponInHandType.TwoHand:
                    _leftHand.UnLoadWeaponModel();
                    _rightHand.UnLoadWeaponModel();
                    break;
            }
        }

        private void OnWeaponEquip(WeaponEventInfo eventInfo)
        {
            var weapon = eventInfo.WeaponItem;
            var location = eventInfo.LocationWeaponInHandType;
            var isUnarmed = eventInfo.IsUnarmed;
            switch (location)
            {
                case LocationWeaponInHandType.LeftHand:
                    LoadWeaponOnLeftHand(weapon);
                    break;
                case LocationWeaponInHandType.RightHand:
                    LoadWeaponOnRightHand(weapon);
                    break;
                case LocationWeaponInHandType.TwoHand:
                    LoadTwoHandWeapon(weapon);
                    break;
            }
        }

        private void LoadTwoHandWeapon(WeaponItem weapon)
        {
            _leftHand.UnLoadWeaponModel();
            _rightHand.UnLoadWeaponModel();
            
            switch (weapon.Info.ModelInfo.ModelInHand)
            {
                case LocationWeaponInHandType.LeftHand:
                    LoadWeaponOnLeftHand(weapon);
                    break;
                case LocationWeaponInHandType.RightHand:
                    LoadWeaponOnRightHand(weapon);
                    break;
                case LocationWeaponInHandType.LeftAndRightHand:
                    var installedPrefab = _dualHand.LoadWeaponModel(weapon);
                    
                    LeftHandWeaponModel leftHandWeaponModel = 
                        installedPrefab.GetComponentInChildren<LeftHandWeaponModel>();
                    RightHandWeaponModel rightHandWeaponModel =
                        installedPrefab.GetComponentInChildren<RightHandWeaponModel>();

                    _leftHand.SetPosition(leftHandWeaponModel.gameObject);
                    _rightHand.SetPosition(rightHandWeaponModel.gameObject);
                    _leftHand.SetWeaponModel(leftHandWeaponModel.gameObject);
                    _rightHand.SetWeaponModel(rightHandWeaponModel.gameObject);
                    /*weaponModel.SetColliders(new[]
                    {
                        leftHand.GetComponentInChildren<Collider>(),
                        rightHand.GetComponentInChildren<Collider>()
                    });
                    weaponModel.SetColliderDetections(new []
                    {
                        leftHand.GetComponentInChildren<ColliderDetection>(),
                        rightHand.GetComponentInChildren<ColliderDetection>()
                    });*/
                    break;
            }
        }

        private void LoadWeaponOnLeftHand(WeaponItem weapon)
        {
            _leftHand.UnLoadWeaponModel();
            _leftHand.LoadWeaponModel(weapon);
        }

        private void LoadWeaponOnRightHand(WeaponItem weapon)
        {
            _rightHand.UnLoadWeaponModel();
            _rightHand.LoadWeaponModel(weapon);
        }

        public void OnDisable()
        {
            _weaponSlot.WeaponEquipped -= OnWeaponEquip;
            _weaponSlot.WeaponUnequipped -= OnWeaponUnequip;           
        }
    }

    public interface IWeaponPrefabInstaller
    {
    }
}