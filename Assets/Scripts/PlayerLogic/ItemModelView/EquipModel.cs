using System.Collections.Generic;
using UnityEngine;

namespace PlayerLogic.ItemModelView
{
    public class EquipModel : MonoBehaviour
    {
        [SerializeField] private BootsModelChanger _bootsModelChanger; 
        [SerializeField] private HamletModelChanger _hamletModelChanger;
        [SerializeField] private ArmorModelChanger _armorModelChanger;
        
        

        private List<IInventorySlot> _equippedSlots;


        private void Equip(IEquippedItemInfo itemInfo)
        {
            switch (itemInfo.Type)
            {
                case EquippedItemType.Boots:
                    _bootsModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                    break;
                case EquippedItemType.Hamlet:
                    _hamletModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                    break;
                case EquippedItemType.Armor:
                    _armorModelChanger.EquipModelByNames(itemInfo.ModelInfo.Names, itemInfo.ModelInfo.IsNacktModelUnload);
                    break;

            }
        }
        public void UnEquip(IEquippedItemInfo itemInfo)
        {
            EquippedItemType equippedItemType = itemInfo.Type;
            switch (equippedItemType)
            {
                case EquippedItemType.Boots:
                    _bootsModelChanger.EnableDefaultModel();
                    break;
                case EquippedItemType.Hamlet:
                    _hamletModelChanger.EnableDefaultModel();
                    break;
                case EquippedItemType.Armor:
                    _armorModelChanger.EnableDefaultModel();
                    break;
            }
        }


        
    }
}
