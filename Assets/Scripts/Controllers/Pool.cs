using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Pool<T>
{
    private Dictionary<Enum, LinkedList<T>> pool =
        new Dictionary<Enum, LinkedList<T>>();

    private Transform spawnParentTransform;
    private Func<T> creator;
    private Action<T> init;

    public Pool(Func<T> creator, List<Enum> types, Action<T> init = null)
    {
        foreach (Enum type in types)
        {
            pool.Add(type, new LinkedList<T>());
        }
        this.creator = creator;
        this.init = init;
    }
    
    public T GetFreeObject(Enum type)
    {
        if (pool[type].Count < 1)
        {
            var obj = creator();
            if (init != null)
                init.Invoke(obj);
            return obj;
        }
        else
        {
            var obj = pool[type].First.Value;
            pool[type].RemoveFirst();
            return obj;
        }
    }

    public bool AddFreeObject(Enum type, T obj)
    {
        if (!pool.Keys.Contains(type))
        {
            return false;
        }
        pool[type].AddLast(obj);
        return true;
    }
}
