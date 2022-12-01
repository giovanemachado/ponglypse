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

        [SerializeField] BallData _ballData;

        bool _isStarted = false;
        float _lastHitAt;

        public override void OnStart()
        {
            _lastHitAt = Time.time;
            GameObject player = GameObject.Find("Player");
            Physics2D.IgnoreCollision(player.GetComponent<Collider2D>(), GetComponent<Collider2D>());
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
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            Hit();
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
