using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField] public GameObject lazerPrefab;
    [SerializeField]public GameObject _tripleShotPrefab;
    [SerializeField] public GameObject playerShield;
    [SerializeField] private float _fireRate = 0.1f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    [SerializeField] private Material _mat;
    private SpawnManager _spawnManager;
    private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive;
    private bool _isShieldActive;
    [SerializeField]
    private int _score;
    private UI_Manager _uiManager;
    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _isShieldActive = false;
        _isTripleShotActive = false;
        _isSpeedBoostActive = false;
        transform.position = new Vector3(0, -5, 0);
        if (_spawnManager == null)
        {
            Debug.LogError("Spawn Manager Is Null");
        }
        _uiManager = GameObject.Find("Canvas").GetComponent<UI_Manager>();
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
        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y,-3.12f,0), 0);
        transform.Translate(direction * speed * Time.deltaTime);
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
        if (_isTripleShotActive == true)
        {
            tripleshot();
        }
        else
        {
            Instantiate(lazerPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }
                   
    }
    void tripleshot()
    {
        Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.5f, 1.05f, 0), Quaternion.identity);
       
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            _isShieldActive = false;
            return;
           
        }
        


        _lives--;
         
       _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            
            
            Debug.Log("Player Destroyed");
            Destroy(this.gameObject);
            _spawnManager.OnPlayerDeath();
        }
    }

    public void Scoreup(int points)
    {

        _score += points;
       _uiManager.UpdateScore(_score);
    }







    //powerup methods

    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;

        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void ActivateSpeedBoost()
    {
        _isSpeedBoostActive = true;
        if (_isSpeedBoostActive == true)
        {
            speed *= 4;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        

    }
    public void ActivateShield()
    {
        _isShieldActive = true;
        //change material color to blue
        playerShield.SetActive(true);
        StartCoroutine(ShieldPowerDownRoutine());
    }


    //powerdown coroutines

    IEnumerator TripleShotPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isTripleShotActive = false;
    }
    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        speed /= 4;
        _isSpeedBoostActive = false;
       
        
    }
    IEnumerator ShieldPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _isShieldActive = false;
      
        playerShield.SetActive(false);
    }


}