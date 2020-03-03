using System;
using RosSharp.RosBridgeClient.MessageTypes.MoveitTutorials;
using RosSharp.RosBridgeClient.Messages.Geometry;

namespace RosSharp.RosBridgeClient.Actionlib
{
    public class ExecutePathActionClient : ActionClient
    <ExecutePathAction, ExecutePathActionGoal, ExecutePathActionResult,
     ExecutePathActionFeedback, ExecutePathGoal, ExecutePathResult, ExecutePathFeedback>
    {
        public Pose[] waypoints;

        public string status = "";
        public string feedback = "";
        public string result = "";

        public ExecutePathActionClient(string actionName, RosSocket rosSocket)
        {
            this.actionName = actionName;
            this.rosSocket = rosSocket;
            action = new ExecutePathAction();
            goalStatus = new MessageTypes.Actionlib.GoalStatus();
        }

        protected override ExecutePathActionGoal GetActionGoal()
        {
            action.action_goal.goal.waypoints = waypoints;

            return action.action_goal;
        }

        protected override void OnStatusUpdated()
        {
        }

        protected override void OnFeedbackReceived()
        {
        }

        protected override void OnResultReceived()
        {
        }

        public string GetStatusString()
        {
            if (goalStatus != null)
            {
                return ((ActionStatus)(goalStatus.status)).ToString();
            }
            return "";
        }

        public string GetFeedbackString()
        {
            if (action != null)
                return action.action_feedback.feedback.log;
            return "";
        }

        public string GetResultString()
        {
            if (action != null)
                return action.action_result.result.log;
            return "";
        }
    }
}