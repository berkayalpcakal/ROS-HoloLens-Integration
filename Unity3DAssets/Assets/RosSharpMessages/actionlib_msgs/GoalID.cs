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

namespace RosSharp.RosBridgeClient.MessageTypes.Actionlib
{
    public class GoalID : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "actionlib_msgs/GoalID";

        //  The stamp should store the time at which this goal was requested.
        //  It is used by an action server when it tries to preempt all
        //  goals that were requested before a certain time
        public Time stamp;
        //  The id provides a way to associate feedback and
        //  result message with specific goal requests. The id
        //  specified must be unique.
        public string id;

        public GoalID()
        {
            this.stamp = new Time();
            this.id = "";
        }

        public GoalID(Time stamp, string id)
        {
            this.stamp = stamp;
            this.id = id;
        }
    }
}