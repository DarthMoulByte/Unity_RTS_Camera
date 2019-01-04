using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RTS_Camera : MonoBehaviour
{
    public CameraType _camType;
    public enum CameraType {
        FollowTarget,
        FreeMovement
    }

    [Header("Follow target")]
    public Transform _target;
    public float camSpeedFollow = 2f;

    [Header("Free movement")]
    public float camSpeedMovement = 2f;

    [Header("Camera zoom")]
    public float maxFov = 60f;
    public float minFov = 45f;

    public float zoomSpeed = 1f;

    float lerpFov = 0;
    float curFov = 0;
    Camera _cam;

    private void Start()
    {
        //get camera component from child
        _cam = gameObject.GetComponentInChildren<Camera>();

        //update camera fov
        curFov = maxFov;
        lerpFov = curFov;
        _cam.fieldOfView = curFov;
    }

    void Update()
    {
        ZoomCamera();
        MoveCamera();

        //change camera type when press 'C' key
        if (Input.GetKeyDown(KeyCode.C))
        {
            if (_camType == CameraType.FollowTarget)
            {
                _camType = CameraType.FreeMovement;
            }
            else if (_camType == CameraType.FreeMovement)
            {
                _camType = CameraType.FollowTarget;
            }
        }
    }

    void ZoomCamera()
    {
        //calculate fov
        curFov += Input.GetAxis("Mouse ScrollWheel") * zoomSpeed;

        //clamp fov value
        curFov = Mathf.Clamp(curFov, minFov, maxFov);

        //lerp fov
        lerpFov = Mathf.Lerp(lerpFov, curFov, Time.deltaTime * 4);

        //apply fov value to camera
        _cam.fieldOfView = lerpFov;
    }

    void MoveCamera()
    {
        if (_camType == CameraType.FollowTarget)
        {
            transform.position = Vector3.Lerp(transform.position, _target.position, Time.fixedDeltaTime * camSpeedFollow);
        }
        else if (_camType == CameraType.FreeMovement)
        {
            if (Input.mousePosition.x > Screen.width - (Screen.width / 20))//move camera right
            {
                transform.Translate(Vector3.right * Time.deltaTime * camSpeedMovement);
            }
            else if (Input.mousePosition.x < Screen.width / 20)//move camera left
            {
                transform.Translate(Vector3.left * Time.deltaTime * camSpeedMovement);
            }

            if (Input.mousePosition.y > Screen.height - (Screen.height / 15))//move camera up
            {
                transform.Translate(Vector3.forward * Time.deltaTime * camSpeedMovement);
            }
            else if (Input.mousePosition.y < Screen.height / 15)//move camera down
            {
                transform.Translate(Vector3.back * Time.deltaTime * camSpeedMovement);
            }
        }
    }
}
