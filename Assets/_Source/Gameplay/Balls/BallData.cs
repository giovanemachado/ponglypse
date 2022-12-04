using UnityEngine;

namespace RouteTeamStudio.Gameplay.Balls
{
    [CreateAssetMenu(fileName = "BallData", menuName = "DataObjects/BallData")]
    public class BallData : ScriptableObject
    {
        public float AutoDestroyTime;
    }
}
