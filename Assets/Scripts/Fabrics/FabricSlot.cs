using System.Collections.Generic;
using PlayerLogic.Stats;

namespace Fabrics
{
    public class FabricSlot : IFabricSlot
    {
        public WeaponSlot<WeaponItem> WeaponSlot => _weaponSlot;
        
        private readonly PlayerStats _playerStats;
        private readonly IItemDataBaseService _itemDataBaseService;
        
        private readonly WeaponItem _leftUnarmed;
        private readonly WeaponItem _rightUnarmed;
        private readonly WeaponItem _twoHandUnarmed;
        
        private WeaponSlot<WeaponItem> _weaponSlot;


        public FabricSlot(PlayerStats playerStats,
            IItemDataBaseService itemDataBaseService)
        {
            _playerStats = playerStats;
            _itemDataBaseService = itemDataBaseService;
        }


        public IInventorySlot CreateInventorySlot() => 
            new InventorySlot();

        public IInventorySlot CreateInventorySlot(Item item) =>
            new InventorySlot(item);

        public List<IEquippedSlot<EquippedItem>> CreateEquippedSlot()
        {
            return new List<IEquippedSlot<EquippedItem>>
            {
                new EquippedSlot<ArmorItem>(EquippedItemType.Armor),
                new EquippedSlot<BootsItem>(EquippedItemType.Boots),
                new EquippedSlot<HamletItem>(EquippedItemType.Hamlet),
                new EquippedSlot<RingItem>(EquippedItemType.Ring),
                GetWeaponSlot()
            };
        }

        private WeaponSlot<WeaponItem> GetWeaponSlot()
        {
            _weaponSlot = new WeaponSlot<WeaponItem>(_playerStats,
                GetWeapon(0),
                GetWeapon(1),
                GetWeapon(2)).SetFistWeapon();
     
            
            return _weaponSlot;
        }

        private WeaponItem GetWeapon(int id)
        {
            var itemInfo = _itemDataBaseService.GetItem(id);
            WeaponInfo weaponInfo = itemInfo as WeaponInfo;
            return weaponInfo.GetCreationItem();
        }
    }

    public class RingItem : EquippedItem
    {
        public RingItem(IEquippedItemInfo info, IItemState itemState = null) : base(info, itemState)
        {
        }
    }

    public class HamletItem : EquippedItem
    {
        public HamletItem(IEquippedItemInfo info, IItemState itemState = null) : base(info, itemState)
        {
        }
    }

    public class BootsItem : EquippedItem
    {
        public BootsItem(IEquippedItemInfo info, IItemState itemState = null) : base(info, itemState)
        {
        }
    }

    public class ArmorItem : EquippedItem
    {
        public ArmorItem(IEquippedItemInfo info, IItemState itemState = null) : base(info, itemState)
        {
        }
    }
    
}