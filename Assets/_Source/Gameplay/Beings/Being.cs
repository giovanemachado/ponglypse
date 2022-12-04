using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Balls;
using RouteTeamStudio.Gameplay.Players;
using System;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Beings
{
    public class Being : Controller
    {
        public static event Action<Being> OnBeingDie;

        [SerializeField] BeingData _beingData;

        public int CurrentHP => _currentHP;
        int _currentHP;

        bool _isStarted = false;

        public override void OnStart()
        {
            _currentHP = _beingData.MaxHealthPoints;
        }

        public override void OnUpdate()
        {
            StartProcess();
            CheckIfAlive();
        }

        public void Damage(int damageAmount)
        {
            _currentHP -= damageAmount;
        }

        public bool IsAlive()
        {
            return _currentHP > 0;
        }

        void CheckIfAlive()
        {
            if (_currentHP <= 0)
            {
                OnBeingDie?.Invoke(this);
            }
        }

        void StartProcess()
        {
            if (!_isStarted)
            {
                OnAwake();
                OnStart();

                _isStarted = true;
            }
        }
    }
}
