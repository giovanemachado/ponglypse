using RouteTeamStudio.Core;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Zombies
{
    public class ZombieAnimatorController : AnimatorController
    {
        public int WalkAnim { get; private set; } = Animator.StringToHash("ZombieWalk");
        public int AttackAnim { get; private set; } = Animator.StringToHash("ZombieAttack");
        public int HurtAnim { get; private set; } = Animator.StringToHash("ZombieHurt");

        ZombieData _zombieData;

        public void OnAwake(ZombieData zombieData)
        {
            base.OnAwake();

            _zombieData = zombieData;
        }

        public override void ChangeAnimation(int animation)
        {
            if (Time.time < _animationLocked)
            {
                return;
            }

            if (animation == HurtAnim)
            {
                _animationAsked = LockAnimation(animation, _zombieData.HurtAnimDuration);
                return;
            }

            if (animation == AttackAnim)
            {
                _animationAsked = LockAnimation(animation, _zombieData.AttackAnimDuration);
                return;
            }

            _animationAsked = WalkAnim;
        }
    }
}