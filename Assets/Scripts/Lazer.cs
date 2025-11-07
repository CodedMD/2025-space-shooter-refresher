using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{ 
    private float speed = 15f;
    private Vector3 direction = Vector3.up;
   private bool isEnemyLazer = false;

    // Update is called once per frame
    void Update()
    {
       if (isEnemyLazer == false)
        {
            PlayerLazer();
        }
        else
        {
            EnemyLazer();
        }

    }

    public void PlayerLazer()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if (transform.position.y > 6f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void EnemyLazer()
    {
        
        transform.Translate(-direction * speed * Time.deltaTime);
        if (transform.position.y < -5f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }

    public void AssignEnemyLazer()
    {

        isEnemyLazer = true;
    }

    

 }
