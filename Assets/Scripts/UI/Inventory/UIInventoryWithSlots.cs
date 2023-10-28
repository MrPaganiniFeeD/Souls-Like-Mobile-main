using System.Collections.Generic;
using Infrastructure.AssetsManagement;
using Infrastructure.Factory;
using Infrastructure.Services.Inventory;
using UnityEngine;
using Zenject;

namespace UI.Inventory
{
    public class UIInventoryWithSlots : MonoBehaviour, IUIInventory
    {
        [SerializeField] private TooltipItem _tooltipItem;
        
        public InventoryWithSlots Inventory { get; private set; }
        public UIRenderInventory RenderInventory { get; private set; }
        
        private List<UIInventorySlot> _uiSlots;
        private IGameFactory _gameFactory;

        [Inject]
        public void Constructor(IInventoryService inventoryService, IGameFactory gameFactory)
        {
            Inventory = inventoryService.InventoryWithSlots;
            _gameFactory = gameFactory;
            _uiSlots = new List<UIInventorySlot>();
        }
        private void Start()
        {
            /*_uiSlots = GetComponentsInChildren<UISlot>();
            RenderInventory = new UIRenderInventory(Inventory, _uiSlots);*/

            CreateStartingUISlots();
        }

        private void CreateStartingUISlots()
        {
            Debug.Log("Invenotry Slots "  + Inventory.Slots.Count);
            foreach (var slot in Inventory.Slots)
            {
                var uiSlot = CreateSlot();
                uiSlot.Construct(slot, _tooltipItem);
                uiSlot.Refresh();
                _uiSlots.Add(uiSlot);
            }
        }

        private UIInventorySlot CreateSlot()
        {
            var uiSlot = _gameFactory.InstantiateRegister(AssetsPath.UISlot,
                Quaternion.identity, Vector3.zero, transform)
                .GetComponent<UIInventorySlot>();
            return uiSlot;
        }
    }
}
