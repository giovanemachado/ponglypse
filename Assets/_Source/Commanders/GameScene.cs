using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using RouteTeamStudio.Gameplay.Players;
using RouteTeamStudio.Gameplay.Zombies;
using System;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.SceneManagement;

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

            Being.OnBeingDie += OnBeingDie;

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

        private void OnBeingDie(Being being)
        {
            Player playerDead = being.GetComponent<Player>();

            if (!playerDead)
            {
                return;
            }
            
            SceneManager.LoadScene("MenuScene");
        }
    }
}
