using Hero;
using UI.Stats;
using UnityEngine;
using Zenject;

namespace Infrastructure.DI
{
    public class LocationInstaller : MonoInstaller
    {
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private Transform _startPlayerPoint;
        [SerializeField] private AttackArea _attackArea;
        [SerializeField] private StatTooltip _statTooltip;
        [SerializeField] private LeftHandAttackButton _leftHandLeftAttackButton;
        [SerializeField] private RightHandAttackButton _rightHandLeftAttackButton;
        public override void InstallBindings()
        {
            /*BindStatTooltip();
            BindAttackArea();
            BindInputService();
            BindRollButton();
            BindLeftHandAttackButton();
            BindRightHandAttackButton();*/
            //BindPlayer();
        }

        private void BindLeftHandAttackButton() => 
            Container.Bind<LeftHandAttackButton>().FromInstance(_leftHandLeftAttackButton).AsSingle();

        private void BindRightHandAttackButton() => 
            Container.Bind<RightHandAttackButton>().FromInstance(_rightHandLeftAttackButton).AsSingle();

        private void BindStatTooltip() => 
            Container.Bind<StatTooltip>().FromInstance(_statTooltip).AsSingle();

        private void BindAttackArea() => 
            Container.Bind<AttackArea>().FromInstance(_attackArea).AsSingle();

        private void BindPlayer()
        {
            Player player = Container
                .InstantiatePrefabForComponent<Player>
                (_playerPrefab,     
                    _startPlayerPoint.position, 
                    Quaternion.identity, null);
            Container
                .Bind<Player>()
                .FromInstance(player)
                .AsSingle();
        }
        

        private void BindInputService()
        {
            /*
        if (Application.isEditor)
            Container.Bind<IInputService>()
                .To<StandaloneInputService>()
                .FromNew()
                .AsSingle();
        else if(Application.platform == RuntimePlatform.Android)
        {
            Container.Bind<IInputService>()
                .To<MobileInputService>()
                .FromNew()
                .AsSingle();
        }
    */
        }
    }
}
