using UnityEngine;

namespace RouteTeamStudio.Gameplay.Beings
{
    [CreateAssetMenu(fileName = "BeingData", menuName = "DataObjects/BeingData")]
    public class BeingData : ScriptableObject
    {
        public int MaxHealthPoints;
    }
}
