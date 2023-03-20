using Data;
using Services.Particles.Service;
using UnityEngine;
using Zenject;

namespace Money
{
    public class MoneyDropTrigger : MonoBehaviour
    {
        [SerializeField] private ParticleEffectName _moneyParticles;
        [Inject] private IParticlesService _particles;
        [Inject] private MoneyDropPool _moneyDropPool;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(Tags.BrokenBlock))
            {
                var block = other.gameObject.GetComponent<IBrokenBlock>();
                var drop = _moneyDropPool.GetItem();
                drop.amount = block.GetMoneyAndHide();
                drop.DropOut(other.transform.position);
               // var pos = other.transform.position;
               // _particles.PlayParticles(new PlayParticlesArg().Name(_moneyParticles).Duration(1f).IsTimed(true).Position(pos));
            }
        }
        
        
    }
}