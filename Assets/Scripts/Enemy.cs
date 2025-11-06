using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4.0f;
    private Player _player;
    [SerializeField]
    private Animator _enemyAnimator;
    [SerializeField]
    private  GameObject enemyLazerPrefab;
    private int _canFire = -1;
    private float _fireRate = 3.0f;
    // Start is called before the first frame update
    void Start()
    {
       _player = GameObject.Find("Player").GetComponent<Player>();
        _enemyAnimator = gameObject.GetComponent<Animator>();
        if (_player == null)
        {
            Debug.LogError("Player is null");
        }
        if (_enemyAnimator == null)
        {
            Debug.LogError("Enemy Animator is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        EnemyFire();
        Vector3 direction = new Vector3(0,-1,0);
        // move the player
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    public void EnemyFire()
    { 
          if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3.0f, 7.0f);
            _canFire = (int)(Time.time + _fireRate);
            GameObject enemyLazer = Instantiate(enemyLazerPrefab, transform.position, Quaternion.identity);
            Lazer[] lazers = enemyLazer.GetComponentsInChildren<Lazer>();
            for (int i = 0; i < lazers.Length; i++)
            {
                lazers[i].AssignEnemyLazer();
            }
        }
    }

   public void EnemyDeath()
    {
       _enemyAnimator.SetTrigger("OnEnemyDeath");
        speed = 0;
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {

       
        if (other.tag == "PlayerLazer")
        {
            
            Destroy(other.gameObject);
            if (_player!=null)
            {
                _player.Scoreup(10);
            }
            EnemyDeath();
            Destroy(GetComponent< Collider2D >());
            Destroy(this.gameObject,52.5f);

        }
        else if (other.tag == "Player")
        {
            
            if (_player != null)
            {
                _player.Damage();
            }

            EnemyDeath();
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject,2.5f);

            
        }
    }
}
