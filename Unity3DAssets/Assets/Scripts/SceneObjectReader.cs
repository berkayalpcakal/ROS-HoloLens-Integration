using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class SceneObjectReader : MonoBehaviour
    {
        public bool sendBoundingBoxes = false;

        public Transform ref_transform;
        public SpatialMeshExportManager spatialMeshExportManager;
        private BoundingBox[] boundingBoxes;

        public string[] names;
        public double[] positions_x;
        public double[] positions_y;
        public double[] positions_z;
        public double[] orientations_x;
        public double[] orientations_y;
        public double[] orientations_z;
        public double[] orientations_w;
        public double[] sizes_x;
        public double[] sizes_y;
        public double[] sizes_z;
        public byte[] mesh;

        private int numOfBoxes = 0;

        void Start()
        {
            Initialize();
            mesh = new byte[0];
        }

        private void Initialize()
        {
            for (int i = 0; i < numOfBoxes; i++)
            {
                positions_x = new double[numOfBoxes];
                positions_y = new double[numOfBoxes];
                positions_z = new double[numOfBoxes];

                orientations_x = new double[numOfBoxes];
                orientations_y = new double[numOfBoxes];
                orientations_z = new double[numOfBoxes];
                orientations_w = new double[numOfBoxes];

                sizes_x = new double[numOfBoxes];
                sizes_y = new double[numOfBoxes];
                sizes_z = new double[numOfBoxes];

                names = new string[numOfBoxes];
            }          
        }

        public void GetMesh()
        {
            mesh = spatialMeshExportManager.ExportMesh();
        }

        public void GetBoundingBoxes()
        {
            numOfBoxes = 0;
            boundingBoxes = FindObjectsOfType<BoundingBox>();

            for (int i = 0; i < boundingBoxes.Length; i++)
            {
                MeshRenderer[] meshRenderers = boundingBoxes[i].gameObject.GetComponentsInChildren<MeshRenderer>();
                foreach(MeshRenderer mr in meshRenderers)
                {
                    if(mr.enabled)
                    {
                        numOfBoxes++;
                    }
                }
            }

            Initialize();

            for (int i = 0, idx = 0; i < boundingBoxes.Length; i++)
            {
                MeshRenderer[] meshRenderers = boundingBoxes[i].gameObject.GetComponentsInChildren<MeshRenderer>();

                for (int j = 0; j < boundingBoxes[i].numMeshComponents; j++)
                {
                    if (meshRenderers[j].enabled)
                    {
                        Vector3 temp_pose = (Quaternion.Inverse(ref_transform.rotation) * (boundingBoxes[i].centers[j] - ref_transform.position)).Unity2Ros();
                        //temp_pose.y *= -1; temp_pose.x *= -1;
                        positions_x[idx] = temp_pose.x;
                        positions_y[idx] = temp_pose.y;
                        positions_z[idx] = temp_pose.z;

                        Quaternion temp_q = (Quaternion.Inverse(ref_transform.rotation) * boundingBoxes[i].orientations[j]).Unity2Ros();
                        orientations_x[idx] = temp_q.x;
                        orientations_y[idx] = temp_q.y;
                        orientations_z[idx] = temp_q.z;
                        orientations_w[idx] = temp_q.w;

                        Vector3 temp_size = boundingBoxes[i].sizes[j].Unity2Ros();
                        sizes_x[idx] = Math.Abs(temp_size.x);
                        sizes_y[idx] = Math.Abs(temp_size.y);
                        sizes_z[idx] = Math.Abs(temp_size.z);

                        names[idx] = idx.ToString();
                        idx++;
                    }
                }
            }
        }
       

    }
}