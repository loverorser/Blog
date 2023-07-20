using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class EventSystem
{
    class CCC
    {
        public int id;
        public Action<IBaseEvent> callback;

    }

    static Dictionary<Type,List<CCC>> dic_n = new Dictionary<Type, List<CCC>>();
    public static void AddEventListener<T>(Action<T> callback) where T : IBaseEvent
    {


        Action<IBaseEvent> a = (e) => { callback.Invoke((T)e); };

        CCC cCC = new CCC()
        {
            callback=a,
            id=callback.GetHashCode()
        };

        AddEventListener(typeof(T), cCC);
    }
    static void AddEventListener(Type eventType,CCC ccc)
    {
        if (dic_n.ContainsKey(eventType))
        {
            if (!dic_n[eventType].Contains(ccc))
            {
                dic_n[eventType].Add(ccc);
            }
        }
        else
            dic_n.Add(eventType, new List<CCC>() { ccc });
    }
    public static void RemoveEventListener<T>(Action<T> callback) where T : IBaseEvent
    {
        int id=callback.GetHashCode();
        RemoveEventListener(typeof(T), id);
    }
    static void RemoveEventListener(Type eventType, int id)
    {
        if (dic_n.ContainsKey(eventType))
            foreach (var v in dic_n[eventType])
                if (v.id == id)
                {
                    dic_n[eventType].Remove(v);
                    return;
                }

    }
    public static void InvokeEvent<T>(T evt) where T : IBaseEvent
    {
        InvokeEvent(typeof(T), evt);
    }
    static void InvokeEvent(Type eventType,IBaseEvent evt)
    {
        if (dic_n.ContainsKey(eventType))
        {
            foreach (var v in dic_n[eventType])
                v.callback.Invoke(evt);
        }
    }

}
