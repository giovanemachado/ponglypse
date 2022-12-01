using UnityEngine;

namespace RouteTeamStudio.Gameplay.Beings
{
    [CreateAssetMenu(fileName = "BeingData", menuName = "Beings/BeingData")]
    public class BeingData : ScriptableObject
    {
        public int MaxHealthPoints;
    }
}
