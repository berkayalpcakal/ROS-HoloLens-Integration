using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityExecutePathActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public ExecutePathActionClient executePathActionClient;
        public PathWaypointsReader pathWaypointsReader;

        public string actionName;
        public string status = "";
        public string feedback = "";
        public string result = "";

        private void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            executePathActionClient = new ExecutePathActionClient(actionName, rosConnector.RosSocket);
            executePathActionClient.Initialize();
        }

        private void Update()
        {
            status = executePathActionClient.GetStatusString();
            feedback = executePathActionClient.GetFeedbackString();
            result = executePathActionClient.GetResultString();
        }

        public void RegisterGoal()
        {
            executePathActionClient.waypoints = pathWaypointsReader.waypoints;

        }

    }
}
