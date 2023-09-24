using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UIDiscriptionItem : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _discription;
    [FormerlySerializedAs("_itemStatPanel")] [SerializeField] private ItemStatPanel itemStatPanel;
    
    public void RenderItemInfo(IItemInfo itemInfo)
    {
        _icon.sprite = itemInfo.Icon;
        _name.text = itemInfo.Name;
        _discription.text = itemInfo.Discription;
        
        OpenDisplay();
    }
    private void OpenDisplay()
    {
        gameObject.SetActive(true);
    }
    
}
