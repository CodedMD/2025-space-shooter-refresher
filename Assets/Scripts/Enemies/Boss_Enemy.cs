using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Enemy : MonoBehaviour
{
    [SerializeField]
    private float _speed = 3.5f;
    public Sprite _openEyes, _closedEyes, _closedEyesCharge1, _openEyesRoar;

    [SerializeField]
    private GameObject _particlesCharge;

    private float _canFire;
    [SerializeField]
    private float _fireRate = 1f;

    private int _bossHealth = 100;
    private int _bossMovement = 1;
    private bool _isBossAttcking = false;
    private bool  _bossAi = false;
    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(0, 7, 0);
        _particlesCharge.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (transform.position.y > 2.3f)
        {
            BossLoadingMovement();
        }
        if (transform.position.y <= 2.3f)
        {
         
            PowerBurst();
            SidewaysMovements();
        }
       
      

    }

    void BossLoadingMovement()
    {

        transform.Translate(Vector3.down * _speed * Time.deltaTime);

    }
    public void SidewaysMovements()
    {
       
        if (transform.position.x <= -5.3f)
        {
            _bossMovement = 1;
        }
        else if (transform.position.x >= 5.3f)
        {
            _bossMovement = -1;

        }
        transform.Translate(Vector3.right *_bossMovement * _speed * Time.deltaTime);
        
    }

    IEnumerator BattlePause()
    {
        _speed = 0;
        yield return new WaitForSeconds(2f);
        _isBossAttcking = true;
        _bossAi = true;
        _speed = 3.5f;

    }

    public void PowerBurst()
    {
        
        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(5f, 8f);
            _canFire = Time.time + _fireRate;
            StartCoroutine(BattlePause());
            StartCoroutine(ChargeUpWarning());

            StartCoroutine(ChargingLights());
        }
        StopCoroutine(ChargeUpWarning());
        StopCoroutine(ChargingLights());
      
    }


    IEnumerator ChargeUpWarning()
    {
       
        _particlesCharge.gameObject.SetActive(true);
        GetComponent<SpriteRenderer>().sprite = _closedEyesCharge1;
        yield return new WaitForSeconds(3f);
        _particlesCharge.gameObject.SetActive(false);


    }

    IEnumerator ChargingLights()
    {
        GetComponent<SpriteRenderer>().sprite = _openEyesRoar;
        yield return new WaitForSeconds(2f);
        GetComponent<SpriteRenderer>().sprite = _closedEyesCharge1;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = _closedEyes;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = _closedEyesCharge1;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = _closedEyes;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = _closedEyesCharge1;
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().sprite = _closedEyes;
        yield return new WaitForSeconds(1f);


    }

    private void BossHealth()
    {
      

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Laser")
        {
            _bossHealth--;
            Destroy(other.gameObject);
            if (_bossHealth < 1)
            {
                Destroy(this.gameObject);
            }
        }
    }




}
