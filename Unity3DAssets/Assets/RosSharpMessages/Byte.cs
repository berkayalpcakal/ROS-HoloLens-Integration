using Newtonsoft.Json;

namespace RosSharp.RosBridgeClient.Messages.Standard
{
    public class Byte : Message
    {
        [JsonIgnore]
        public const string RosMessageName = "std_msgs/Float64";
        public double data;

        public Byte()
        {
            this.data = 0;
        }

        public Byte(double data)
        {
            this.data = data;
        }
    }
}