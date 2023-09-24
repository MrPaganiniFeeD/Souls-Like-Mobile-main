using System.Collections.Generic;
using Infrastructure.Services.PersistentProgress;
using TMPro;
using UnityEngine.UIElements;
using Zenject;

namespace Infrastructure.Services
{
    public class AllServices
    {
        public List<ISavedProgress> SavedProgressesServices => _savedProgressesServices;

        private readonly List<IUpdateableService> _updateableServices;
        private readonly DiContainer _diContainer;
        private readonly List<ISavedProgress> _savedProgressesServices;


        public AllServices(DiContainer diContainer)
        {
            _diContainer = diContainer;
            _updateableServices = new List<IUpdateableService>();
            _savedProgressesServices = new List<ISavedProgress>();
        }


        public FromServiceBindGeneric<TService> RegisterSingle<TService>() where TService : IService
        {
            return new FromServiceBindGeneric<TService>(_diContainer, this);
        }

        public TService Single<TService>() where TService : IService => 
            _diContainer.Resolve<TService>();

        private static class Implementation<TService> where TService : IService
        {
            public static TService ServiceInstance;
        }

        public void AddImplementation<TConcrete>(TConcrete implementation) 
        {
            if(implementation is IUpdateableService updateableService)
                _updateableServices.Add(updateableService);
            if (implementation is ISavedProgress savedProgressService)
                _savedProgressesServices.Add(savedProgressService);
        }
        public void Update()
        {
            foreach (IUpdateableService service in _updateableServices) 
                service.Update();
        }
        
    }

    public class FromServiceBindGeneric<TContract>
    {
        private readonly DiContainer _diContainer;
        private readonly AllServices _allServices;

        public FromServiceBindGeneric(DiContainer diContainer, AllServices allServices)
        {
            _diContainer = diContainer;
            _allServices = allServices;
        }

        public new FromServiceBindGeneric<TConcrete> To<TConcrete>(TConcrete implementation)
            where TConcrete : TContract
        {
            _diContainer.Bind<TContract>().To<TConcrete>().FromInstance(implementation).AsSingle();
            _allServices.AddImplementation(implementation);
            return new FromServiceBindGeneric<TConcrete>(_diContainer, _allServices);
        }
    }
    
}