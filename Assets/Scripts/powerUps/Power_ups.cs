using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_ups : MonoBehaviour
{

    private float speed = 3.0f;
    [SerializeField]private int powerupID;
    private bool _moveCloser;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

    }

    // Update is called once pers  frame
    void Update()
    {
        if (_moveCloser)
        {
            if (Vector3.Distance(_player.transform.position, transform.position) > 0)
            {
               transform.position += (Vector3)(_player.transform.position - transform.position).normalized * speed * Time.deltaTime;
            }
            
        }
        Vector2 direction = new Vector2(0, -1);
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    public void MoveCloserToPlayer()
    {
        _moveCloser = true;
    }
    public void StopMovingCloserToPlayer()
    {
        _moveCloser = false;
    }

    public void OnEnable()
    {
       EventDelegator.movePowerupsTowardPlayer += MoveCloserToPlayer;
         EventDelegator.dontMoveTowardsPlayer += StopMovingCloserToPlayer;

    }
    public void OnDisable()
    {
        EventDelegator.movePowerupsTowardPlayer -= MoveCloserToPlayer;
         EventDelegator.dontMoveTowardsPlayer -= StopMovingCloserToPlayer;
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "EnemyLaser")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();


            if (player != null)
            {
                switch (powerupID)
                {
                    case 0:
                    player.ActivateTripleShot();
                        break;
                        case 1:
                        player.ActivateSpeedBoost();
                        break;
                          case 2:
                           player.ActivateShield();
                             break;
                            case 3:
                                player.RefillAmmo();
                                break;
                        case 4:
                            player.HealPlayer();
                            break;
                        case 5:
                            player.ActivateNinjaStars();
                            break;
                    case 6:
                        player.Damage();
                        break;
                        case 7:
                            player.ActivateHomingOrbs();
                        break;
                    default:
                        Debug.Log("Default case");
                        break;
                }
              
            }
            Destroy(this.gameObject);  



        }
    }
}
