using UnityEngine;
using Zenject;
public class InstallPrefabInHand : MonoBehaviour
{
    [SerializeField] private Transform _parentOverride;

    [Inject] private DiContainer _diContainer;
    private GameObject _currentWeaponModel;

    public GameObject LoadWeaponModel(WeaponItem weapon)
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
        _currentWeaponModel = model;
        return _currentWeaponModel;
    }

    public GameObject LoadWeaponModelNotInstantiate(GameObject weapon)
    {
        DestroyWeapon();

        if (weapon == null)
        {
            Debug.LogError("Weapons Dont have prefabs");
            return null;
        }
        SetPosition(weapon);
        
        _currentWeaponModel = weapon;
        return _currentWeaponModel;
    }

    public void LoadLastEnableWeaponModel() => 
        _currentWeaponModel.gameObject.SetActive(true);

    public void UnLoadWeaponModel()
    {
        if (_currentWeaponModel != null)
            _currentWeaponModel.gameObject.SetActive(false);
    }

    private void SetPosition(GameObject model)
    {
        model.transform.parent = _parentOverride;
        model.transform.localPosition = Vector3.zero;
        model.transform.localRotation = Quaternion.identity;
    }

    public void DestroyWeapon()            
    {
        if (_currentWeaponModel != null)
            Destroy(_currentWeaponModel);
    }
}

