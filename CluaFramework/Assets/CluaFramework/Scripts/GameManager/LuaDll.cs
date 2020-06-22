using AOT;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LuaInterface
{
#if UNITY_EDITOR_WIN || UNITY_STANDALONE_WIN
    [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    public delegate int LuaCSFunction(IntPtr luaState);
    //[UnmanagedFunctionPointer(CallingConvention.Cdecl)]
    //public delegate void LuaHookFunc(IntPtr L, ref Lua_Debug ar);
#else
    public delegate int LuaCSFunction(IntPtr luaState);    
    //public delegate void LuaHookFunc(IntPtr L, ref Lua_Debug ar);    
#endif
    class Clua
    {

        #region c++打印
        public delegate void LogDelegate(IntPtr message, uint iSize);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern void InitCSharpDelegate(LogDelegate log);

        //C# Function for C++‘s call
        [MonoPInvokeCallback(typeof(LogDelegate))]
        public static void LogMessageFromCpp(IntPtr message, uint iSize)
        {
            Debug.Log(Marshal.PtrToStringAnsi(message, (int)iSize));
        }
        #endregion

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern int myAdd(int a, int b);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr clua_luaL_newstate();

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern void clua_luaL_openlibs(IntPtr L);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern int clua_luaL_dofile(IntPtr L, string fn);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        public static extern void clua_lua_close(IntPtr L);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_Loger(IntPtr L);

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern double clua_CallLuaFunc(IntPtr L, string func, double x, double y);

        public static double CallLuaFunc(IntPtr L, string func, double x, double y)
        {
            return clua_CallLuaFunc(L, func, x, y);
        }
        /// <summary>
        /// lua中打印log初始化 lua中调用Loger
        /// </summary>
        public static void LuaLogerInit(IntPtr L)
        {
            clua_lua_Loger(L);
        }
        public static IntPtr luaL_newstate()
        {
            return clua_luaL_newstate();
        }
        public static void luaL_openlibs(IntPtr L)
        {
            clua_luaL_openlibs(L);
        }
        public static int luaL_dofile(IntPtr L, string fn)
        {
            return clua_luaL_dofile(L, fn);
        }
        public static void lua_close(IntPtr L)
        {
            clua_lua_close(L);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_luaCall(IntPtr L, string func);
        public static void luaCall(IntPtr L, string func)
        {
            clua_luaCall(L, func);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern int clua_gettop(IntPtr L);
        public static int lua_gettop(IntPtr L)
        {
            return clua_gettop(L);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_luaopen_base(IntPtr L);
        public static void luaopen_base(IntPtr L)
        {
            clua_luaopen_base(L);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_luaL_requiref(IntPtr L, string modename, IntPtr openf, int glb);
        public static void luaL_requiref(IntPtr L, string modename, LuaCSFunction openf, int glb)
        {
            IntPtr fn = Marshal.GetFunctionPointerForDelegate(openf);
            clua_luaL_requiref(L, modename, fn, glb);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_luaL_newmetatable(IntPtr L, string tname);
        public static void luaL_newmetatable(IntPtr L, string tname)
        {
            clua_luaL_newmetatable(L, tname);
        }
        //clua_lua_pushstring
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_pushstring(IntPtr L, string str);
        public static void lua_pushstring(IntPtr L, string str)
        {
            clua_lua_pushstring(L, str);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_pushnumber(IntPtr L, double n);
        public static void lua_pushnumber(IntPtr L, double n)
        {
            clua_lua_pushnumber(L, n);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_pushvalue(IntPtr L, int idx);
        public static void lua_pushvalue(IntPtr L, int idx)
        {
            clua_lua_pushvalue(L, idx);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_pushcfunction(IntPtr L, IntPtr f);
        public static void lua_pushcsfunction(IntPtr L, LuaCSFunction f)
        {
            //LuaCSFunction csf_delegate = new LuaCSFunction(f);
            IntPtr fn = Marshal.GetFunctionPointerForDelegate(f);
            clua_lua_pushcfunction(L, fn);
        }

        //clua_lua_tostring
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr clua_lua_tostring(IntPtr L, int idx);
        public static string lua_tostring(IntPtr L, int idx)
        {
            IntPtr str = clua_lua_tostring(L, idx);
            if (str != IntPtr.Zero)
            {
                return Marshal.PtrToStringAnsi(str);
            }
            return null;
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern double clua_lua_tonumber(IntPtr L, int idx);
        public static double lua_tonumber(IntPtr L, int idx)
        {
            return clua_lua_tonumber(L, idx);
        }

        //clua_lua_pushlightuserdata
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_pushlightuserdata(IntPtr L, IntPtr userdata);
        public static void lua_pushlightuserdata(IntPtr L, object o)  
        {
            int lenght = Marshal.SizeOf(o);
            IntPtr pA = Marshal.AllocHGlobal(lenght);
            Marshal.StructureToPtr(o, pA, false);
            clua_lua_pushlightuserdata(L, pA);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_setglobal(IntPtr L, string name);
        public static void lua_setglobal(IntPtr L, string name)
        {
            clua_lua_setglobal(L, name);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_setfield(IntPtr L, int idx, string key);
        public static void lua_setfield(IntPtr L, int idx, string key)
        {
            clua_lua_setfield(L, idx, key);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_rawset(IntPtr L, int idx);
        public static void lua_rawset(IntPtr L, int idx)
        {
            clua_lua_rawset(L, idx);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_newtable(IntPtr L);
        public static void lua_newtable(IntPtr L)
        {
            clua_lua_newtable(L);
        }
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_getglobal(IntPtr L,string name);
        public static void lua_getglobal(IntPtr L,string name)
        {
            clua_lua_getglobal(L,name);
        }

        //clua_lua_setmetatable
        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern void clua_lua_setmetatable(IntPtr L, int objidx);
        public static void lua_setmetatable(IntPtr L, int objidx)
        {
            clua_lua_setmetatable(L, objidx);
        }

        [DllImport("Clua", CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr clua_lua_newuserdata(IntPtr L, uint sz);
        
        public static void lua_newuserdata(IntPtr L, object o)
        {
            //int lenght = Marshal.SizeOf(o);
            //IntPtr pA = Marshal.AllocHGlobal(lenght);
            //Marshal.StructureToPtr(o, pA, false);
            IntPtr go = clua_lua_newuserdata(L, 1);
            //go = pA;
            LuaManager.Instance.luaObjectDic.Add(go.ToInt64(), o);
        }
    }
}