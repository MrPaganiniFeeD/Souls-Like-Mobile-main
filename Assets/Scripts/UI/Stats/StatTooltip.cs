using System.Text;
using TMPro;
using UnityEngine;

namespace UI.Stats
{
    public class StatTooltip : MonoBehaviour
    {
        [SerializeField] private TMP_Text _statName;
        [SerializeField] private TMP_Text _statModifiers;

        private StringBuilder _stringBuilder = new StringBuilder();

        public void Show(PlayerLogic.Stats.Stat stat, TMP_Text statName)
        {
            _statName.text = GetStatTopText(stat, statName);
            _statName.color = statName.color;
            _statModifiers.text = GetStatModifiersText(stat);
        
            gameObject.SetActive(true);
        }
        public void Hide()
        {
            gameObject.SetActive(false);
        }
        private string GetStatTopText(PlayerLogic.Stats.Stat playerStat, TMP_Text statName)
        {
            _stringBuilder.Length = 0;
            _stringBuilder.Append(statName.text);
            _stringBuilder.Append(" ");
            _stringBuilder.Append(playerStat.Value);

            return _stringBuilder.ToString();
        }
        private string GetStatModifiersText(PlayerLogic.Stats.Stat stat)
        {
            _stringBuilder.Length = 0;

            foreach (StatModifier statModifier in stat.StatModifiers)
            {
                if (_stringBuilder.Length > 0)
                    _stringBuilder.AppendLine();
            
                SetColor(statModifier.Value);

                if (statModifier.Value > 0)
                    _stringBuilder.Append("+");


                if (statModifier.Type == StatModifierType.Flat)
                {
                    _stringBuilder.Append(statModifier.Value);
                }
                else
                {
                    _stringBuilder.Append(statModifier.Value * 100);
                    _stringBuilder.Append("%");
                }

 
                IItemInfo itemInfo = statModifier.Source as IItemInfo;
                if (itemInfo != null)
                {
                    _stringBuilder.Append(" ");
                    _stringBuilder.Append(itemInfo.Name);
                }
                else
                {
                    Debug.LogError("Modifier is not an Item!");
                }
            }

            return _stringBuilder.ToString();
        }

        private void SetColor(float value)
        {
            _statModifiers.color = value > 0 ? Color.green : Color.red;
        }
    }
}
