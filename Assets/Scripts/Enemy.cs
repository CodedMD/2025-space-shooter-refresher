using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private float speed = 4.0f;
    
    // Start is called before the first frame update
    void Start()
    {
       
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(0,-1,0);
        // move the player
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y < -6f)
        {
            float randomX = Random.Range(-9.36f, 9.36f);
            transform.position = new Vector3(randomX, 7, 0);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Lazer")
        {
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Player")
        {
            Player player =  other.transform.GetComponent<Player>();
            if (player != null)
                player.Damage();
            Destroy(this.gameObject);

            
        }
    }
}
