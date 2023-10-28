using UnityEngine;

namespace Hero.Weapon
{
    public class InstallWeaponInPlayerModel : InstallPrefabInHand
    {
        public override GameObject LoadWeaponModel(WeaponItem weapon)
        {
            DestroyWeapon();
            
            var weaponModel = Instantiate(weapon.Info.Prefab);
            SetPosition(weaponModel);
            SetUILayerMask(weaponModel);
            SetScale(weaponModel);
            CurrentWeaponModel = weaponModel;
            return CurrentWeaponModel;
        }

        private void SetScale(GameObject weaponModel)
        {
            weaponModel.transform.localScale = new Vector3(1, 1, 1);
        }

        private void SetUILayerMask(GameObject weaponModel)
        {
            var transforms = weaponModel.GetComponentsInChildren<Transform>();
            foreach (var transform in transforms)
            {
                transform.gameObject.layer = LayerMask.NameToLayer("UI");
            }
        }
    }
}