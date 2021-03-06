﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LuaInterface;
using System;
using System.Runtime.CompilerServices;

public class Main : MonoBehaviour
{
    string main ;
    IntPtr L;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
        try
        {
            main = Application.dataPath + "/Lua/" + "Main.lua";
            Clua.InitCSharpDelegate(Clua.LogMessageFromCpp); //c++ log委托绑定
            Debug.Log(Clua.myAdd(10, 8));
            L = Clua.luaL_newstate();
            Clua.luaL_openlibs(L);
            Clua.LuaLogerInit(L);
            UnityEngine_GameObjectWrap.Register(L);
            int index = Clua.luaL_dofile(L, main);
            Debug.Log("执行dofile的返回值 " + index);
            double xx = Clua.CallLuaFunc(L, "main", 10, 18);
            Clua.luaCall(L, "Start");
            Debug.Log("-------------" + xx);
            Debug.Log("-------------");
            foreach (var item in LuaManager.Instance.luaObjectDic)
            {
                Debug.Log("xxx " + item.Key + ":" + item.Value);
            }
        }
        catch(Exception e)
        {
            Debug.LogError(e.Message);
        }
        
        
    }
   
    // Update is called once per frame
    void Update()
    {
        Clua.luaCall(L, "Update");
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("调用c函数结果是: " + Clua.myAdd(20, 19));
        }
    }
    private void FixedUpdate()
    {
        Clua.luaCall(L, "FixedUpdate");
    }

    private void OnApplicationQuit()
    {
        Clua.luaCall(L, "OnApplicationQuit");
        Clua.lua_close(L);
    }
}
