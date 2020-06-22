using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuaManager : MonoBehaviour
{
    private static LuaManager instance;
    public static LuaManager Instance
    {
        get
        {
            return instance;
        }
    }
    public Dictionary<long, object> luaObjectDic;
    private void Awake()
    {
        instance = this;
        luaObjectDic = new Dictionary<long, object>();
    }
    
}
