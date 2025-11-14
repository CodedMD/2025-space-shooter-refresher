using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinning : MonoBehaviour
{
    [SerializeField]
    private float _rotationSpeed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.down * _rotationSpeed * Time.deltaTime);
    }
}
