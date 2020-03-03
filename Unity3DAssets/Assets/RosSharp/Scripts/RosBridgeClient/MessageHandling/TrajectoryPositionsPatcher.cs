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

using UnityEngine;
using System.Collections.Generic;
using RosSharp.Urdf;

namespace RosSharp.RosBridgeClient
{
    public class TrajectoryPositionsPatcher : MonoBehaviour
    {
        public UrdfRobot ghostRobot;
        public UrdfRobot referenceRobot;
       
        public void SetSubscribeTrajectoryPositions(bool subscribe)
        {
            if (subscribe)
            {
                TrajectoryPositionsSubscriber trajectoryPositionsSubscriber = transform.AddComponentIfNotExists<TrajectoryPositionsSubscriber>();
                trajectoryPositionsSubscriber.trajectoryPositionsWriters = new List<TrajectoryPositionsWriter>();
                trajectoryPositionsSubscriber.jointNames = new List<string>();

                foreach (UrdfJoint urdfJoint in ghostRobot.GetComponentsInChildren<UrdfJoint>())
                {
                    if (urdfJoint.JointType != UrdfJoint.JointTypes.Fixed)
                    {
                        trajectoryPositionsSubscriber.trajectoryPositionsWriters.Add(urdfJoint.transform.AddComponentIfNotExists<TrajectoryPositionsWriter>());
                        trajectoryPositionsSubscriber.jointNames.Add(urdfJoint.JointName);
                        foreach (UrdfJoint referenceRobotJoint in referenceRobot.GetComponentsInChildren<UrdfJoint>())
                        {
                            if (referenceRobotJoint.JointName == urdfJoint.JointName)
                            {
                                trajectoryPositionsSubscriber.trajectoryPositionsWriters[trajectoryPositionsSubscriber.trajectoryPositionsWriters.Count - 1].referenceJoint = referenceRobotJoint;
                            }
                        }
                    }
                }
            }
            else
            {
                GetComponent<TrajectoryPositionsSubscriber>()?.trajectoryPositionsWriters.Clear();
                GetComponent<TrajectoryPositionsSubscriber>()?.jointNames.Clear();

                foreach (TrajectoryPositionsWriter writer in ghostRobot.GetComponentsInChildren<TrajectoryPositionsWriter>())
                    writer.transform.DestroyImmediateIfExists<TrajectoryPositionsWriter>();
            }
        }
    }
}
