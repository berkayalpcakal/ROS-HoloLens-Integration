using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityPlanPathActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public PlanPathActionClient planPathActionClient;
        public PathWaypointsReader pathWaypointsReader;

        public string actionName;
        public string status = "";
        public string feedback = "";
        public string result = "";

        private void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            planPathActionClient = new PlanPathActionClient(actionName, rosConnector.RosSocket);
            planPathActionClient.Initialize();
        }

        private void Update()
        {
            status = planPathActionClient.GetStatusString();
            feedback = planPathActionClient.GetFeedbackString();
            result = planPathActionClient.GetResultString();
        }

        public void RegisterGoal()
        {
            planPathActionClient.waypoints = pathWaypointsReader.waypoints;

        }

    }
}
