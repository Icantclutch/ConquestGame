using UnityEngine;
using UnityEngine.InputSystem;

public class CameraControls : MonoBehaviour
{
    [SerializeField]
    private InputActionReference Movement, Look, Interact, RightClick;

    [SerializeField]
    private int CameraMovementSpeed = 50;

    public Vector3 viewVector;

    private bool RClickHold;
    private Vector2 cameraRotation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cameraRotation = Vector2.zero;
        gameObject.transform.rotation = Quaternion.LookRotation(cameraRotation);
    }

    // Update is called once per frame
    void Update()
    {
        //Gather Raw Inputs
        Vector2 moveInput = Movement.action.ReadValue<Vector2>();
        RClickHold = RightClick.action.ReadValue<float>() == 1 ? true : false;

        //Camera Movement
        
        Vector3 movement = new Vector3(moveInput.x, 0, moveInput.y);
        gameObject.transform.position += movement * CameraMovementSpeed * Time.deltaTime;


        //Camera Rotation
        if (RClickHold)
        {
            //Gather Mouse raw Input
            Vector2 cameraLook = Look.action.ReadValue<Vector2>();           
            viewVector = cameraLook;

            Vector3 mousePosition = Input.mousePosition;


            cameraRotation += cameraLook / 10;

            gameObject.transform.rotation = Quaternion.LookRotation(cameraRotation);

        }    

    }

}
