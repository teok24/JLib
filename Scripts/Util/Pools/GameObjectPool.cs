using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace JLib.Utils.Pools
{
    public class GameObjectPool : Pool<GameObject>
    {
        private GameObject prefab;
        private Transform parent;
        public List<GameObject> poolList = new();
        public static event System.Action OnCreateObject;
        public delegate void OnObjectReturnedDelegate(GameObject obj);
        public delegate void OnObjectTakenDelegate(GameObject obj);
        public static event OnObjectReturnedDelegate OnObjectReturned;
        public static event OnObjectTakenDelegate OnObjectTaken;
        public GameObjectPool(int poolSize,GameObject prefab, Transform parent) : base(poolSize)
        {
            this.prefab = prefab;
            this.parent = parent;
            Initialize();
        }

        protected override GameObject CreateObject()
        {
            var obj = Object.Instantiate(prefab, parent);
            obj.SetActive(false);
            OnCreateObject?.Invoke();
            poolList.Add(obj);
            return obj;
        }
        /// <summary>
        /// When an object is returned to the pool, set it to inactive
        /// </summary>
        /// <param name="obj"></param>
        protected override void ObjectReturned(GameObject obj)
        {
            //Maybe a delegate here to be called when object is returned
            obj.SetActive(false);
            OnObjectReturned?.Invoke(obj);
        }
        /// <summary>
        /// When an object is taken from the pool, set it to active
        /// </summary>
        /// <param name="obj"></param>
        protected override void ObjectTaken(GameObject obj)
        {
            obj.SetActive(true);
            OnObjectTaken?.Invoke(obj);
        }
        public void SetObjectPosition(GameObject obj, Vector3 position)
        {
            SetObjectPosition(obj, position, Quaternion.identity);
        }
        public void SetObjectPosition(GameObject obj, Vector3 position, Quaternion rotation)
        {
            obj.transform.SetPositionAndRotation(position, rotation);
        }
    }
}