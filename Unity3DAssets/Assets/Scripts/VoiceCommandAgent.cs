using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Windows.Speech;
using Vuforia;
using RosSharp.Urdf;

namespace RosSharp.RosBridgeClient
{
    public class VoiceCommandAgent : MonoBehaviour
    {

        private float timeout = 10;
        private KeywordRecognizer keywordRecognizer;
        private Dictionary<string, Action> actions = new Dictionary<string, Action>();
        public GameObject rangeSmall;
        public GameObject rangeBig;

        [SerializeField] private GameObject[] objectsToClone = null;
        [SerializeField] private GameObject   rosConnector   = null;
        [SerializeField] private String       RealRobotName  = null;
        [SerializeField] private String       GhostRobotName = null;
    
        void Start()
        {
            actions.Add("disable vuforia",    DisableVuforia);
            actions.Add("enable vuforia",     EnableVuforia);
            actions.Add("plan trajectory",    PlanTrajectory);
            actions.Add("execute trajectory", ExecuteTrajectory);
            actions.Add("set target",         SetTargetPose);
            actions.Add("clear targets",      ClearTargetPoses);
            actions.Add("enable range",       EnableRange);
            actions.Add("disable range",      DisableRange);


            keywordRecognizer = new KeywordRecognizer(actions.Keys.ToArray());
            keywordRecognizer.OnPhraseRecognized += RecognizedSpeech;
            keywordRecognizer.Start();
        }

        private void RecognizedSpeech(PhraseRecognizedEventArgs speech)
        {
            actions[speech.text].Invoke();
        }

        private void DisableVuforia()
        {
            UrdfRobot RealRobot  = null;
            UrdfRobot GhostRobot = null;

            foreach (GameObject objectToClone in objectsToClone)
            {
                GameObject cloneObject = Instantiate<GameObject>(objectToClone);
                cloneObject.transform.parent = null;
                cloneObject.transform.localScale = objectToClone.transform.lossyScale;
                cloneObject.transform.position   = objectToClone.transform.position;
                cloneObject.transform.rotation   = objectToClone.transform.rotation;
                if (cloneObject.name == RealRobotName)
                {
                    RealRobot = cloneObject.GetComponent<UrdfRobot>();
                }
                else if (cloneObject.name == GhostRobotName)
                {
                    GhostRobot = cloneObject.GetComponent<UrdfRobot>();
                }
            }

            VuforiaBehaviour.Instance.enabled = false;

            if (RealRobot)
            {
                JointStatePatcher jsp = rosConnector.GetComponent<JointStatePatcher>();
                jsp.UrdfRobot = RealRobot;
                jsp.SetSubscribeJointStates(true);
            }

            if (GhostRobot)
            {
                TrajectoryPositionsPatcher tpp = rosConnector.GetComponent<TrajectoryPositionsPatcher>();
                tpp.ghostRobot= GhostRobot;
                tpp.SetSubscribeTrajectoryPositions(true);
            }

        }

        private void EnableRange()
        {
            rangeSmall.GetComponent<MeshRenderer>().enabled = true;
            rangeBig.GetComponent<MeshRenderer>().enabled = true;
        }

        private void DisableRange()
        {
            rangeSmall.GetComponent<MeshRenderer>().enabled = false;
            rangeBig.GetComponent<MeshRenderer>().enabled = false;
        }

        private void EnableVuforia()
        {
            VuforiaBehaviour.Instance.enabled = true;
        }

        private void PlanTrajectory()
        {
            rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().sceneObjectReader.GetBoundingBoxes();
            rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().sceneObjectReader.GetMesh();
            rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().RegisterGoal();
            rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().registerSceneObjectsActionClient.SendGoal();

            string status = rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().registerSceneObjectsActionClient.status;

            //float startTime = Time.realtimeSinceStartup;
            //float endTime = timeout + startTime;

            //while (status!="SUCCEEDED" && Time.realtimeSinceStartup < endTime) 
            //{
                status = rosConnector.GetComponent<Actionlib.UnityRegisterSceneObjectsActionClient>().registerSceneObjectsActionClient.status;
            //}

            rosConnector.GetComponent<Actionlib.UnityPlanPathActionClient>().pathWaypointsReader.GetWaypoints();
            rosConnector.GetComponent<Actionlib.UnityPlanPathActionClient>().RegisterGoal();
            rosConnector.GetComponent<Actionlib.UnityPlanPathActionClient>().planPathActionClient.SendGoal();

            GameObject ghostRobot = GameObject.Find(GhostRobotName);
            if (ghostRobot == null)
            {
                ghostRobot = GameObject.Find(GhostRobotName + "(Clone)");
            }
            if (ghostRobot!=null)
            {
                foreach(MeshRenderer mr in ghostRobot.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = true;
                }
            }
        }

        private void ExecuteTrajectory()
        {
            GameObject ghostRobot = GameObject.Find(GhostRobotName);
            if (ghostRobot == null)
            {
                ghostRobot = GameObject.Find(GhostRobotName + "(Clone)");
            }
            if (ghostRobot != null)
            {
                foreach (MeshRenderer mr in ghostRobot.GetComponentsInChildren<MeshRenderer>())
                {
                    mr.enabled = false;
                }
            }
            rosConnector.GetComponent<Actionlib.UnityExecutePathActionClient>().pathWaypointsReader.GetWaypoints();
            rosConnector.GetComponent<Actionlib.UnityExecutePathActionClient>().RegisterGoal();
            rosConnector.GetComponent<Actionlib.UnityExecutePathActionClient>().executePathActionClient.SendGoal();
        }

        private void SetTargetPose()
        {
            rosConnector.GetComponent<Actionlib.UnityExecutePathActionClient>().pathWaypointsReader.RegisterWaypoint();
        }

        private void ClearTargetPoses()
        {
            rosConnector.GetComponent<Actionlib.UnityExecutePathActionClient>().pathWaypointsReader.ClearTargets();
        }
    }

}
