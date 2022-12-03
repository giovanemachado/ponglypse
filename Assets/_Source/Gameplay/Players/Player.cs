using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using System;
using Unity.VisualScripting;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    public class Player : Controller
    {
        public static event Action OnPlayerHurt;
        [SerializeField] PlayerData _playerData;
        [SerializeField] BarsManager _barsManager;

        // Composition
        Paddle _paddle; // should refactor
        Movement _movement; // should refactor
        Being _being;
        PlayerAnimatorController _playerAnimatorController;

        bool _barReady = false;

        public override void OnAwake()
        {
            _playerData.BallSpawn = GameObject.Find("BallSpawn").transform; 
            _playerData.BallsFolder = GameObject.Find("Balls").transform;

            _being = GetComponent<Being>();
            _playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();

            _paddle = GetComponent<Paddle>();
            _movement = gameObject.GetOrAddComponent<Movement>();

            _paddle.OnAwake(_playerData);
            _movement.OnAwake(_playerData);

            _playerAnimatorController.OnAwake(_playerData, GetComponent<Rigidbody2D>());
        }

        public override void OnUpdate()
        {
            _being.OnUpdate();

            if (!_barReady)
            {
                _barsManager.SetMaxValue(_barsManager.HealthSlider, _being.CurrentHP);
                _barReady = true;
            }

            _playerAnimatorController.OnUpdate();

            if (!_being.IsAlive())
            {
                return;
            }

            _paddle.OnUpdate();

            CheckHitBall();
            CheckSpawnNewBall();
        }

        public void Damage(int damageAmount)
        {
            _being.Damage(damageAmount);
            _barsManager.UpdateValue(_barsManager.HealthSlider, _being.CurrentHP);
            OnPlayerHurt?.Invoke();
            _playerAnimatorController.ChangeAnimation(_playerAnimatorController.HurtAnim);
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

        void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _playerData.HitPaddleRange);
        }
    }
}
