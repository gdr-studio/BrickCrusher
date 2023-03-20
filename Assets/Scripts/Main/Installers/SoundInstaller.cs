using Game.Sound.Data;
using Game.Sound.Impl;
using Zenject;

namespace Main.Installers
{
    public class SoundInstaller : MonoInstaller
    {
        public SoundManager manager;
        public AudioSourceProvider sources;
        public GlobalSoundSettings settings;
        public SoundRepository repo;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SoundManager>().FromInstance(manager).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSourceProvider>().FromInstance(sources).AsSingle();
            Container.BindInterfacesAndSelfTo<GlobalSoundSettings>().FromInstance(settings).AsSingle();
            Container.BindInterfacesAndSelfTo<SoundRepository>().FromInstance(repo).AsSingle();

        }
    }
}