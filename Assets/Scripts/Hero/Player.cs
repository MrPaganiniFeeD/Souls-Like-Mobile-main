using Hero.Stats;
using UnityEngine;

namespace Hero
{
    public class Player : MonoBehaviour
    {
        private PlayerStats _playerStats;
        private Transform _selfTransform;
        private ClassType _classType;
        private IStatsProvider _statsProvider;


        public void Construct(PlayerStats playerStats)
        {
            _playerStats = playerStats;
        }

        private void Awake()
        {
            _selfTransform = GetComponent<Transform>();
        }

        private void Start()
        {
            /*
            _playerStats = InitClassType(ClassType.Warrior).GetStats();
            _playerStats.ShowInfoStats();
        */
        }

        private IStatsProvider InitClassType(ClassType classType)
        {
            _classType = classType;
            return _statsProvider = new ClassStats(_classType);
        }
        
    }
}