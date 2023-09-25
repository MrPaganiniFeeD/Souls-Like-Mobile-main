using Infrastructure.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace UI.Bars
{
    public class StaminaBar : Bar
    {
        [Inject]
        public override void Constructor(IPersistentProgressService progressService)
        {
            Stat = progressService.PlayerProgress.playerStateData.PlayerStats.Stamina;
            SetValue();
            Stat.StateChanged += UpdateValue;
        }
        private void OnDestroy() => 
            Stat.StateChanged -= UpdateValue;
    }
}
