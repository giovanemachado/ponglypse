using RouteTeamStudio.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace RouteTeamStudio.Gameplay.Zombies
{
    public class Zombie : Controller
    {
        NavMeshAgent _navMeshAgent;
        GameObject _playerTemp;

        bool _isStarted = false;

        private void Awake()
        {
            // for some reason this just works in the actual awake. might be something with exec order
            _navMeshAgent = GetComponent<NavMeshAgent>();
            SetupAgent(_navMeshAgent);
        }

        public override void OnAwake()
        {
            _playerTemp = GameObject.Find("Player");
        }

        public override void OnUpdate()
        {
            StartProcess();

            _navMeshAgent.SetDestination(_playerTemp.transform.position);
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
    }
}
