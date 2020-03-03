using System;
using RosSharp.RosBridgeClient.MessageTypes.MoveitTutorials;

namespace RosSharp.RosBridgeClient.Actionlib
{
    public class RegisterSceneObjectsActionClient : ActionClient
    <RegisterSceneObjectToMoveitAction, RegisterSceneObjectToMoveitActionGoal, RegisterSceneObjectToMoveitActionResult, 
     RegisterSceneObjectToMoveitActionFeedback, RegisterSceneObjectToMoveitGoal, RegisterSceneObjectToMoveitResult, RegisterSceneObjectToMoveitFeedback>
    {
        public string[] names;
        public double[] positions_x;
        public double[] positions_y;
        public double[] positions_z;
        public double[] orientations_x;
        public double[] orientations_y;
        public double[] orientations_z;
        public double[] orientations_w;
        public double[] sizes_x;
        public double[] sizes_y;
        public double[] sizes_z;
        public byte[] mesh;

        public string status = "";
        public string feedback = "";
        public string result = "";

        public RegisterSceneObjectsActionClient(string actionName, RosSocket rosSocket)
        {
            this.actionName = actionName;
            this.rosSocket = rosSocket;
            action = new RegisterSceneObjectToMoveitAction();
            goalStatus = new MessageTypes.Actionlib.GoalStatus();
        }

        protected override RegisterSceneObjectToMoveitActionGoal GetActionGoal()
        {
            action.action_goal.goal.names = names;
            action.action_goal.goal.positions_x = positions_x;
            action.action_goal.goal.positions_y = positions_y;
            action.action_goal.goal.positions_z = positions_z;

            action.action_goal.goal.orientations_x = orientations_x;
            action.action_goal.goal.orientations_y = orientations_y;
            action.action_goal.goal.orientations_z = orientations_z;
            action.action_goal.goal.orientations_w = orientations_w;

            action.action_goal.goal.sizes_x = sizes_x;
            action.action_goal.goal.sizes_y = sizes_y;
            action.action_goal.goal.sizes_z = sizes_z;

            action.action_goal.goal.mesh = mesh;

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