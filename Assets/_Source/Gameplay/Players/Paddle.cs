using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Balls;
using RouteTeamStudio.Utility;
using System;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    public class Paddle : Controller
    {
        public static event Action OnHitBall;
        PlayerData _playerData;
        
        PlayerAnimatorController _playerAnimatorController;

        float _lastHitAt;
        float _lastSpawnBallAt;
        int _ballLayer;

        bool _thereIsBall;
        Ball _currentBall;

        public override void OnAwake(ScriptableObject dataObject)
        {
            _playerAnimatorController = GetComponentInChildren<PlayerAnimatorController>();
            Ball.OnBallDestroy += OnBallDestroy;
            _playerData = (PlayerData) dataObject;
            _ballLayer = LayerMask.GetMask("Ball");
        }

        public override void OnUpdate()
        {
            if (_currentBall)
            {
                _currentBall.OnUpdate();
            }
        }

        public void HitBall()
        {
            if (SpawnNewBallIfDestroyed())
            {
                return;
            }

            if (CheckHitBallCooldown())
            {
                return;
            }

            Rigidbody2D ballRigidbody2d = CheckForBallHit();

            if (ballRigidbody2d == null)
            {
                return;
            }

            ThrowBall(ballRigidbody2d);
        }

        public void SpawnNewBall()
        {
            if (CheckSpawnBallCooldown())
            {
                return;
            }

            if (_currentBall)
            {
                Destroy(_currentBall.gameObject);
            }

            _thereIsBall = false;
            SpawnNewBallIfDestroyed();
        }

        bool SpawnNewBallIfDestroyed()
        {
            if (_thereIsBall)
            {
                return false;
            }

            _currentBall = Instantiate(_playerData.BallPrefab, _playerData.BallSpawn).GetComponent<Ball>();
            _currentBall.transform.SetParent(_playerData.BallsFolder);

            _thereIsBall = true;

            return true;
        }

        void OnBallDestroy(Ball ball)
        {
            _thereIsBall = false;
        }

        bool CheckHitBallCooldown()
        {
            if (Timers.CheckInCooldown(_lastHitAt, _playerData.HitPaddleCooldown))
            {
                return true;
            }

            _playerAnimatorController.ChangeAnimation(_playerAnimatorController.AttackAnim);
            _lastHitAt = Time.time;

            return false;
        }

        bool CheckSpawnBallCooldown()
        {
            if (Timers.CheckInCooldown(_lastSpawnBallAt, _playerData.SpawnBallCooldown))
            {
                return true;
            }

            _lastSpawnBallAt = Time.time;

            return false;
        }

        Rigidbody2D CheckForBallHit()
        {
            Collider2D ballColliderHit = Physics2D.OverlapCircle(transform.position, _playerData.HitPaddleRange, _ballLayer);
           
            if (!ballColliderHit)
            {
                return null;
            }

            _currentBall.Hit();

            return ballColliderHit.gameObject.GetComponent<Rigidbody2D>();
        }

        Vector3 GetMouseDirectionNormalized()
        {
            Vector3 screenPoint = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 direction = (Vector3)(Input.mousePosition - screenPoint);
            direction.Normalize();

            return direction;
        }

        void ThrowBall(Rigidbody2D ballRigidbody2d)
        {
            OnHitBall?.Invoke();
            ballRigidbody2d.velocity = Vector2.zero;
            ballRigidbody2d.AddForce(GetMouseDirectionNormalized() * _playerData.HitStrength, ForceMode2D.Force);
        }
    }
}
