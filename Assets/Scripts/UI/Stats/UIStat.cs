using PlayerLogic.Stats;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace UI.Stats
{
    public class UIStat : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] public TMP_Text Name;
        [SerializeField] public TMP_Text Value;
        [SerializeField] private StatTooltip _statTooltip;
    
        private Stat _playerStat;

        public void SetStat(Stat playerStat)
        {
            _playerStat = playerStat;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _statTooltip.Show(_playerStat, Name);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _statTooltip.Hide();
        }
    }
}
