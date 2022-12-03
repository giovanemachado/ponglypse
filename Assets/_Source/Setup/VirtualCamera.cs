using Cinemachine;
using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using RouteTeamStudio.Gameplay.Players;
using UnityEngine;
using UnityEngine.TextCore.Text;

namespace RouteTeamStudio.Setup
{
    public class VirtualCamera : Controller
    {
        [Header("Camera shake effect")]
        [SerializeField] float _duration;
        [SerializeField] float _intensity;
        float _startingIntensity;
        float _shakeTimer;
        float _shakeTimerTotal;

        CinemachineVirtualCamera _virtualCamera;

        public override void OnAwake()
        {
            Paddle.OnHitBall += OnEventShakeCamera;
            Player.OnPlayerHurt += OnEventShakeCamera;

            _virtualCamera = GetComponent<CinemachineVirtualCamera>();
        }

        private void OnDestroy()
        {
            Paddle.OnHitBall -= OnEventShakeCamera;
            Player.OnPlayerHurt -= OnEventShakeCamera;
        }

        public override void OnUpdate()
        {
            if (_shakeTimer > 0)
            {
                _shakeTimer -= Time.deltaTime;

                if (_shakeTimer <= 0f)
                {
                    CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin
                        = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

                    cinemachineBasicMultiChannelPerlin.m_AmplitudeGain =
                        Mathf.Lerp(_startingIntensity, 0f, 1 - (_shakeTimer / _shakeTimerTotal));
                }
            }
        }

        public void Shake()
        {
            CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin
                       = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

            cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = _intensity;

            _startingIntensity = _intensity;
            _shakeTimerTotal = _duration;
            _shakeTimer = _duration;
        }

        void OnEventShakeCamera(Being _)
        {
            Shake();
        }

        void OnEventShakeCamera()
        {
            Shake();
        }
    }
}