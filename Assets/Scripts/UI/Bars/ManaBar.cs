using Infrastructure.Services.PersistentProgress;
using Hero.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Bars
{
    public class ManaBar : Bar
    {
        [Inject]
        public override void Constructor(IPersistentProgressService progressService)
        {
            Stat = progressService.PlayerProgress.playerStateData.PlayerStats.Mana;
            SetValue();
            Stat.StateChanged += UpdateValue;
        }
    }
}
