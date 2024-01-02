using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JLib.Utils
{
    /// <summary>
    /// A simple billboard class with the option of initializing a transform to look at.
    /// If no transform is passed in, it will look at the main camera.
    /// </summary>
    public class Billboard : MonoBehaviour
    {
        Transform mainTransform;
        void Start()
        {
            mainTransform = Camera.main.transform;
        }
        public void Initialize(Transform transform)
        {
            mainTransform = transform;
        }
        // Update is called once per frame
        void Update()
        {
            transform.LookAt(transform.position + mainTransform.rotation * Vector3.forward, mainTransform.rotation * Vector3.up);
        }
    }
}
