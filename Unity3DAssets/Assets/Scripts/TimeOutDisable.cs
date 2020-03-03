using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.Urdf;
using System;

namespace RosSharp.RosBridgeClient
{
    public class TimeOutDisable : MonoBehaviour
    {
        public float timer = 0;
        public float timeOutLimit = 10;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            if (timer > timeOutLimit)
            {
                Debug.Log("Time Out!");
                timer -= timeOutLimit;

            }
            timer += Time.deltaTime;

        }
    }
}
