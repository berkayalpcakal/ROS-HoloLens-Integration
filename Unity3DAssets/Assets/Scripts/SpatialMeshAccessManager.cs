using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.SpatialAwareness;

public class SpatialMeshAccessManager : MonoBehaviour
{
    private List<SpatialAwarenessMeshObject> spatialAwarenessMeshObjects = null;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public List<MeshFilter> GetSpatialMappingMesh()
    {
        List<MeshFilter> meshFilters = new List<MeshFilter>();

        var spatialAwarenessService  = CoreServices.SpatialAwarenessSystem as IMixedRealityDataProviderAccess;
        var spatialMapDataProvider   = spatialAwarenessService.GetDataProvider<IMixedRealitySpatialAwarenessMeshObserver>();
        spatialAwarenessMeshObjects  = spatialMapDataProvider.Meshes.Values.ToList();
        foreach(var spatialAwarenessMeshObject in spatialAwarenessMeshObjects) 
        {
            meshFilters.Add(spatialAwarenessMeshObject.Filter);
        }

        return meshFilters;
    }
}
