using Infrastructure.Services.PersistentProgress;
using PlayerLogic.Stats;
using UnityEngine;
using Zenject;

namespace UI.Bars
{
    public class HealthBar : Bar
    {
        [Inject]
        public override void Constructor(IPersistentProgressService progressService)
        {
            Stat = progressService.PlayerProgress.playerStateData.PlayerStats.Health;
            SetValue();
            Stat.StateChanged += UpdateValue;
        }
    }
}
