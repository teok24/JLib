using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Utils.Pools
{
    public abstract class Pool<T>
    {
        private Queue<T> poolQueue = new Queue<T>();
        private int poolSize;

        public Pool(int poolSize)
        {
            this.poolSize = poolSize;
        }
        public void Initialize()
        {
            for (int i = 0; i < poolSize; i++)
            {
                poolQueue.Enqueue(CreateObject());
            }
        }
        public T GetObject()
        {
            if (poolQueue.Count > 0)
            {
                T obj = poolQueue.Dequeue();
                ObjectTaken(obj);
                return obj;
            }
            else
            {
                T obj = CreateObject();
                ObjectTaken(obj);
                return obj;
            }
        }

        public void ReturnObject(T obj)
        {
            ObjectReturned(obj);
            poolQueue.Enqueue(obj);
        }

        protected abstract T CreateObject();
        protected abstract void ObjectTaken(T obj);
        protected abstract void ObjectReturned(T obj);
    }
}
