using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing_Orbs : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    private float _rotationSpeed = 350f;
    private GameObject _closeEnemy;
    [SerializeField]
    private Rigidbody2D _rb;

    public bool lookingForEnemy = false;
    public bool foundEnemy = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        if (_rb != null)
        {
          //  Debug.LogError("_rb is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (_closeEnemy == null)
        {
            _closeEnemy = WheresTheEnemy();
        }
        if (_closeEnemy != null)
        {
            MoveTowardsEnemy();
        }
        else
        {
        
        transform.Translate(Vector3.up*(speed/3)*Time.deltaTime);
        }

        if (transform.position.y > 5 || transform.position.y < -5 || transform.position.x < -9 || transform.position.x > 9)
                
            {
        
            if(transform.parent!= null && transform.position.y > 5)
            {
                Destroy(transform.parent.gameObject);
            }
           
            }



    }

    
    private GameObject WheresTheEnemy()
    {
        lookingForEnemy = true;
        foundEnemy = false;
        try
        {
           

            GameObject[] enemies;
            enemies = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject close = null;
            float distance = Mathf.Infinity;
            Vector3 position = transform.position;

            foreach (GameObject enemy in enemies)
            {
                Vector3 other = enemy.transform.position - position;
                float curDistance = other.sqrMagnitude;
                if(curDistance < distance)
                {
                    close = enemy;
                    distance = curDistance;
                }

            }
            return close;

        }
        catch
        {
                       return null;
        }

    }


    private void MoveTowardsEnemy()
    {
        foundEnemy = true;
        lookingForEnemy = false;

        Vector3 direction = _rb.position - (Vector2)_closeEnemy.transform.position;
        direction.Normalize();
        float rotateAmount = Vector3.Cross(direction, transform.up).z;
        _rb.angularVelocity = rotateAmount * _rotationSpeed;
        _rb.velocity = transform.up * speed;
    }

}
