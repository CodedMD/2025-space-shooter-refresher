using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Dodge : MonoBehaviour
{
    [SerializeField]
    private Enemy _enemy;

  
    // Start is called before the first frame update
    void Start()
    {
        _enemy = transform.GetComponentInParent<Enemy>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator DodgeCooldown()
    {
        transform.gameObject.SetActive(false);
        yield return new WaitForSeconds(2.0f);
        transform.gameObject.SetActive(true);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "PlayerLazer")
        {
            
            _enemy.DodgeLazer();
            StartCoroutine(DodgeCooldown());
        }

        if (other.tag == "Player")
        {
            
            _enemy.DodgeLazer();
            StartCoroutine(DodgeCooldown());
        }

    }


}
