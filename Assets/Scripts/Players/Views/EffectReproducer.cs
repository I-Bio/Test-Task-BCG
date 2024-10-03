using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class EffectReproducer
    {
        private readonly Dictionary<EffectType, ParticleSystem> _effects;

        public EffectReproducer(Dictionary<EffectType, ParticleSystem> effects)
        {
            _effects = effects;
        }

        public void Play(EffectType effect)
        {
            _effects[effect].Play();
        }
    }
}