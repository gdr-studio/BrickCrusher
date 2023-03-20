using Data;
using Services.Particles.Service.Impl;
using Statues;
using Weapons;
using Zenject;

namespace Main.Installers
{
    public class SettingsInstaller : MonoInstaller
    {
        public MainGameConfig mainConfig;
        public Layers layers;
        public StatueRepository statueRepository;
        public CannonRepository cannonRepository;
        public ParticlesRepository particlesRepository;
        public CannonBallRepository cannonBallRepository;
        
        
        // ReSharper disable Unity.PerformanceAnalysis
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<MainGameConfig>().FromInstance(mainConfig).AsSingle();
            Container.BindInterfacesAndSelfTo<StatueRepository>().FromInstance(statueRepository).AsSingle();
            Container.BindInterfacesAndSelfTo<Layers>().FromInstance(layers).AsSingle();
            Container.BindInterfacesAndSelfTo<ParticlesRepository>().FromInstance(particlesRepository).AsSingle();
            Container.BindInterfacesAndSelfTo<CannonRepository>().FromInstance(cannonRepository).AsSingle();
            Container.BindInterfacesAndSelfTo<CannonBallRepository>().FromInstance(cannonBallRepository).AsSingle();
            
        }

    }
}