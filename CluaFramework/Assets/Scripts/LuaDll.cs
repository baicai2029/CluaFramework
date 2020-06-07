using AOT;
using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace LuaInterface
{
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
        private static extern double clua_CallLuaFunc(IntPtr L,string func, double x, double y);

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
    }
}