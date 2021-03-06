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
    public class RegisterSceneObjectToMoveitActionResult : ActionResult<RegisterSceneObjectToMoveitResult>
    {
        [JsonIgnore]
        public const string RosMessageName = "moveit_tutorials/RegisterSceneObjectToMoveitActionResult";

        public RegisterSceneObjectToMoveitActionResult() : base()
        {
            this.result = new RegisterSceneObjectToMoveitResult();
        }

        public RegisterSceneObjectToMoveitActionResult(Header header, GoalStatus status, RegisterSceneObjectToMoveitResult result) : base(header, status)
        {
            this.result = result;
        }
    }
}
