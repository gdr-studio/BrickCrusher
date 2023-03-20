using UnityEngine;

namespace Services.Particles.Service
{
    public interface IParticlesRepository
    {
        ParticleSystem GetPrefab(ParticleEffectName name);
    }
}