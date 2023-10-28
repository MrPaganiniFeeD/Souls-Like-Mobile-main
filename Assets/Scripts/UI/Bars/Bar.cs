using Infrastructure.Services.PersistentProgress;
using Hero.Stats;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Bars
{
    public abstract class Bar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        protected Stat Stat;

        [Inject]
        public abstract void Constructor(IPersistentProgressService progressService);
        
        protected void SetValue()
        {
            _slider.value = Stat.Value;
            _slider.maxValue = Stat.MaxValue;
        }

        protected void UpdateValue()
        {
            _slider.value = Stat.Value;
        }
    }
}