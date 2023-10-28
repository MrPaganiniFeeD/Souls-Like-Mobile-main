using Infrastructure.Services.Inventory;
using Inventory.InventoryWithSlots.Equipped;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Zenject;


public class TooltipItem : Window
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private Button _equipButton;
    [SerializeField] private Button _unequipButton;
    [FormerlySerializedAs("itemStatPanel")] [SerializeField] private ItemStatPanel _itemStatPanel;
    private InventoryEquipped _inventoryEquipped;
    private Item _currentItem;
    private AudioSource _audioSource;

    [Inject]
    public void Construct(IInventoryService inventoryService)
    {
        _inventoryEquipped = inventoryService.InventoryEquipped;
        _audioSource = GetComponent<AudioSource>();
    }
    
    public void UpdateInfo(Item item)
    {
        _currentItem = item;
        var itemInfo = item.Info;
        _icon.sprite = itemInfo.Icon;
        _name.text = itemInfo.Name;
        //_discription.text = itemInfo.Discription;
        
        if (itemInfo is IEquippedItemInfo equippedItemInfo)
        {
            var buffStats = equippedItemInfo.ItemBuffStats;
            _itemStatPanel.UpdateStats(buffStats);
        }
        if (item.State.IsEquipped) 
            EnableButton(_unequipButton.gameObject);
        else
            EnableButton(_equipButton.gameObject);
        
        OpenDisplay();
    }

    public override void Close()
    {
        base.Close();
        Clear();
    }

    public void EquipItem()
    {
        if (_currentItem is EquippedItem equippedItem)
            if (_inventoryEquipped.TryEquipped(equippedItem))
            {
                _audioSource.Play();
                EnableButton(_unequipButton.gameObject);
                DisableButton(_equipButton.gameObject);
            }
    }

    public void UnequipItem()
    {
        if (_currentItem is EquippedItem equippedItem)
            if (_inventoryEquipped.TryUnequip(equippedItem))
            {
                _audioSource.Play();
                DisableButton(_unequipButton.gameObject);
                EnableButton(_equipButton.gameObject);
            }
    }
    

    private void EnableButton(GameObject gameObject) =>
        gameObject.SetActive(true);
    private void DisableButton(GameObject gameObject) =>
        gameObject.SetActive(false);
    private void OpenDisplay() => 
        gameObject.SetActive(true);

    private void Clear()
    {
        _name.text = "";
        //_discription.text = "";
        _icon.sprite = null;
        _currentItem = null;
        _itemStatPanel.Clear();
    }
}
