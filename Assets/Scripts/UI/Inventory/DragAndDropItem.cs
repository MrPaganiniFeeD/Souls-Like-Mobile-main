using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Inventory
{
    [RequireComponent(typeof(UIItem))]
    public class DragAndDropItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private Transform _parentOverride;

        private CanvasGroup _canvasGroup;
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvasGroup = GetComponent<CanvasGroup>();
        }
        public void OnBeginDrag(PointerEventData eventData)
        {
            Transform slotTransform = _rectTransform.parent;
            Transform inventoryTransform = _parentOverride;
            slotTransform.SetAsLastSibling();
            inventoryTransform.SetAsLastSibling();

        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = Input.mousePosition;
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            _rectTransform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
        }
    }
}
