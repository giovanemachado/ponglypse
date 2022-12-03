using RouteTeamStudio.Core;
using RouteTeamStudio.Gameplay.Beings;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

        [SerializeField] GameObject _blood;
        [SerializeField] Transform _bloodFolder;

        [SerializeField] TextMeshProUGUI _zombieKillsCount;
        int _zombiesKilled = 0;
        int _bestScore = 0;

        readonly List<Zombie> _zombies = new List<Zombie>();

        public override void OnAwake()
        {
            Being.OnBeingDie += OnBeingDie;
        }

        private void OnDestroy()
        {
            Being.OnBeingDie -= OnBeingDie;
        }

        public override void OnStart()
        {
            SpawnZombies();
            StartCoroutine(SpawnRoutine());

            _bestScore = PlayerPrefs.GetInt("score");
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
            _zombiesKilled++;
            _zombieKillsCount.text = "Zombies killed: " + _zombiesKilled;

            if (_zombiesKilled > _bestScore)
            {
                PlayerPrefs.SetInt("score", _zombiesKilled);
            }

            int zombieToRemoveIndex = _zombies.FindIndex(currentZombie => currentZombie.GetInstanceID() == zombieDead.GetInstanceID());

            GameObject _bloodInst = Instantiate(_blood, _zombies[zombieToRemoveIndex].gameObject.transform);
            _bloodInst.transform.SetParent(_bloodFolder);
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
