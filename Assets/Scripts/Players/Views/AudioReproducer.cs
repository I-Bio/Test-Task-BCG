using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class AudioReproducer
    {
        private readonly Dictionary<EffectType, AudioSource> _sounds;

        public AudioReproducer(Dictionary<EffectType, AudioSource> sounds)
        {
            _sounds = sounds;
        }

        public void Play(EffectType effect)
        {
            _sounds[effect].Play();
        }
    }
}