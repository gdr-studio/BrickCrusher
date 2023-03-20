using ImageToVolume;
using Money;
using Statues.Cracking;
using UnityEngine;
using Zenject;

namespace Main.Installers
{
    public class PoolsInstaller : MonoInstaller
    {
        [SerializeField] private CrackTexturePool _crackTexturePool;
        [SerializeField] private ElementSubPool _elementSubPool;
        [SerializeField] private MoneyDropPool _moneyDropPool;

        public override void InstallBindings()
        {
            Container.Inject(_crackTexturePool);
            Container.Inject(_elementSubPool);
            Container.Inject(_moneyDropPool);
            
            Container.Bind<CrackTexturePool>().FromInstance(_crackTexturePool).AsSingle();
            Container.Bind<ElementSubPool>().FromInstance(_elementSubPool).AsSingle();
            Container.Bind<MoneyDropPool>().FromInstance(_moneyDropPool).AsSingle();

            _crackTexturePool.Spawn();
            _elementSubPool.Spawn();
            _moneyDropPool.Spawn();
        }
    }
}