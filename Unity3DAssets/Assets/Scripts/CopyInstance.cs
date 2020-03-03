using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopyInstance : MonoBehaviour
{
    public GameObject objectToCopy;
    private GameObject clone;
    void Start()
    {
        Vector3 position = objectToCopy.transform.position;
        position.x += 1.0f;
        clone = Instantiate<GameObject>(objectToCopy, position, Quaternion.identity);
    }

}
