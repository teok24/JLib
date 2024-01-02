using System;
using System.Collections;
using System.Collections.Generic;

namespace JLib.Utils.Interfaces
{
    public interface IPoolEvents<T>
    {
        public T Create();
        public void OnGet(T item);
        public void OnRelease(T item);
    }
}
