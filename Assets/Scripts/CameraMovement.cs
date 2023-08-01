using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    GameObject player;
    public float sensitivity = 0f;
    public float speed = 1.0f;
    public float speedR = 1.0f;
    Quaternion defaultRot;
    Vector3 defaultPos;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        defaultRot = transform.localRotation;
        defaultPos = transform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            float horizRot = Input.GetAxis("Mouse X");
            float vertRot = Input.GetAxis("Mouse Y");
            transform.RotateAround(player.transform.position, -Vector3.up, -horizRot * sensitivity);
            transform.RotateAround(player.transform.position, transform.right, -vertRot * sensitivity);
            Cursor.lockState = CursorLockMode.Confined;
        }
        else
        {
            float step = speed * Time.deltaTime;
            float stepR = speedR * Time.deltaTime;
            Cursor.lockState = CursorLockMode.None;
            transform.localRotation = Quaternion.RotateTowards(transform.localRotation, defaultRot, stepR);
            transform.localPosition = Vector3.MoveTowards(transform.localPosition, defaultPos, step);

        }
    }
}
