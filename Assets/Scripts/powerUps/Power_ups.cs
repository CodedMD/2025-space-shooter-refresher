using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Power_ups : MonoBehaviour
{

    private float speed = 3.0f;
    [SerializeField]private int powerupID;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once pers  frame
    void Update()
    {
        Vector2 direction = new Vector2(0, -1);
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        
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
                    default:
                        Debug.Log("Default case");
                        break;
                }
              
            }
            Destroy(this.gameObject);  



        }
    }
}
