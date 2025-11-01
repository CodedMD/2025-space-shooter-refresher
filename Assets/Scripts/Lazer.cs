using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lazer : MonoBehaviour
{ 
    private float speed = 15f;
    private Vector3 direction = Vector3.up;
   

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
        if(transform.position.y > 6f)
        {
            Destroy(this.gameObject);
        }

    }
    
}
