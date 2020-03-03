using UnityEditor;
using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{ 
    [CustomEditor(typeof(UnityRegisterSceneObjectsActionClient))]
    public class RegisterSceneObjectsActionClientEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Send Goal"))
            {
                ((UnityRegisterSceneObjectsActionClient)target).sceneObjectReader.GetBoundingBoxes();
                ((UnityRegisterSceneObjectsActionClient)target).sceneObjectReader.GetMesh();
                ((UnityRegisterSceneObjectsActionClient)target).RegisterGoal();
                ((UnityRegisterSceneObjectsActionClient)target).registerSceneObjectsActionClient.SendGoal();
            }

            if (GUILayout.Button("Cancel Goal"))
            {
                ((UnityRegisterSceneObjectsActionClient)target).registerSceneObjectsActionClient.CancelGoal();
            }
        }
    }
}
