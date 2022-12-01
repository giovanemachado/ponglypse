using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace RouteTeamStudio.Gameplay.Zombies
{
    public class ZombiesManager : Controller
    {
        [SerializeField] float _spawnTiming;
        [SerializeField] Transform[] _zombieSpawns;
        [SerializeField] Transform _zombiesFolder;
        [SerializeField] GameObject _zombiePrefab;

        [SerializeField] int _minQuantityToSpawn;
        [SerializeField] int _maxQuantityToSpawn;

        readonly List<Zombie> _zombies = new List<Zombie>();

        public override void OnAwake()
        {
            Being.OnBeingDie += OnBeingDie;
        }

        public override void OnStart()
        {
            SpawnZombies();
            StartCoroutine(SpawnRoutine());
        }

        public override void OnUpdate()
        {
            foreach (Zombie zombie in _zombies.ToArray())
            {
                zombie.OnUpdate();
            }
        }

        void OnBeingDie(Being being)
        {
            Zombie zombieDead = being.GetComponent<Zombie>();

            if (!zombieDead)
            {
                return;
            }

            int zombieToRemoveIndex = _zombies.FindIndex(currentZombie => currentZombie.GetInstanceID() == zombieDead.GetInstanceID());
            Destroy(_zombies[zombieToRemoveIndex].gameObject);
            _zombies.RemoveAt(zombieToRemoveIndex);
        }

        IEnumerator SpawnRoutine()
        {
            yield return new WaitForSeconds(_spawnTiming);
            SpawnZombies();
            StartCoroutine(SpawnRoutine());
        }

        void SpawnZombies()
        {
            Transform _zombieSpawn = _zombieSpawns[Random.Range(0, _zombieSpawns.Length)];
            int _zombiesQuantity = Random.Range(_minQuantityToSpawn, _maxQuantityToSpawn);

            for (int i = 0; i < _zombiesQuantity; i++)
            {
                GameObject zombie = Instantiate(_zombiePrefab, _zombieSpawn.transform);
                Vector3 randomNearPosition = Random.insideUnitSphere * 2f + _zombieSpawn.transform.position;
                randomNearPosition.z = 0;
                zombie.transform.SetPositionAndRotation(randomNearPosition, Quaternion.identity);
                zombie.transform.SetParent(_zombiesFolder);

                _zombies.Add(zombie.GetComponent<Zombie>());
            }
        }
    }
}
