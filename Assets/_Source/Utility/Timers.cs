using System.Collections;
using UnityEngine;

namespace RouteTeamStudio.Utility
{
    public static class Timers
    {
        public static bool CheckInCooldown(float lastTime, float cooldown)
        {
            return Time.time < lastTime + cooldown;
        }
    }
}