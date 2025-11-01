using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] public GameObject lazerPrefab;
   [SerializeField] private float _fireRate = 0.1f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField] private Material _mat;
    private SpawnManager _spawnManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
        transform.position = new Vector3(0, -5, 0);
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager Is Null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();
       

        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
        {
            LazerShoot();
        }
      
    }

    void CalculateMovement()
    {
        // get input from keyboard
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        // calculate direction based on input
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        // move the player
        transform.Translate(direction * speed * Time.deltaTime);


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.12f,0), 0);

        if (transform.position.x <= -9.36)
        {
            transform.position = new Vector3(9.36f, transform.position.y, 0);
        }
        else if (transform.position.x >= 9.36)
        {
            transform.position = new Vector3(-9.36f, transform.position.y, 0);

        }
    }

    void LazerShoot()
    {
        
        
            _canFire = Time.time + _fireRate;
           
            Instantiate(lazerPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);

        
    }

    public void Damage()
    {
        _lives--;
         
       
        if (_lives < 1)
        {
            
            
            Debug.Log("Player Destroyed");
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

   

}