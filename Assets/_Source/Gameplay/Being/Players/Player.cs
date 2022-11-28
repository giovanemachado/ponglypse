using RouteTeamStudio.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.RestService;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    public class Player : Controller
    {
        [SerializeField] PlayerData _playerData;

        Paddle _paddle;
        Movement _movement;

        public override void OnAwake()
        {
            _playerData.BallSpawn = GameObject.Find("BallSpawn").transform; 
            _playerData.BallsFolder = GameObject.Find("Balls").transform; 

            _paddle = gameObject.GetOrAddComponent<Paddle>();
            _movement = gameObject.GetOrAddComponent<Movement>();

            _paddle.OnAwake(_playerData);
            _movement.OnAwake(_playerData);
        }

        public override void OnUpdate()
        {
            _paddle.OnUpdate();

            UpdateMovement();
            CheckHitBall();
        }

        void CheckHitBall()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _paddle.HitBall();
            }
        }

        void UpdateMovement()
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");

            _movement.Move(horizontal, vertical);
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _playerData.HitPaddleRange);
        }
    }
}
