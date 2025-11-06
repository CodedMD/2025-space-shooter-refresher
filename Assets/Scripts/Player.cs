using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 5f;
    [SerializeField]
    private GameObject _rightHurt;
    [SerializeField]
    private GameObject _leftHurt;
    [SerializeField] public GameObject lazerPrefab;
    [SerializeField]public GameObject _tripleShotPrefab;
    // Shield Variables
    [SerializeField] public GameObject playerShield;
    [SerializeField] private GameObject _playerHurtShield;
    private bool _isShieldActive = false;
    private bool _ishurtShieldActive = false;
    [SerializeField]
    private int _shieldLives = 3;



    [SerializeField] private GameObject _thrusterBoost;
    [SerializeField] private float _fireRate = 0.1f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
 

    [SerializeField] private Material _mat;
    private SpawnManager _spawnManager;
     private bool _isTripleShotActive = false;
    private bool _isSpeedBoostActive;

    [SerializeField]
    private int _score;
    private UI_Manager _uiManager;
    [SerializeField]
    private AudioSource _audioSource;

    // Lazer Variables
    [SerializeField]
    private int _ammo = 15;
    [SerializeField]
    private AudioClip _lazerAudio;
    [SerializeField]
    private AudioClip _noAmmoAudio;
    [SerializeField]
    private AudioClip _explosionAudio;
    [SerializeField]
    private AudioClip _powerUpClip;

    // Thruster power variables
    [SerializeField]
    private float _thrusterBarPrecentage;
    private bool _isThrusterBoostActive = false;
    private bool _thrusterRecover = false;
    private bool _isThrusterBaseActive = true;
   



    // Start is called before the first frame update
    void Start()
    {
        _thrusterBarPrecentage = 100f;
        //_shieldLives = 3;
        _ammo = 15;
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
       _rightHurt.SetActive(false);
        _leftHurt.SetActive(false);
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
        if (_isShieldActive == true)
        {

        }

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

        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (_thrusterRecover == false)
            {
                ThrusterBoostActive();
                speed = 9.5f;
            }
            else if (_thrusterRecover == true)
            {
                ThrusterBaseActive();
                speed = 3.5f;
            }
            if (_thrusterBarPrecentage <= 0f)
            {
                _thrusterBarPrecentage = 0;
                ThrusterBaseActive();
          
            }
           // Debug.LogError("Thruster initiated");
            StopCoroutine(ThrusterRecoverRoutine());
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            ThrusterBaseActive();
        }
        else if (_thrusterBarPrecentage <= 0)
        {
            StartCoroutine(ThrusterRecoverRoutine());
            ThrusterBaseActive();
            if(_thrusterBarPrecentage >= 100f)
            {
                //_thrusterBarPrecentage = 100f;
                StopCoroutine(ThrusterRecoverRoutine());
            }
        }
        
       
    }
   
    
    
    void ThrusterBaseActive()
    {
        if (_isThrusterBaseActive == true)
        {
            speed = 3.5f;
           // _thrusterBase.gameObject.SetActive(true);
            _thrusterBoost.gameObject.SetActive(false);
        }
    }
    void ThrusterBoostActive()
    {
        if (_thrusterBarPrecentage > 1 || _thrusterBarPrecentage < 100)
        {
            _thrusterRecover = false;
            _isThrusterBoostActive = true;
            _thrusterBoost.gameObject.SetActive(true);
            _thrusterBarPrecentage -= 10.0f * 5 * Time.deltaTime;
            //Debug.LogError("Thruster Active");

        }
        _uiManager.UpdateThrusterBar(_thrusterBarPrecentage);
    }

    void LazerShoot()
    {
        _ammo = Mathf.Clamp(_ammo,0, 15);
        _ammo--;
       _uiManager.UpdateAmmo(_ammo);
        if (_ammo >=0)
        {

            _canFire = Time.time + _fireRate;
            if (_isTripleShotActive == true)
            {
                tripleshot();
            }
            else
            {
                _audioSource.PlayOneShot(_lazerAudio);
                Instantiate(lazerPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
            }
        }
        else if (_ammo <= 0)
        {
            Debug.Log("Out of Ammo");
            _ammo = 0;
            _audioSource.PlayOneShot(_noAmmoAudio);


        }
    }
    void tripleshot()
    {
        _audioSource.PlayOneShot(_lazerAudio);
        Instantiate(_tripleShotPrefab, transform.position + new Vector3(-0.5f, 1.05f, 0), Quaternion.identity);
       
    }

    public void Damage()
    {
        
        if (_isShieldActive == true)
        {
            _shieldLives--;
            _uiManager.UpdateShield(_shieldLives);
            if (_shieldLives == 3)
            {
                StartCoroutine(ShieldPowerDownRoutine());
                return;
            }
            if (_shieldLives == 2)
            {
                StartCoroutine(ShieldPowerDownRoutine());
                return;
            }

            if (_shieldLives == 1)
            {
                StartCoroutine(ShieldPowerDownRoutine());
                return;
            }
            if (_shieldLives <= 0)
            {
                _isShieldActive = false;
                playerShield.SetActive(false);
                _uiManager.UpdateShield(_shieldLives);
                return;
            }
           
        }


        _lives--;
         if (_lives < 3)
        {
            _rightHurt.SetActive(true);
        }
         if (_lives < 2)
        {
            _leftHurt.SetActive(true);
        }





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
    public void HealPlayer()
    {
        if (_lives <= 2)
        {
            _lives++;
            _uiManager.UpdateLives(_lives);
            if (_lives == 3)
            {
                _leftHurt.SetActive(false);
                _rightHurt.SetActive(false);
            }
            if (_lives == 2)
            {
                _rightHurt.SetActive(true);
                _leftHurt.SetActive(false);
            }
            if (_lives == 1)
            {
                _rightHurt.SetActive(true);
                _leftHurt.SetActive(true);
            }
        }
        else
        {
            return;
        }
    }
    public void RefillAmmo()
    {
        _audioSource.PlayOneShot(_powerUpClip);
        _ammo = 15;
        _uiManager.UpdateAmmo(_ammo);
    }
    public void ActivateTripleShot()
    {
        _isTripleShotActive = true;
        _ammo = 15;
        _audioSource.PlayOneShot(_powerUpClip);
        StartCoroutine(TripleShotPowerDownRoutine());
    }
    public void ActivateSpeedBoost()
    {
        _audioSource.PlayOneShot(_powerUpClip);
        _isSpeedBoostActive = true;
        if (_isSpeedBoostActive == true)
        {
            speed *= 4;
            StartCoroutine(SpeedBoostPowerDownRoutine());
        }
        

    }
    public void ActivateShield()
    {
        
        _audioSource.PlayOneShot(_powerUpClip);
        _isShieldActive = true;
        _shieldLives =3;
        _uiManager.UpdateShield(_shieldLives);
        playerShield.SetActive(true);
      

        
      
       
       //StartCoroutine(ShieldPowerDownRoutine());
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
        playerShield.SetActive(false);
        _playerHurtShield.SetActive(true);
        yield return new WaitForSeconds(0.30f);
        _playerHurtShield.SetActive(false);
        playerShield.SetActive(true);
        //_playerHurtShield.SetActive(false);

        
    }
    IEnumerator ThrusterRecoverRoutine()
    {
        while (_thrusterBarPrecentage <= 100f)
        {
            yield return new WaitForSeconds(0.8f);
            _thrusterRecover = true;
            _thrusterBarPrecentage += 500/100 * Time.deltaTime;
            _uiManager.UpdateThrusterBar(_thrusterBarPrecentage);
            yield return new WaitForSeconds(0.5f);

            if(_thrusterBarPrecentage >= 100f)
            {
                _thrusterBarPrecentage = 100f;
                _uiManager.UpdateThrusterBar(_thrusterBarPrecentage);
                _thrusterRecover = false;
                break;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLazer")
        {
           Damage();

        }
    }

}