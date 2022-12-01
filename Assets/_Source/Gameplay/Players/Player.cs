using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
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

        // Composition
        Paddle _paddle; // should refactor
        Movement _movement; // should refactor
        Being _being;

        public override void OnAwake()
        {
            _playerData.BallSpawn = GameObject.Find("BallSpawn").transform; 
            _playerData.BallsFolder = GameObject.Find("Balls").transform;

            _being = GetComponent<Being>();

            _paddle = gameObject.GetOrAddComponent<Paddle>();
            _movement = gameObject.GetOrAddComponent<Movement>();

            _paddle.OnAwake(_playerData);
            _movement.OnAwake(_playerData);
        }

        public override void OnUpdate()
        {
            _being.OnUpdate();

            if (!_being.IsAlive())
            {
                return;
            }

            _paddle.OnUpdate();

            UpdateMovement();
            CheckHitBall();
        }

        public void Damage(int damageAmount)
        {
            _being.Damage(damageAmount);
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
