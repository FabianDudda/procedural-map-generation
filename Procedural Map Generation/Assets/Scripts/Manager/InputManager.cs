using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : Singleton<InputManager>
{
   
    void Update()
    {
        CameraMovement();
        ZoomCamera();
    }

    private void CameraMovement()
    {
        float xAxis = Input.GetAxis("Horizontal");
        float yAxis = Input.GetAxis("Vertical");

        Camera camera = Camera.main;
        camera.transform.position = new Vector3(camera.transform.position.x + xAxis, camera.transform.position.y + yAxis, camera.transform.position.z - 1);
    }

    private void ZoomCamera()
    {
        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 5, 25);  // ZoomSize between 5 and 25.
    }

}
