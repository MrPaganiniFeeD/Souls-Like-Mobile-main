using Infrastructure.Services.Inventory;
using Inventory.Item.EquippedItem.Weapon;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class AttackButtonUI : MonoBehaviour
{
    private WeaponSlot _weaponSlot;

    [SerializeField] private Button _leftHandButton;
    [SerializeField] private Button _rightHandButton;
    [SerializeField] private Button _mainAttackButton;
    
    
    [Inject]
    public void Construct(IInventoryService inventoryService)
    {
        _weaponSlot = inventoryService.InventoryEquipped.WeaponSlot;
        _weaponSlot.WeaponEquipped += OnWeaponEquipped;
        _weaponSlot.WeaponUnequipped += OnWeaponUnequipped;
    }

    public void Start()
    {
        if (_weaponSlot.TwoHandWeaponItem == null)
            DisableOrEnableLeftRightButton(true);
        else
            _mainAttackButton.gameObject.SetActive(true);
    }

    public void OnDestroy()
    {
        _weaponSlot.WeaponEquipped -= OnWeaponEquipped;
        _weaponSlot.WeaponUnequipped -= OnWeaponUnequipped;
    }

    private void DisableOrEnableLeftRightButton(bool value)
    {
        _leftHandButton.gameObject.SetActive(value);
        _rightHandButton.gameObject.SetActive(value);
    }

    private void OnWeaponEquipped(WeaponItem weapon, LocationWeaponInHandType type)
    {
        switch (type)
        {
            case LocationWeaponInHandType.LeftHand:
                _mainAttackButton.gameObject.SetActive(false);
                DisableOrEnableLeftRightButton(true);
                break;
            case LocationWeaponInHandType.RightHand:
                _mainAttackButton.gameObject.SetActive(false);
                DisableOrEnableLeftRightButton(true);
                break;
            case LocationWeaponInHandType.TwoHand:
                _mainAttackButton.gameObject.SetActive(true);
                DisableOrEnableLeftRightButton(false);
                break;
        }
    }

    private void OnWeaponUnequipped(WeaponItem arg1, LocationWeaponInHandType arg2)
    {
        
    }
}
