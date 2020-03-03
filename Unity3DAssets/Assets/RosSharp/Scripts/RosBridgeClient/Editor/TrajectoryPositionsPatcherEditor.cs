/*
© Siemens AG, 2017-2018
Author: Dr. Martin Bischoff (martin.bischoff@siemens.com)

Licensed under the Apache License, Version 2.0 (the "License");
you may not use this file except in compliance with the License.
You may obtain a copy of the License at
<http://www.apache.org/licenses/LICENSE-2.0>.
Unless required by applicable law or agreed to in writing, software
distributed under the License is distributed on an "AS IS" BASIS,
WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
See the License for the specific language governing permissions and
limitations under the License.
*/

using UnityEngine;
using UnityEditor;

namespace RosSharp.RosBridgeClient
{
    [CustomEditor(typeof(TrajectoryPositionsPatcher))]
    public class TrajectoryPositionsPatcherEditor : Editor
    {
        private TrajectoryPositionsPatcher trajectoryPositionsPatcher;
        private static GUIStyle buttonStyle;

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (buttonStyle == null)
                buttonStyle = new GUIStyle(EditorStyles.miniButtonRight) { fixedWidth = 75 };

            trajectoryPositionsPatcher = (TrajectoryPositionsPatcher) target;
            
            GUILayout.Label("All Urdf Joints", EditorStyles.boldLabel);
            DisplaySettingsToggle(new GUIContent("Subs. Trajectory Pos.", "Adds/removes a Trajectory Positions Writer on each joint."),
                trajectoryPositionsPatcher.SetSubscribeTrajectoryPositions);
        }

        private delegate void SettingsHandler(bool enable);

        private static void DisplaySettingsToggle(GUIContent label, SettingsHandler handler)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.PrefixLabel(label);
            if (GUILayout.Button("Enable", buttonStyle))
                handler(true);
            if (GUILayout.Button("Disable", buttonStyle))
                handler(false);
            EditorGUILayout.EndHorizontal();
        }
    }
}