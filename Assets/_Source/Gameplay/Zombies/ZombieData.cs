using UnityEngine;

namespace RouteTeamStudio.Gameplay.Zombies
{
    [CreateAssetMenu(fileName = "ZombieData", menuName = "Zombies/ZombieData")]
    public class ZombieData : ScriptableObject
    {
        public float AttackRange;
        public int AttackDamage;
        public float AttackCooldown;
        public float MinRandomSpeed;
        public float MaxRandomSpeed;

        [Header("Animations")]
        public float HurtAnimDuration;
        public float AttackAnimDuration;

    }
}
