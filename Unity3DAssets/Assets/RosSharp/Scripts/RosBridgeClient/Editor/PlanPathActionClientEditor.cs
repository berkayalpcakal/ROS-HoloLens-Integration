using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [CustomEditor(typeof(UnityPlanPathActionClient))]
    public class PlanPathActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityPlanPathActionClient)target).pathWaypointsReader.GetWaypoints();
                ((UnityPlanPathActionClient)target).RegisterGoal();
                ((UnityPlanPathActionClient)target).planPathActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityPlanPathActionClient)target).planPathActionClient.CancelGoal();
            }
        }
    }
}
