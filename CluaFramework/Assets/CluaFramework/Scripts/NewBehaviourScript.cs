using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        foreach (MethodInfo item in typeof(GameObject).GetMethods())
        {
           // Debug.Log(item.Name);
        }
        Type t = Type.GetType("Main");
        Debug.Log("8888--------" + typeof(GameObject));
        Debug.Log("ccccc  " + typeof(GameObject).Name + ":" + typeof(GameObject).Namespace);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
