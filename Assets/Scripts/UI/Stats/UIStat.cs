using System;
using Hero.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Stats
{
    public class UIStat : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] public TMP_Text Name;
        [SerializeField] public TMP_Text Value;
        [SerializeField] private StatTooltip _statTooltip;
        [SerializeField] private Slider _slider;
    
        private Stat _playerStat;

        public void SetStat(Stat playerStat)
        {
            _playerStat = playerStat;
            _playerStat.StateChanged += UpdateValue;
            UpdateValue();
        }

        public void OnPointerDown(PointerEventData eventData) => 
            _statTooltip.Show(_playerStat, Name);

        public void OnPointerUp(PointerEventData eventData) => 
            _statTooltip.Hide();

        private void UpdateValue()
        {
            int maxValue = _playerStat.MaxValue;
            if (maxValue == int.MaxValue || maxValue == 0)
                maxValue = _playerStat.Value;
            _slider.value = _playerStat.Value / maxValue;
        }

        public void OnDestroy() => 
            _playerStat.StateChanged -= UpdateValue;
    }
}
