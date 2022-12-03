using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Players;
using RouteTeamStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEditor.Experimental.GraphView.GraphView;

namespace RouteTeamStudio.Gameplay.Balls
{
    public class Ball : Controller
    {
        public static event Action<Ball> OnBallDestroy;
        public static event Action OnBallHit;

        [SerializeField] BallData _ballData;

        bool _isStarted = false;
        float _lastHitAt;
        Rigidbody2D _rigidbody2D;
        int _increasedVelocityTimes = 0;

        public override void OnAwake()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            GameObject player = GameObject.Find("Player");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
        }

        public override void OnStart()
        {
            _lastHitAt = Time.time;
        }

        public override void OnUpdate()
        {
            StartProcess();
            
            if (!CheckInCooldown())
            {
                OnBallDestroy?.Invoke(this);
                Destroy(gameObject);
            }
        }

        public void Hit()
        {
            _lastHitAt = Time.time;
            OnBallHit?.Invoke();
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Hit();
            
            if (_increasedVelocityTimes < 5)
            {
                if (!_rigidbody2D)
                {
                    return;
                }

                _rigidbody2D.velocity += Vector2.one / 10;
                _increasedVelocityTimes++;
            }
        }

        void StartProcess()
        {
            if (_isStarted)
            {
                return;
            }

            OnAwake();
            OnStart();

            _isStarted = true;
        }

       
        bool CheckInCooldown()
        {
            if (Timers.CheckInCooldown(_lastHitAt, _ballData.AutoDestroyTime))
            {
                return true;
            }
            
            _lastHitAt = Time.time;
            return false;
        }
    }
}
