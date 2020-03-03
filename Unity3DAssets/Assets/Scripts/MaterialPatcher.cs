using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialPatcher : MonoBehaviour
{
    [SerializeField] private Material material = null;
    // Start is called before the first frame update
    void Start()
    {
        foreach (MeshRenderer mr in this.GetComponentsInChildren<MeshRenderer>())
        {
            if (mr != null)
            {
                mr.sharedMaterial = material;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {

    }
}
