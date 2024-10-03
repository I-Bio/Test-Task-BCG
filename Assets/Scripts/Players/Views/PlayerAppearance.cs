using System.Collections.Generic;
using UnityEngine;

namespace Players
{
    public class PlayerAppearance
    {
        private readonly Dictionary<Stage, Mesh> _skins;
        private readonly SkinnedMeshRenderer _renderer;
        private readonly Animator _animator;

        public PlayerAppearance(Dictionary<Stage, Mesh> skins, SkinnedMeshRenderer renderer, Animator animator)
        {
            _skins = skins;
            _renderer = renderer;
            _animator = animator;
        }

        public void Change(Stage target)
        {
            _renderer.sharedMesh = _skins[target];
        }

        public void Play(PlayerAnimations animation)
        {
            _animator.SetTrigger(animation.ToString());
        }
    }
}