using GameCamera;
using GameUI;
using Helpers.Impl;
using Levels;
using PlayerInput;
using Services.Parent.Impl;
using Services.Particles.Service.Impl;
using Services.ScalePieceDown;
using Weapons.Input;
using Zenject;

namespace Main.Installers
{
    public class ServiceInstaller : MonoInstaller
    {
        public CameraMover _cameraMover;
        public LevelManager levelManager;
        public CoroutineService coroutineService;
        public PlayerInputManager inputManager;
        public WeaponInput weaponInput;
        public ParticlesService particlesService;
        public ActionFilter actionFilter;
        public UIManager uiManager;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ParentService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputManager>().FromInstance(inputManager).AsSingle();
            Container.BindInterfacesAndSelfTo<LevelManager>().FromInstance(levelManager).AsSingle();
            Container.BindInterfacesAndSelfTo<CoroutineService>().FromInstance(coroutineService).AsSingle();
            Container.BindInterfacesAndSelfTo<ScalePieceDownService>().AsSingle();
            Container.BindInterfacesAndSelfTo<WeaponInput>().FromInstance(weaponInput).AsSingle();
            Container.BindInterfacesAndSelfTo<CameraMover>().FromInstance(_cameraMover).AsSingle();
            Container.BindInterfacesAndSelfTo<ParticlesService>().FromInstance(particlesService).AsSingle();
            Container.BindInterfacesAndSelfTo<ActionFilter>().FromInstance(actionFilter).AsSingle();
            Container.BindInterfacesAndSelfTo<UIManager>().FromInstance(uiManager).AsSingle();

        }
    }
}