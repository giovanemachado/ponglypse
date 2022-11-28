using RouteTeamStudio.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RouteTeamStudio.Gameplay.Balls
{
    public class Ball : Controller
    {
        public static event Action<Ball> OnBallDestroy;

        [SerializeField] BallData _ballData;

        bool _isStarted = false;

        public override void OnAwake()
        {
            StartCoroutine(AutoDestroy());
        }

        public override void OnUpdate()
        {
            StartProcess();
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

        IEnumerator AutoDestroy()
        {
            yield return new WaitForSeconds(_ballData.AutoDestroyTime);
            OnBallDestroy?.Invoke(this);
            Destroy(gameObject);
        }
    }
}
