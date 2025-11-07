using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grabiod : MonoBehaviour
{
    [SerializeField]
    private float speed = 4.0f;
    private Player _player;
    [SerializeField]
    private Lazer _lazer;
    //[SerializeField]
    //private Animator _enemyAnimator;
    //[SerializeField]
    //private GameObject enemyLazerPrefab;

    [SerializeField]
    private GameObject _explosionPrefab;

    private float _canFire;
    private float _fireRate = 3.0f;

    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private AudioClip _explosionAudio;

    [SerializeField]
    private Transform[] _wayPoint;
    [SerializeField]
    private int _wayPointIndex = 0;
    [SerializeField]
    private GameObject _enemyBeam;


    // Start is called before the first frame update
    void Start()
    {
        
        _audioSource = GetComponent<AudioSource>();
       // _lazer = gameObject.GetComponent<Lazer>();
        _player = GameObject.Find("Player").GetComponent<Player>();
        //_enemyAnimator = gameObject.GetComponent<Animator>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
       if (_lazer == null)
        {
            Debug.LogError("Enemy Lazer is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        //CinemachineShake sets enemy lazer prefab to inactive so we need to set it to active here
        _enemyBeam.gameObject.SetActive(true);
        if (_player != null)
        {
            GraboidMode();
            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(1f, 3f);
                _canFire = Time.time + _fireRate;
                // fire lazer
                EnemyLazer();
                
            }
        }

           
    }

    public void GraboidMode()
    {
        if (_wayPointIndex <= _wayPoint.Length - 1)
        {
            transform.position = Vector3.MoveTowards(transform.position, _wayPoint[_wayPointIndex].transform.position, speed * Time.deltaTime);
        }
        if (transform.position == _wayPoint[_wayPointIndex].transform.position)
        {
            _wayPointIndex += 1;
            if (_wayPointIndex == _wayPoint.Length)
            { _wayPointIndex = 0; }

        }

        if(transform.position.x >= 9.0f)
        {
            transform.position = new Vector3(Random.Range(-9.0f, -9.90f), 1, 0); 
            Vector3.MoveTowards(transform.position, _wayPoint[_wayPointIndex].transform.position, speed * Time.deltaTime);
        
        }


    }

    public void EnemyLazer()
    {

        GameObject enemyLazer = Instantiate(_enemyBeam, transform.position + new Vector3(0,-1.5f,0), Quaternion.identity);
        Lazer[] lazers = enemyLazer.GetComponentsInChildren<Lazer>();

        for (int i = 0; i < lazers.Length; i++)
        {
            lazers[i].AssignEnemyLazer();
        }


    }

    public void EnemyDeath()
    {
        //_audioSource.PlayOneShot(_explosionAudio);
        //  _enemyAnimator.SetTrigger("OnEnemyDeath");
        Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
        speed = 0;


    }

    void OnTriggerEnter2D(Collider2D other)
    {


        if (other.tag == "PlayerLazer")
        {

            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.Scoreup(10);
                _enemyBeam.SetActive(false);
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
                _enemyBeam.SetActive(false);
            }

            EnemyDeath();
            _enemyBeam.SetActive(false);
            Destroy(GetComponent<Collider2D>());

            Destroy(this.gameObject, 1.5f);


        }
    }
}
