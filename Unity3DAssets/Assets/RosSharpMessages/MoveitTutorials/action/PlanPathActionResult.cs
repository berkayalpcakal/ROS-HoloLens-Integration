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
    public class PlanPathActionResult : ActionResult<ExecutePathResult>
    {
        [JsonIgnore]
        public const string RosMessageName = "moveit_tutorials/PlanPathActionResult";

        public PlanPathActionResult() : base()
        {
            this.result = new ExecutePathResult();
        }

        public PlanPathActionResult(Header header, GoalStatus status, ExecutePathResult result) : base(header, status)
        {
            this.result = result;
        }
    }
}