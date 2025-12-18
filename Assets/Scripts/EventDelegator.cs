using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventDelegator : MonoBehaviour
{

    public delegate void MovePowerupsTowardPlayer();
    public static event MovePowerupsTowardPlayer movePowerupsTowardPlayer;
    public delegate void DontMoveTowardsPlayer();
    public static event DontMoveTowardsPlayer dontMoveTowardsPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (movePowerupsTowardPlayer != null)
            {
               movePowerupsTowardPlayer();
            }
        }
        if (Input.GetKeyUp(KeyCode.C))
        {
            if (dontMoveTowardsPlayer != null)
            {
               dontMoveTowardsPlayer();
            }
        }
    }

  



}
