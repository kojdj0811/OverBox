using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Request", menuName = "Scriptable Object/Create New Request")]
public class Request : ScriptableObject
{
    // Start is called before the first frame update
    public string clientName;
    public string clientType;
    public int matchIDX;
    
}
