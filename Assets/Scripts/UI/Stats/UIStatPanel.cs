using Infrastructure.Services.PersistentProgress;
using Hero.Stats;
using UnityEngine;
using Zenject;

namespace UI.Stats
{
    public class UIStatPanel : MonoBehaviour
    {
        [SerializeField] private UIStat[] _uiStat;
        [SerializeField] private string[] _statNames;
        private PlayerStats _playerStats;
        
        
        [Inject]
        public void Construct(IPersistentProgressService progressService)
        {
            _playerStats = progressService.PlayerProgress.playerStateData.PlayerStats;
        }
    
        private Stat[] _stats;

        private void Awake()
        {
            //_uiStat = GetComponentsInChildren<UIStat>();
            //UpdateStatName();
        }

        private void Start()
        {
            SetStats(_playerStats.Health,
                _playerStats.Damage,
                _playerStats.Stamina,
                _playerStats.Mana,
                _playerStats.Intelligence,
                _playerStats.Protection);
        }

        public void SetStats(params Stat[] playerStats)
        {
            _stats = playerStats;
            for (int i = 0; i < _stats.Length; i++)
            {
                //UpdateStatValue();
                //_stats[i].StateChanged += UpdateStatValue;
                _uiStat[i].SetStat(_stats[i]);
            }
        
            if(_stats.Length > _uiStat.Length)
            {
                Debug.Log("Not Enough Stat display");
                return;
            }
            /*for (int i = 0; i < _uiStat.Length; i++)
            {
                _uiStat[i].gameObject.SetActive(i < _stats.Length);
            }*/
        }
        private void UpdateStatValue()
        {
            for (int i = 0; i < _stats.Length; i++) 
                _uiStat[i].Value.text = _stats[i].Value.ToString();
        }

        private void UpdateStatName()
        {
            for (int i = 0; i < _statNames.Length; i++) 
                _uiStat[i].Name.text = _statNames[i];
        }
        private void OnDestroy()
        {
            foreach (var stat in _stats) 
                stat.StateChanged -= UpdateStatValue;
        }
    }
}
