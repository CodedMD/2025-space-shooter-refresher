using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "nameWave.asset", menuName = "Scriptableobjects/new Wave")]
public class Wave : ScriptableObject
{
    public List<GameObject> sequence;
}
