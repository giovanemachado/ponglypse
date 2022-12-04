using UnityEngine;

namespace RouteTeamStudio.Gameplay.Zombies
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "DataObjects/ZombieData")]
    public class ZombieData : ScriptableObject
    {
        [Header("Attack")]
        public float AttackRange;
        public int AttackDamage;
        public float AttackCooldown;

        [Header("Speed")]
        public float MinRandomSpeed;
        public float MaxRandomSpeed;

        [Header("Animations")]
        public float HurtAnimDuration;
        public float AttackAnimDuration;
    }
}
