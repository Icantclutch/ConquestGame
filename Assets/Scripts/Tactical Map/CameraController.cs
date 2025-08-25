using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    private CameraControl CameraActions;
    private InputAction CameraMovement;
    private Transform CameraTransform;

    //Horizontal Motion
    [SerializeField]
    private float MaxSpeed = 50f;
    private float speed;

    [SerializeField]
    private float Accleration = 10f;

    [SerializeField]
    private float Damping = 15f;

    //Vertical Motion
    [SerializeField]
    private float StepSize = 2f;

    [SerializeField]
    private float ZoomDamping = 7.5f;

    [SerializeField]
    private float MinHeight = 5f;

    [SerializeField]
    private float MaxHeight = 50f;

    [SerializeField]
    private float ZoomSpeed = 2f;

    [SerializeField]
    private float ZoomStepSize = 2f;


    //Rotation
    [SerializeField]
    private float MaxRotationSpeed = 0.25f;

    //Value set in various functions
    //Used to update the pos of camera base object
    private Vector3 TargetPosition;

    private float ZoomHeight;

    //Track velocity w/o rigidbody
    private Vector3 HorizontalVelocity;
    private Vector3 LastPosition;

    private Vector3 StartDrag;

    /*
     * The Look Down angle is determined by the starting Height and Offset from 0 (x or z) of the camera object. Ex: Y = 50 and Z = 50 will result in a 45 degree look down angle
     */


    private void Awake()
    {
        CameraActions = new CameraControl();
        CameraTransform = GetComponentInChildren<Camera>().transform;
    }

    private void OnEnable()
    {
        ZoomHeight = CameraTransform.localPosition.y;
        CameraTransform.LookAt(transform);

        LastPosition = transform.position;
        CameraMovement = CameraActions.Camera.Movement;
        CameraActions.Camera.RotateCamera.performed += RotateCamera;
        CameraActions.Camera.ZoomCamera.performed += ZoomCamera;
        CameraActions.Camera.Enable();
    }

    private void OnDisable()
    {
        CameraActions.Camera.RotateCamera.performed -= RotateCamera;
        CameraActions.Camera.ZoomCamera.performed -= ZoomCamera;
        CameraActions.Disable();
    }


    private void Update()
    {
        GetKeyboardMovement();

        UpdateVelocity();
        UpdateCameraPosition();
        UpdateBasePosition();  
    }

    private void UpdateVelocity()
    {
        HorizontalVelocity = (transform.position - LastPosition) / Time.deltaTime;
        HorizontalVelocity.y = 0;
        LastPosition = transform.position;
    }

    private void GetKeyboardMovement()
    {
        Vector3 inputValue = CameraMovement.ReadValue<Vector2>().x * GetCameraRight() + CameraMovement.ReadValue<Vector2>().y * GetCameraForward();
        inputValue = inputValue.normalized;

        //Checks to see if there is substantial values/movement from the input vector
        if (inputValue.sqrMagnitude > 0.1f)
        {
            TargetPosition += inputValue;
        }
    }

    private Vector3 GetCameraRight()
    {
        Vector3 right = CameraTransform.right;
        right.y = 0;
        return right;
    }

    private Vector3 GetCameraForward()
    {
        Vector3 forward = CameraTransform.forward;
        forward.y = 0;
        return forward;
    }

    private void UpdateBasePosition()
    {
        //Checks to see if there is substantial values/movement from the target position vector
        if (TargetPosition.sqrMagnitude > 0.1f)
        {
            //Lerping the speed to ramp speed up and down for smooth motion
            speed = Mathf.Lerp(speed, MaxSpeed, Time.deltaTime * Accleration);
            transform.position += TargetPosition * speed * Time.deltaTime;
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, -60, 60), transform.position.y, Mathf.Clamp(transform.position.z, -60, 60));
        }
        else
        {
            //Ramping velocity down
            HorizontalVelocity = Vector3.Lerp(HorizontalVelocity, Vector3.zero, Time.deltaTime * Damping);
            transform.position += HorizontalVelocity * Time.deltaTime;
        }

        //Reset target position
        TargetPosition = Vector3.zero;
    }

    private void RotateCamera(InputAction.CallbackContext inputValue)
    {
        //Only rotate if right Mouse button is pressed. This check doesn't care which frame the button was pressed
        if (!Mouse.current.rightButton.isPressed)
        {
            return;
        }

        float value = inputValue.ReadValue<Vector2>().x;
        transform.rotation = Quaternion.Euler(0f, value * MaxRotationSpeed + transform.rotation.eulerAngles.y, 0f);

    }

    private void ZoomCamera(InputAction.CallbackContext inputValue)
    {
        //Get the negative of the input
        float value = -inputValue.ReadValue<Vector2>().y * ZoomStepSize;

        //basically an if not zero check
        if(Mathf.Abs(value) > 0.1f)
        {
            ZoomHeight = CameraTransform.localPosition.y + value * StepSize;
            //Clamping zoom height
            ZoomHeight = Mathf.Clamp(ZoomHeight, MinHeight, MaxHeight);         
        }
    }

    private void UpdateCameraPosition()
    {
        Vector3 zoomTarget = new Vector3(CameraTransform.localPosition.x, ZoomHeight, CameraTransform.localPosition.z);
        //Where is the camera's current y value to the target y, tiems the zoom speed, along the axis of the forward vector. This zoom is not only height, but also a zooming in/out motion
        zoomTarget -= ZoomSpeed * (ZoomHeight - CameraTransform.localPosition.y) * Vector3.forward;

        CameraTransform.localPosition = Vector3.Lerp(CameraTransform.localPosition, zoomTarget, Time.deltaTime * ZoomDamping);
        CameraTransform.LookAt(transform);
    }
}
