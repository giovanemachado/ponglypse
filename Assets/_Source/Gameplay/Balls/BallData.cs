using UnityEngine;

namespace RouteTeamStudio.Gameplay.Balls
{
    [CreateAssetMenu(fileName = "BallData", menuName = "Balls/BallData")]
    public class BallData : ScriptableObject
    {
        public float AutoDestroyTime;
    }
}
