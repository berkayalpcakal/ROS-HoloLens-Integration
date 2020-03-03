/*
© PPG AG, 2020 Good Vibes Only
Author: Oguzhan, Mirac Berkay (TUM 2020)

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

using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class TrajectoryPositionsSubscriber : Subscriber<MessageTypes.MoveitTutorials.TrajectoryPositions>
    {
        public List<string> jointNames;
        public List<TrajectoryPositionsWriter> trajectoryPositionsWriters;

        protected override void Start()
        {
            base.Start();
            GetComponent<RosConnector>().RosSocket.Subscribe<Messages.Sensor.JointState>("/iiwa/ghost_joint_states", ReceiveGhostJointStates, (int)(TimeStep * 1000));
            GetComponent<RosConnector>().RosSocket.Subscribe<Messages.Sensor.JointState>("/iiwa/joint_states", ReceiveActualJointStates, (int)(TimeStep * 1000)); 
        }

        private void ReceiveGhostJointStates(Messages.Sensor.JointState jointState)
        {
            int index;
            for (int i = 0; i < jointState.name.Length; i++)
            {
                index = jointNames.IndexOf(jointState.name[i]);
                if (index != -1)
                {
                    trajectoryPositionsWriters[index].ghostJointState = (float)jointState.position[i];
                }
            }
        }

        private void ReceiveActualJointStates(Messages.Sensor.JointState jointState)
        {
            int index;
            for (int i = 0; i < jointState.name.Length; i++)
            {
                index = jointNames.IndexOf(jointState.name[i]);
                if (index != -1)
                {
                    trajectoryPositionsWriters[index].actualJointState = (float)jointState.position[i];

                }
            }
        }

        protected override void ReceiveMessage(MessageTypes.MoveitTutorials.TrajectoryPositions message)
        {
            List<float> durationInSecond = new List<float>();
            foreach (Messages.Standard.Duration duration in message.time_from_start)
            {
                durationInSecond.Add(duration.secs + (float)(duration.nsecs / 1e9));
            }

            int index;
            for (int i = 0; i < message.joint_states[0].name.Length; i++)
            {
                index = jointNames.IndexOf(message.joint_states[0].name[i]);
                if (index != -1)
                {
                    List<float> trajectory = new List<float>();
                    foreach (Messages.Sensor.JointState joint_state in message.joint_states)
                    {
                        trajectory.Add((float)joint_state.position[index]);
                    }
                    trajectoryPositionsWriters[index].Write(trajectory.ToArray(), durationInSecond.ToArray());
                }
            } 
        }
    }
}