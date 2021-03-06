/* 
 * This message is auto generated by ROS#. Please DO NOT modify.
 * Note:
 * - Comments from the original code will be written in their own line 
 * - Variable sized arrays will be initialized to array of size 0 
 * Please report any issues at 
 * <https://github.com/siemens/ros-sharp> 
 */

using Newtonsoft.Json;

using RosSharp.RosBridgeClient.Messages.Standard;
using RosSharp.RosBridgeClient.MessageTypes.Actionlib;

namespace RosSharp.RosBridgeClient.MessageTypes.MoveitTutorials
{
    public class ExecutePathActionFeedback : ActionFeedback<ExecutePathFeedback>
    {
        [JsonIgnore]
        public const string RosMessageName = "moveit_tutorials/ExecutePathActionFeedback";

        public ExecutePathActionFeedback() : base()
        {
            this.feedback = new ExecutePathFeedback();
        }

        public ExecutePathActionFeedback(Header header, GoalStatus status, ExecutePathFeedback feedback) : base(header, status)
        {
            this.feedback = feedback;
        }
    }
}
