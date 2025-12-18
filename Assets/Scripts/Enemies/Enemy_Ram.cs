using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Ram : MonoBehaviour
{

    private Player _player;
    [SerializeField]
    private GameObject _explosionPrefab;

    private float _distance;
    [SerializeField] private float _distanceBetween;
    [SerializeField] private float _speed;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is null");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (_player != null)
        {
            RamTime();
        }
    }

    public void RamTime()
    {
        _distance = Vector3.Distance(transform.position, _player.transform.position);
        Vector3 direction = _player.transform.position - transform.position;

        if (_distance >= _distanceBetween)
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }
        else if (_distance <= _distanceBetween)
        {
            _speed = 15;
            transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime);
       
        }

        if (transform.position.y <= -6)
        {
            transform.position = new Vector3(Random.Range(-8, 8), 7, 0); 
        }

    }

    public void EnemyDeath()
    {
        //_audioSource.PlayOneShot(_explosionAudio);
        //  _enemyAnimator.SetTrigger("OnEnemyDeath");
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        _speed = 0;


    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "PlayerLazer")
        {

            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.Scoreup(10);
             
            }
            EnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject);



        }
        else if (other.tag == "Player")
        {

            if (_player != null)
            {
                _player.Damage();
               
            }

            EnemyDeath();

            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject);


        }
    }

}
