using RouteTeamStudio.Core;
using RouteTeamStudio.Utility;
using System;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Balls
{
    public class Ball : Controller
    {
        public static event Action<Ball> OnBallDestroy;
        public static event Action OnBallHit;

        [SerializeField] BallData _ballData;

        Rigidbody2D _rigidbody2D;
        float _lastHitAt;
        int _increasedVelocityTimes = 0;
        bool _isStarted = false;

        public override void OnAwake()
        {
            GameObject player = GameObject.Find("Player");
            _rigidbody2D = GetComponent<Rigidbody2D>();
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
