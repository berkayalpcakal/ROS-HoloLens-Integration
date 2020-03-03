using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public float rotationSpeed = 100.0f;
    public float movementSpeed = 1.0f;


    void Update() {
        if(Input.GetKey(KeyCode.W)) {
            //moving forward
            transform.position += this.transform.forward * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.S)) {
            //moving backward
            transform.position += -this.transform.forward * movementSpeed * Time.deltaTime;
        }
        if(Input.GetKey(KeyCode.A)) {
            //rotate left
            transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D)) {
            //rotate right
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        }

    }

}
