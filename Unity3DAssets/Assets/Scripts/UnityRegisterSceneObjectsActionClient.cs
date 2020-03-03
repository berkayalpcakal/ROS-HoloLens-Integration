using UnityEngine;

namespace RosSharp.RosBridgeClient.Actionlib
{
    [RequireComponent(typeof(RosConnector))]
    public class UnityRegisterSceneObjectsActionClient : MonoBehaviour
    {
        private RosConnector rosConnector;
        public RegisterSceneObjectsActionClient registerSceneObjectsActionClient;
        public SceneObjectReader sceneObjectReader;

        public string actionName;
        public string status = "";
        public string feedback = "";
        public string result = "";

        private void Start()
        {
            rosConnector = GetComponent<RosConnector>();
            registerSceneObjectsActionClient = new RegisterSceneObjectsActionClient(actionName, rosConnector.RosSocket);
            registerSceneObjectsActionClient.Initialize();
        }

        private void Update()
        {
            status = registerSceneObjectsActionClient.GetStatusString();
            feedback = registerSceneObjectsActionClient.GetFeedbackString();
            result = registerSceneObjectsActionClient.GetResultString();
        }

        public void RegisterGoal()
        {

            registerSceneObjectsActionClient.names = sceneObjectReader.names;
            registerSceneObjectsActionClient.positions_x = sceneObjectReader.positions_x;
            registerSceneObjectsActionClient.positions_y = sceneObjectReader.positions_y;
            registerSceneObjectsActionClient.positions_z = sceneObjectReader.positions_z;

            registerSceneObjectsActionClient.orientations_x = sceneObjectReader.orientations_x;
            registerSceneObjectsActionClient.orientations_y = sceneObjectReader.orientations_y;
            registerSceneObjectsActionClient.orientations_z = sceneObjectReader.orientations_z;
            registerSceneObjectsActionClient.orientations_w = sceneObjectReader.orientations_w;

            registerSceneObjectsActionClient.sizes_x = sceneObjectReader.sizes_x;
            registerSceneObjectsActionClient.sizes_y = sceneObjectReader.sizes_y;
            registerSceneObjectsActionClient.sizes_z = sceneObjectReader.sizes_z;

            registerSceneObjectsActionClient.mesh = sceneObjectReader.mesh;
        }

    }
}
