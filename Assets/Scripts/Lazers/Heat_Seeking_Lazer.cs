using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heat_Seeking_Lazer : MonoBehaviour
{
    private Player _player;
    private Vector3 direction = Vector3.down;
    //[SerializeField]
   // private bool _isHeatSeeking = false;
    [SerializeField] private float _distanceBetween;
    private float _distance;
    private float _speed = 5f;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        
    }

    // Update is called once per frame
    void Update()
    {
       if (_player != null)
        {
            HeatSeekingLazer();
        }
      // HeatSeekingLazer();
    }


    public void HeatSeekingLazer()
    {

        transform.Translate(direction * _speed * Time.deltaTime);
       _distance = Vector3.Distance(transform.position, _player.transform.position);
         Vector3 _direction = _player.transform.position - transform.position;


        if (_distance >= _distanceBetween)
         {
             transform.Translate(direction * _speed * Time.deltaTime);
            //* transform.Translate(Vector3.down * _speed * Time.deltaTime);
         }
         else if (_distance <= _distanceBetween )
         {
            _speed = 10;

             transform.position = Vector3.MoveTowards(this.transform.position, _player.transform.position, _speed * Time.deltaTime);
            
         }

        if (transform.position.y < -5.1f)
        {
            
                

            
            Destroy(this.gameObject);
        }
    
    
    }

   



}
