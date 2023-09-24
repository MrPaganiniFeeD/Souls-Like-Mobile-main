using PlayerLogic.Stats;
using UnityEngine;
using Zenject;

namespace UI.Stats
{
    public class UIRenderStats : MonoBehaviour
    {
        [SerializeField] private UIStatPanel _statPanel;

        [Inject] private PlayerStats _playerStats;
    
        private void Start()
        {
            InitPlayerStats();
        }

        private void InitPlayerStats()
        {
        
        }

    }
}
