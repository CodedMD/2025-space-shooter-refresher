using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

   
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyHolder;
    [SerializeField] private GameObject _powerupHolder;
    [SerializeField]public GameObject[] _powerups;
   
    //public GameObject[] _powerupIDs;
    private float randomX => Random.Range(-8f, 8f); 
    private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
       
    }
    // Spawn enemies every 5 seconds
    // IEnumerator SpawnEnemyRoutine()  
    // use a coroutine to spawn enemies

    public void StartSpawning()
    {
        StartCoroutine(SpawnRoutine());
    }
    IEnumerator SpawnRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (!_stopSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(5.0f);
            Spawnpowerups();
            yield return new WaitForSeconds(1.0f);
        }
       
    }
    public void SpawnEnemy()
    {
        
        GameObject NewEnemy = Instantiate(_enemyPrefab, new Vector3(randomX, 7, 0), Quaternion.identity);
        NewEnemy.transform.parent = _enemyHolder.transform;
    }

    public void Spawnpowerups()
    {
        int randomPowerup = Random.Range(0,3);
        GameObject NewPowerups = Instantiate(_powerups[randomPowerup], new Vector3(randomX, 7, 0), Quaternion.identity);
        NewPowerups.transform.parent = _powerupHolder.transform;
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
