using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    
    
    [SerializeField] private GameObject _enemyPrefab;
    [SerializeField] private GameObject _enemyHolder;
     private bool _stopSpawning = false;
    // Start is called before the first frame update
    void Start()
    {
        
        StartCoroutine(SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    // Spawn enemies every 5 seconds
    // IEnumerator SpawnEnemyRoutine()  
    // use a coroutine to spawn enemies
    IEnumerator SpawnRoutine()
    {
        while (!_stopSpawning)
        {
            SpawnEnemy();
            yield return new WaitForSeconds(5.0f);
        }
       
    }
    public void SpawnEnemy()
    {
        GameObject NewEnemy = Instantiate(_enemyPrefab, new Vector3(Random.Range(-9.36f, 9.36f), 7, 0), Quaternion.identity);
        NewEnemy.transform.parent = _enemyHolder.transform;
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }

}
