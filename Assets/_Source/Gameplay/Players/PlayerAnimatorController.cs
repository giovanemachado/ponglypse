using RouteTeamStudio.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace RouteTeamStudio.Gameplay.Players
{
    public class PlayerAnimatorController : AnimatorController
    {
        public int IdleAnim { get; private set; } = Animator.StringToHash("PlayerIdle");
        public int WalkAnim { get; private set; } = Animator.StringToHash("PlayerWalk");
        public int AttackAnim { get; private set; } = Animator.StringToHash("PlayerAttack");
        public int HurtAnim { get; private set; } = Animator.StringToHash("PlayerHurt");

        PlayerData _playerData;
        Rigidbody2D _rigidbody;

        public void OnAwake(PlayerData playerData, Rigidbody2D rig)
        {
            base.OnAwake();

            _playerData = playerData;
            _rigidbody = rig;
        }

        public override void ChangeAnimation(int animation)
        {
            if (Time.time < _animationLocked)
            {
                return;
            }

            if (animation == HurtAnim)
            {
                _animationAsked = LockAnimation(animation, _playerData.HurtAnimDuration);
                return;
            }

            if (animation == AttackAnim)
            {
                _animationAsked = LockAnimation(animation, _playerData.AttackAnimDuration);
                return;
            }

            _animationAsked = _rigidbody.velocity != Vector2.zero ? WalkAnim : IdleAnim;
        }
    }
}