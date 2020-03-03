/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using RosSharp.Urdf;
using System.Threading;
using UnityEngine;
using Joint = UnityEngine.Joint;

namespace RosSharp.RosBridgeClient
{
    [RequireComponent(typeof(Joint)), RequireComponent(typeof(UrdfJoint))]
    public class TrajectoryPositionsWriter : MonoBehaviour
    {
        private UrdfJoint urdfJoint;
        public UrdfJoint referenceJoint;
        public float actualJointState;
        public float ghostJointState;

        private float[] trajectory; 
        private float[] duration;            
        private bool isMsgReset, isAnimationStart;
        private float timer, newState, prevState;
        private int intervalIndex;

        private void Start()
        {
            urdfJoint = GetComponent<UrdfJoint>();
            isMsgReset = false;
        }
    
        private void Update()
        {

            if (isAnimationStart)
            {
                WriteUpdate();
            }
            else
            {
                ResetPosition();
            }
            
        }

        private void ResetPosition()
        {
            if (!isAnimationStart)
            {
                newState = actualJointState;
            }

            if (trajectory != null)
                urdfJoint.UpdateJointState(trajectory[0] - newState);
        }

        private void WriteUpdate()
        {
            if(isMsgReset)
            {
                ResetPosition();
                isMsgReset = false;
                prevState = trajectory[0];
                intervalIndex = 0;
                timer = 0;
                return;
            }
           
            timer += Time.deltaTime;
            if (timer > duration[duration.Length - 1])
            {
                timer = 0;
                intervalIndex = 0;
                ResetPosition();
                prevState = trajectory[0];
            }

            if (timer > duration[intervalIndex + 1])
            {
                intervalIndex += 1;
            }
            float timeDelta = (timer - duration[intervalIndex]) / (duration[intervalIndex + 1] - duration[intervalIndex]);
            newState = InterpolatePosition(timeDelta, trajectory[intervalIndex], trajectory[intervalIndex + 1]);
            urdfJoint.UpdateJointState(newState - prevState);
            prevState = newState;
            
        }

        private float InterpolatePosition(float timeDelta, float startingPos, float endingPos)
        {
            return startingPos + (endingPos - startingPos) * timeDelta;
        }

        public void Write(float[] traj, float[] dura)
        {
            isMsgReset = true;
            isAnimationStart = true;
            trajectory = traj;
            duration = dura;

            Debug.Log("Message is receieved by the joint.");
        }
    }
}