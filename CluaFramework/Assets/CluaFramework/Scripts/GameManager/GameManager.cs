using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager :MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if(instance==null)
            {
                return GameObject.FindGameObjectWithTag("App").GetComponent<GameManager>();
            }
            return instance;
        }
    }
    [HideInInspector]
    public Transform App;
    private void Awake()
    {
        instance = this;
        App = GameObject.FindGameObjectWithTag("App").transform;
        AddComponent();
    }
    private void AddComponent()
    {
        App.gameObject.AddComponent<LuaManager>();
        App.gameObject.AddComponent<PoolManager>();
    }

}
