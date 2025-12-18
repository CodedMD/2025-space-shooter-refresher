using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackFireLazer : MonoBehaviour
{
    private Player _player;
    private float _speed = 15f;
    private Vector3 direction = Vector3.up;
    [SerializeField]
  



    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        if (_player == null)
        {
            Debug.Log("Player is null");
        }
       
        // _isHeatSeeking = false;
    }

    // Update is called once per frame
    void Update()
    {
       
      
        
            EnemyLazer();

        

    }

    
    public void EnemyLazer()
    {

        transform.Translate(direction * _speed * Time.deltaTime);
        if (transform.position.y > 5f)
        {
            
            if (transform.parent != null)
            {
               
                Destroy(transform.parent.gameObject);
            }
            Destroy(this.gameObject);
        }
    }




}
