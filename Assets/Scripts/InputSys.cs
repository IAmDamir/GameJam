using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSys : MonoBehaviour
{
    private NewControls controls;
    public InputAction inp;

    private Vector2 wasdInput;
    private Vector3 wsMovement;
    private Vector3 adMovement;

    public Vector3 direction;
    public bool _dash;
    public bool _bow;

    private Vector3 mousePos;

    private Ray cameraRay;                // The ray that is cast from the camera to the mouse position
    private RaycastHit cameraRayHit;    // The object that the ray hits
    public Transform player;

    public Camera cam;
    private float defaultFov = 90;
    private float zoomDuration = 0.5f;
    private float speed = 100;
    private float sensitivity = 2;
    private float maxZoom = 60;
    private float minZoom = 30;
    private float zoomPosition;

    private void Awake()
    {
        controls = new NewControls();
    }

    protected void OnEnable()
    {
        controls.Enable();
        inp.Enable();
    }

    protected void OnDisable()
    {
        controls.Disable();
        inp.Disable();
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        Zoom();
    }

    public void IsometricMovement(Vector3 frwrd, Vector3 rght)
    {
        wasdInput = inp.ReadValue<Vector2>();

        wsMovement = frwrd * wasdInput.y;
        adMovement = rght * wasdInput.x;

        direction = Vector3.Normalize(wsMovement + adMovement);
    }

    public void Dash()
    {
        _dash = controls.Actions.Dash.triggered;
    }

    public void Bow()
    {
        _bow = /*controls.Actions.Bow.triggered;
        bool bowReleased =*/ controls.Actions.Bow.WasReleasedThisFrame();
        /*if (bowReleased)
        {
            Debug.Log(Time.time);
        }*/
    }

    public Vector3 MouseWorldPos()
    {
        // Cast a ray from the camera to the mouse cursor
        cameraRay = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        Vector3 targetPosition = new Vector3();

        // If the ray strikes an object...
        if (Physics.Raycast(cameraRay, out cameraRayHit))
        {
            // ...make the cube rotate (only on the Y axis) to face the ray hit's position
            targetPosition = new Vector3(cameraRayHit.point.x, player.position.y, cameraRayHit.point.z);
        }
        return targetPosition;
    }

    public void Zoom()
    {
        float zoom = controls.Actions.Zoom.ReadValue<float>();
        zoom = -zoom;
        //zoom /= 120;

        if (zoom != 0)
        {
            zoom = zoom * sensitivity;
            zoom = Mathf.Clamp(zoom, minZoom, maxZoom);
            zoomPosition = Mathf.MoveTowards(zoomPosition, zoom, speed * Time.fixedDeltaTime);
            float angle = Mathf.Abs((defaultFov / zoomPosition) - defaultFov);

            cam.fieldOfView = Mathf.MoveTowards(cam.fieldOfView, zoomPosition, angle / zoomDuration * Time.fixedDeltaTime);
        }
    }
}