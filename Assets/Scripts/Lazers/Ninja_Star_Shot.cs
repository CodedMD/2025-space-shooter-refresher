using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja_Star_Shot : MonoBehaviour
{

    private float _rotationSpeed = 20f;
    private float _speed = 3f;

    [SerializeField]
    private  GameObject[] _ninjaStarPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.up * _speed * Time.deltaTime);
        //transform.Rotate(Vector3.forward * _rotationSpeed * 50 * Time.deltaTime);
        StartCoroutine(LazerFireRoutine());
    }

    IEnumerator LazerFireRoutine()
    {
        yield return new WaitForSeconds(1f);
        transform.Rotate(Vector3.forward * _rotationSpeed * 50 * Time.deltaTime);
        yield return new WaitForSeconds(2.0f);
        int randomIndex = Random.Range(0,4);
       
        if (_ninjaStarPrefab[randomIndex] != null)
        {
            _ninjaStarPrefab[randomIndex].SetActive(true);
        }
        
        yield return new WaitForSeconds(5.0f);
    }
}
