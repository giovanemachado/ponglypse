using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using System;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    [RequireComponent(typeof(Being), typeof(Paddle), typeof(Movement))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Player : Controller
    {
        public static event Action OnPlayerHurt;

        [SerializeField] PlayerData _playerData;

        [Header("Ball")]
        [SerializeField] Transform _ballSpawn;
        [SerializeField] Transform _ballFolder;

        [SerializeField] BarsManager _barsManager;

        // Composition
        Being _being;
        Paddle _paddle;
        Movement _movement;
        Rigidbody2D _rigidbody2D;

        PlayerAnimatorController _playerAnimatorController;

        bool _barReady = false;

        public override void OnAwake()
        {
            _playerData.BallSpawn = _ballSpawn; 
            _playerData.BallsFolder = _ballFolder;

            _being = GetComponent<Being>();
            _paddle = GetComponent<Paddle>();
            _movement = GetComponent<Movement>();
            _rigidbody2D = GetComponent<Rigidbody2D>();

            _playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();

            _being.OnAwake();
            _paddle.OnAwake(_playerData);
            _movement.OnAwake(_playerData);
            _playerAnimatorController.OnAwake(_playerData, _rigidbody2D);
        }

        public override void OnUpdate()
        {
            _being.OnUpdate();

            if (!_barReady)
            {
                _barsManager.SetMaxValue(_barsManager.HealthSlider, _being.CurrentHP);
                _barReady = true;
            }

            if (!_being.IsAlive())
            {
                return;
            }

            _paddle.OnUpdate();
            _movement.OnUpdate();
            _playerAnimatorController.OnUpdate();

            CheckHitBall();
            CheckSpawnNewBall();
        }

        public void Damage(int damageAmount)
        {
            _being.Damage(damageAmount);
            _barsManager.UpdateValue(_barsManager.HealthSlider, _being.CurrentHP);
            _playerAnimatorController.ChangeAnimation(_playerAnimatorController.HurtAnim);
            OnPlayerHurt?.Invoke();
        }

        void CheckHitBall()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _paddle.HitBall();
            }
        }
        void CheckSpawnNewBall()
        {
            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                _paddle.SpawnNewBall();
            }
        }
    }
}
