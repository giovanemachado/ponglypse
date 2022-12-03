using Cinemachine;
using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Balls;
using RouteTeamStudio.Gameplay.Beings;
using RouteTeamStudio.Gameplay.Players;
using RouteTeamStudio.Gameplay.Zombies;
using System;
using UnityEngine;
using UnityEngine.TextCore.Text;
using Random = UnityEngine.Random;

namespace RouteTeamStudio.Setup
{
    public class Audio : Controller
    {
        [Header("Zombie")]
        [SerializeField] AudioClip[] _zumbiHurtSounds;

        [Header("Ball")]
        [SerializeField] AudioClip[] _ballHitSounds;

        AudioSource _audioSource;

        public override void OnAwake()
        {
            Ball.OnBallHit += OnBallHit;
            Paddle.OnHitBall += OnHitBall;
            Zombie.OnZombieHurt += OnZombieHurt;

            _audioSource = GetComponent<AudioSource>();
        }

        void OnBallHit()
        {
            _audioSource.volume = 0.7f;
            _audioSource.PlayOneShot(GetRandomSound(_ballHitSounds));
        }

        void OnZombieHurt()
        {
            _audioSource.volume = 1f;
            _audioSource.PlayOneShot(GetRandomSound(_zumbiHurtSounds));
        }

        void OnHitBall()
        {
            _audioSource.volume = 1f;
            _audioSource.PlayOneShot(GetRandomSound(_ballHitSounds));
        }

        AudioClip GetRandomSound(AudioClip[] sounds)
        {
            return sounds[Random.Range(0, sounds.Length)];
        }
    }
}