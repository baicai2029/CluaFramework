using System;
using System.Diagnostics;
using System.IO;
using AOT;
using LuaInterface;

public class UnityEngine_GameObjectWrap 
{
    public static void Register(IntPtr L)
    {
        Clua.lua_newtable(L);
        int spaceIdx = Clua.lua_gettop(L);
        Clua.luaL_newmetatable(L, typeof(UnityEngine.GameObject).Name);
        int metatableIdx = Clua.lua_gettop(L);
        Clua.lua_pushvalue(L, metatableIdx);
        Clua.lua_setfield(L, metatableIdx, typeof(UnityEngine.GameObject).Name);
        Clua.lua_pushstring(L, "create");
        LuaCSFunction lcf = new LuaCSFunction(create_GameObject);
        Clua.lua_pushcsfunction(L, lcf);
        Clua.lua_rawset(L, metatableIdx);
        Clua.lua_pushstring(L, "Find");
        LuaCSFunction lcff = new LuaCSFunction(GameObject_Find);
        Clua.lua_pushcsfunction(L, lcff);
        Clua.lua_rawset(L, metatableIdx);
        Clua.lua_setfield(L, spaceIdx, typeof(UnityEngine.GameObject).Name);
        Clua.lua_setglobal(L, typeof(UnityEngine.GameObject).Namespace);
    }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int create_GameObject(IntPtr L)
    {
        try
        {
            int count = Clua.lua_gettop(L);
            //UnityEngine.Debug.Log("创建物体的名字是参数个数 "+count);
            string name = Clua.lua_tostring(L, -1);
            //UnityEngine.Debug.Log("创建物体的名字 :" + name);

            UnityEngine.GameObject go = new UnityEngine.GameObject(name);
            
            //Clua.lua_pushstring(L,"创建gameobject成功 "+go.name);
            IntPtr data = Clua.lua_newuserdata(L, go);
            
            LuaManager.Instance.luaObjectDic.Add(data.ToInt64(), go);
            //Clua.lua_pushlightuserdata(L, data);
            
        }
        catch(IOException message)
        {
            UnityEngine.Debug.LogError(message.Message);
        }
        
        return 1;
    }
    [MonoPInvokeCallbackAttribute(typeof(LuaCSFunction))]
    static int GameObject_Find(IntPtr L)
    {
        string name = Clua.lua_tostring(L, -1);
        UnityEngine.GameObject go = UnityEngine.GameObject.Find(name);
        if(go)
        {
            //Clua.lua_pushlightuserdata(L, go);
        }
        else
        {
            UnityEngine.Debug.LogError("go is null");
            return 0;
        }
        return 1;
    }
   
}
