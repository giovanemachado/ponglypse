using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Players;
using RouteTeamStudio.Gameplay.Zombies;
using UnityEngine;

namespace RouteTeamStudio.Commanders
{
    public class GameScene : Commander
    {
        [Header("Controllers")]
        [SerializeField] Player _player;
        [SerializeField] ZombiesManager _zombiesManager;

        protected override void Awake()
        {
            base.Awake();
            ExecuteControllerMethod(_player, Method.AWAKE);
            ExecuteControllerMethod(_zombiesManager, Method.AWAKE);
        }

        protected void Start()
        {
            ExecuteControllerMethod(_player, Method.START);
            ExecuteControllerMethod(_zombiesManager, Method.START);
        }

        protected void Update()
        {
            ExecuteControllerMethod(_player);
            ExecuteControllerMethod(_zombiesManager);
        }
    }
}
