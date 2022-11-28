using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Balls;
using RouteTeamStudio.Utility;
using UnityEngine;

namespace RouteTeamStudio.Gameplay.Players
{
    public class Paddle : Controller
    {
        PlayerData _playerData;

        float _lastHitAt;
        int _ballLayer;

        bool _thereIsBall;
        Ball _currentBall;

        public override void OnAwake(ScriptableObject dataObject)
        {
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

            if (CheckInCooldown())
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

        bool CheckInCooldown()
        {
            if (Timers.CheckInCooldown(_lastHitAt, _playerData.HitPaddleCooldown))
            {
                return true;
            }

            _lastHitAt = Time.time;
            return false;
        }

        Rigidbody2D CheckForBallHit()
        {
            Collider2D ballColliderHit = Physics2D.OverlapCircle(transform.position, _playerData.HitPaddleRange, _ballLayer);
           
            if (!ballColliderHit)
            {
                Debug.Log("no hit");
                return null;
            }

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
            ballRigidbody2d.velocity = Vector2.zero;
            ballRigidbody2d.AddForce(GetMouseDirectionNormalized() * _playerData.HitStrength, ForceMode2D.Force);
        }
    }
}
