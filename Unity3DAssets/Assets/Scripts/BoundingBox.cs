using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RosSharp.RosBridgeClient
{
    public class BoundingBox : MonoBehaviour
    {
        public Vector3[] sizes { get; private set; }
        public Vector3[] centers { get; private set; }
        public Vector3[] localSizes { get; private set; }
        public Vector3[] localCenters { get; private set; }
        public Quaternion[] orientations { get; private set; }
        public Matrix4x4[] localToWorldMatrices { get; private set; }
        public Vector3 humanScaleFactor = Vector3.one;
        public int numMeshComponents = 0;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            for (int i = 0; i < numMeshComponents; i++)
            {
                Gizmos.matrix = localToWorldMatrices[i];
                Gizmos.DrawWireCube(localCenters[i], localSizes[i]);
            }
        }

        void Start()
        {
            numMeshComponents = GetComponentsInChildren<MeshFilter>().Length;
            if (numMeshComponents == 0)
            {
                numMeshComponents = GetComponentsInChildren<SkinnedMeshRenderer>().Length;
            }
            sizes = new Vector3[numMeshComponents];
            centers = new Vector3[numMeshComponents];
            localSizes = new Vector3[numMeshComponents];
            localCenters = new Vector3[numMeshComponents];
            orientations = new Quaternion[numMeshComponents];
            localToWorldMatrices = new Matrix4x4[numMeshComponents];
        }

        void Update()
        {
            MeshFilter[] mfArray = GetComponentsInChildren<MeshFilter>();
            if (mfArray.Length > 0)
            {
                for (int i = 0; i < mfArray.Length; i++)
                {
                    MeshFilter mf = mfArray[i];
                    Mesh m = mf.mesh;

                    localSizes[i] = m.bounds.size;
                    localCenters[i] = m.bounds.center;
                    localToWorldMatrices[i] = mf.transform.localToWorldMatrix;
                    orientations[i] = localToWorldMatrices[i].rotation;
                    centers[i] = localToWorldMatrices[i].MultiplyPoint3x4(localCenters[i]);
                    sizes[i] = Vector3.Scale(localToWorldMatrices[i].lossyScale, localSizes[i]);
                }
            }
            else
            {
                SkinnedMeshRenderer[] smrArray = GetComponentsInChildren<SkinnedMeshRenderer>();
                for (int i = 0; i < smrArray.Length; i++)
                {
                    SkinnedMeshRenderer smr = smrArray[i];

                    localSizes[i] = Vector3.Scale(smr.bounds.size, humanScaleFactor);
                    localCenters[i] = smr.bounds.center;
                    localToWorldMatrices[i] = Matrix4x4.identity;
                    orientations[i] = localToWorldMatrices[i].rotation;
                    centers[i] = localToWorldMatrices[i].MultiplyPoint3x4(localCenters[i]);
                    sizes[i] = Vector3.Scale(Vector3.Scale(localToWorldMatrices[i].lossyScale, localSizes[i]), humanScaleFactor);
                }
            }
        }

    }
}