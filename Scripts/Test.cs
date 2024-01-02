using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JLib;
using JLib.Utils.Interfaces;
using TMPro;
using UnityEditor.AnimatedValues;
using JLib.Utils.Pools;
using System;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
   GameObjectPool pool;
   public bool Get = false;
   public bool Return = false;
   public GameObject prefab;
   int location = -5;



    void Awake()
    {
        
    }

    void Start()
    {
        pool = new GameObjectPool(10, prefab, transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (Get)
        {
            Get = false;
            var obj = pool.GetObject();
            obj.transform.position = new Vector3(location, 0, 0);
            location ++;
        }
        if(Return)
        {
            Return = false;
            var obj = GetFirstActiveObject(pool);
            if(obj != null) pool.ReturnObject(obj);
            location --;
        }
    }

    private GameObject GetFirstActiveObject(GameObjectPool pool)
    {
        for(int i = 0; i < pool.poolList.Count; i++)
        {
            if(pool.poolList[i].activeSelf)
            {
                return pool.poolList[i];
            }
        }
        return null;
    }
}
