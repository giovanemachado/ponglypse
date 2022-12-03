using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Balls;
using RouteTeamStudio.Gameplay.Beings;
using RouteTeamStudio.Gameplay.Players;
using RouteTeamStudio.Utility;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.AI;
using Color = UnityEngine.Color;
using Random = UnityEngine.Random;

namespace RouteTeamStudio.Gameplay.Zombies
{
    public class Zombie : Controller
    {
        public static event Action OnZombieHurt;

        [SerializeField] ZombieData _zombieData;
        [SerializeField] SpriteRenderer[] spritesToPaint;

        Player _player;
        float _lastAttackAt;
        bool _isStarted = false;

        // Composition
        Being _being;
        NavMeshAgent _navMeshAgent;
        ZombieAnimatorController _zombieAnimatorController;

        private void Awake()
        {
            // for some reason this just works in the actual awake. might be something with exec order
            _navMeshAgent = GetComponent<NavMeshAgent>();
            SetupAgent(_navMeshAgent);
        }

        public override void OnAwake()
        {
            _being = GetComponent<Being>();
            _player = GameObject.Find("Player").GetComponent<Player>();
            _zombieAnimatorController = GetComponentInChildren<ZombieAnimatorController>();
            _zombieAnimatorController.OnAwake(_zombieData);
            _navMeshAgent.speed = Random.Range(_zombieData.MinRandomSpeed, _zombieData.MaxRandomSpeed);
            PaintZombie();
        }

        public override void OnUpdate()
        {
            StartProcess();
            _being.OnUpdate();
            _zombieAnimatorController.OnUpdate();

            if (!_being.IsAlive())
            {
                return;
            }

          
            if (Vector2.Distance(_player.transform.position, transform.position) > _zombieData.AttackRange)
            {
                EnterAndUpdatePursuitState();
                return;
            }

            if (CheckAttackCooldown())
            {
                return;
            }

            _player.Damage(_zombieData.AttackDamage);
        }

        bool CheckAttackCooldown()
        {
            if (Timers.CheckInCooldown(_lastAttackAt, _zombieData.AttackCooldown))
            {
                return true;
            }

            _zombieAnimatorController.ChangeAnimation(_zombieAnimatorController.AttackAnim);

            _lastAttackAt = Time.time;

            return false;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Ball"))
            {
                OnZombieHurt?.Invoke();
                _zombieAnimatorController.ChangeAnimation(_zombieAnimatorController.HurtAnim);
                _being.Damage(1);
            }
        }

        void SetupAgent(NavMeshAgent navMeshAgent)
        {
            navMeshAgent.updateRotation = false;
            navMeshAgent.updateUpAxis = false;
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

        void EnterAndUpdatePursuitState()
        {
            _navMeshAgent.SetDestination(_player.transform.position);
        }

        void PaintZombie()
        {
            Color randomColor = Random.ColorHSV(0f, 1f, 1f, 1f, 0.5f, 1f);

            foreach (SpriteRenderer sprite in spritesToPaint)
            {
                sprite.color = randomColor;
            }
        }

        void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, _zombieData.AttackRange);
        }
    }
}
