using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Infrastructure.DI
{
    public class DiContainerInstaller : MonoInstaller
    {
        [Inject] private DiContainer _diContainer;

        public override void InstallBindings()
        {
            DiContainerSceneRef.Container = _diContainer;
        }
    }

    public static class DiContainerSceneRef
    {
        public static DiContainer Container
        {
            get => _container;

            set => _container = value;
        }

        private static DiContainer _container;
    }
}