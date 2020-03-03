using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [CustomEditor(typeof(UnityExecutePathActionClient))]
    public class ExecutePathActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityExecutePathActionClient)target).pathWaypointsReader.GetWaypoints();
                ((UnityExecutePathActionClient)target).RegisterGoal();
                ((UnityExecutePathActionClient)target).executePathActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityExecutePathActionClient)target).executePathActionClient.CancelGoal();
            }
        }
    }
}
