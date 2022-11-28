using RouteTeamStudio.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Zombies
{
    public class ZombiesManager : Controller
    {
        [SerializeField] Zombie[] zombies;

        public override void OnUpdate()
        {
            foreach (Zombie zombie in zombies)
            {
                zombie.OnUpdate();
            }
        }
    }
}
