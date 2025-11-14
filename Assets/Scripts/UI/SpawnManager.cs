using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    //New Wave Class
    [System.Serializable]
    public class Waves
    {
        public string waveName;
        public int numberOfWaves;
        public GameObject[] gameTypeObject;
        public float spawnInterval;
    }

    // Wave tracker Variables
    [SerializeField] private Waves[] _enemyWaves;
    [SerializeField] private Waves _currentWave;
    [SerializeField] private int _currentWaveNumber;
    private float _nextSpawnTime;
   [SerializeField] private bool _canSpawn = true;

    //Game Object
   // [SerializeField] private GameObject[] _enemyPrefab;
    [SerializeField] private GameObject _enemyHolder;
    [SerializeField] private GameObject _powerupHolder;
    [SerializeField] public Waves[] _powerupsWaves;
    [SerializeField] public Waves _currentPowerUpWave;
    [SerializeField] protected int _currentPowerUpWaveNumber;
    private float _nextPowerupSpawnTime;
    [SerializeField] private bool _canSpawnPowerup = true;

    //public GameObject[] _powerupIDs;
    private float randomX => Random.Range(-8f, 8f); 
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
       _currentWaveNumber = 1;
    }

    // Update is called once per frame
    void Update()
    {
        SpawnTracker();
        SpawnPowerupsTracker();


    }
    // Spawn enemies every 5 seconds
    // IEnumerator SpawnEnemyRoutine()  
    // use a coroutine to spawn enemies
    void SpawnTracker()
    {
        _currentWave = _enemyWaves[_currentWaveNumber];
        GameObject[] totalEnemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (totalEnemies.Length == 0 && !_canSpawn && _currentWaveNumber + 1 != _enemyWaves.Length)
        {
            SpawnNextWave();
        }
    }

    void SpawnPowerupsTracker()
    {
        _currentPowerUpWave = _powerupsWaves[_currentPowerUpWaveNumber];
        GameObject[] totalPowerups = GameObject.FindGameObjectsWithTag("PowerUps");
        if (totalPowerups.Length == 0 && !_canSpawnPowerup && _currentPowerUpWaveNumber + 1 != _powerupsWaves.Length)
        {
            SpawnNextPowerUpWave();
        }
    }


    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(PowerupRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        yield return null;
        while (_stopSpawning == false)
        {
            if (_canSpawn && _nextSpawnTime < Time.time)
            {
                GameObject randomEnemy = _currentWave.gameTypeObject[Random.Range(0, _currentWave.gameTypeObject.Length)];
                Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
                GameObject newEnemy = Instantiate(randomEnemy, posToSpawn, Quaternion.identity);

                newEnemy.transform.parent = _enemyHolder.transform;
                _currentWave.numberOfWaves--;
                _nextSpawnTime = Time.time + _currentWave.spawnInterval;
                if (_currentWave.numberOfWaves == 0)
                {
                    _canSpawn = false;
                }
            }
            yield return new WaitForSeconds(5.0f);
        }
    }
    IEnumerator PowerupRoutine()
    {
        yield return null;
        while (_stopSpawning == false)
        {
            if (_canSpawnPowerup && _nextPowerupSpawnTime < Time.time)
            {
                GameObject randomPowerup = _currentPowerUpWave.gameTypeObject[Random.Range(0, _currentPowerUpWave.gameTypeObject.Length)];
                Vector3 posToSpawn = new Vector3(Random.Range(-8, 8), 7, 0);
                GameObject newPowerup = Instantiate(randomPowerup, posToSpawn, Quaternion.identity);

                newPowerup.transform.parent = _powerupHolder.transform;
                _currentPowerUpWave.numberOfWaves--;
                _nextPowerupSpawnTime = Time.time + _currentPowerUpWave.spawnInterval;
                if (_currentPowerUpWave.numberOfWaves == 0)
                {
                    _canSpawnPowerup = false;
                }
            }
            yield return new WaitForSeconds(5.0f);
        }
    }
    public void SpawnNextWave()
    {
        _currentWaveNumber++;

        _canSpawn=true;
    }

    public void SpawnNextPowerUpWave()
    {
        _currentPowerUpWaveNumber++;
        _canSpawnPowerup = true;
    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }



}
