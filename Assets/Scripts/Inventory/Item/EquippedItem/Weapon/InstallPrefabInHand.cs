using Infrastructure;
using UnityEngine;
using Zenject;
public class InstallPrefabInHand : MonoBehaviour
{
    [SerializeField] private Transform _parentOverride;
    protected GameObject CurrentWeaponModel;

    public virtual GameObject LoadWeaponModel(WeaponItem weapon)
    {
        DestroyWeapon();

        if (weapon == null)
        {
            Debug.LogError("Weapons Dont have prefabs");
            return null;
        }
        
        var model = Instantiate(weapon.Info.Prefab);
        SetPosition(model);

        weapon.SetSpawnedPrefab(model);
        CurrentWeaponModel = model;
        return CurrentWeaponModel;
    }

    public void LoadLastEnableWeaponModel() => 
        CurrentWeaponModel.gameObject.SetActive(true);

    public void UnLoadWeaponModel()
    {
        DestroyWeapon();
    }

    public void SetPosition(GameObject model)
    {
        model.transform.parent = _parentOverride;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
    }

    public void SetWeaponModel(GameObject model) => 
        CurrentWeaponModel = model;

    public void DestroyWeapon()            
    {
        if (CurrentWeaponModel != null)
            Destroy(CurrentWeaponModel);
    }
}

