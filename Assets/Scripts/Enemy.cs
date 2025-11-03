using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4.0f;
    private Player _player;
    [SerializeField]
    private Animator _enemyAnimator;
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
        Vector3 direction = new Vector3(0,-1,0);
        // move the player
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-8f, 8f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

   public void EnemyDeath()
    {
       _enemyAnimator.SetTrigger("OnEnemyDeath");
        speed = 0;
       

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Lazer")
        {
            
            Destroy(other.gameObject);
            if (_player!=null)
            {
                _player.Scoreup(10);
            }
            EnemyDeath();
            Destroy(this.gameObject,52.5f);

        }
        else if (other.tag == "Player")
        {
            
            if (_player != null)
            {
                _player.Damage();
            }

            EnemyDeath();

            Destroy(this.gameObject,2.5f);

            
        }
    }
}
