using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RosSharp.RosBridgeClient.Messages.Geometry;

namespace RosSharp.RosBridgeClient
{
    public class PathWaypointsReader : MonoBehaviour
    {
        public GameObject markerObject;
        public GameObject refGameObject;

        public Messages.Geometry.Pose[] waypoints;

        private int numOfWaypoints;
        private List<GameObject> waypointObjects = new List<GameObject>();

        void Start()
        {
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < numOfWaypoints; i++)
            {
                waypoints = new Messages.Geometry.Pose[numOfWaypoints];
            }
        }

        public void RegisterWaypoint()
        {
            GameObject waypoint = (GameObject)Instantiate(Resources.Load("WaypointArrow"));
            waypoint.GetComponentInChildren<MeshRenderer>().material.color = new Color(0.0f, 1.0f, 0.0f, 87.0f / 255.0f);
            waypoint.transform.SetPositionAndRotation(markerObject.transform.position, markerObject.transform.rotation);
            waypoint.transform.localScale = markerObject.transform.lossyScale;
            waypointObjects.Add(waypoint);
        }

        public void GetWaypoints()
        {
            numOfWaypoints = waypointObjects.Count;

            Initialize();

            for (int i = 0; i < numOfWaypoints; i++)
            {
                UnityEngine.Vector3 temp_pose = (UnityEngine.Quaternion.Inverse(refGameObject.transform.rotation) * (waypointObjects[i].transform.position - refGameObject.transform.position)).Unity2Ros();
                UnityEngine.Quaternion temp_q = UnityEngine.Quaternion.Inverse(refGameObject.transform.rotation) * waypointObjects[i].transform.rotation;
                temp_q *= UnityEngine.Quaternion.Euler(0.0f, 0.0f, 180.0f);
                temp_q = temp_q.Unity2Ros();

                waypoints[i] = new Messages.Geometry.Pose
                {
                    position = new Point { x = temp_pose.x, y = temp_pose.y, z = temp_pose.z },
                    orientation = new Messages.Geometry.Quaternion { x = temp_q.x, y = temp_q.y, z = temp_q.z, w = temp_q.w }
                };
            }
        }

        public void ClearTargets()
        {
            numOfWaypoints = waypointObjects.Count;
            for (int i = 0; i < numOfWaypoints; i++)
            {
                Destroy(waypointObjects[i]);
            }
            waypointObjects.Clear();
        }

    }
}