using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    [CreateAssetMenu(fileName = "PlayerData", menuName = "Players/PlayerData")]
    public class PlayerData : ScriptableObject
    {
        [Header("Player")]
        public float Speed;

        [Header("Paddle")]
        public float HitPaddleCooldown;
        public float HitPaddleRange;
        public float HitStrength;

        [Header("Ball Spawn")]
        public float SpawnBallCooldown;
        public GameObject BallPrefab;
        [HideInInspector] public Transform BallSpawn;
        [HideInInspector] public Transform BallsFolder;

        [Header("Animations")]
        public float HurtAnimDuration;
        public float AttackAnimDuration;
    }
}
