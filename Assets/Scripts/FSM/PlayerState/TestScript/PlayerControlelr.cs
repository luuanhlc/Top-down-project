/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlelr : MonoBehaviour
{
    public Rigidbody _rb;
    [Header("LayerMask")]
    public LayerMask GroundLayer;
    public LayerMask WallCanRun;

    [Header("AnimationCurve")]
    public AnimationCurve speedCurve;
    public AnimationCurve jumpCurve;

    [Header("Run")]
    private float timeRun;
    private float speed;
    public float baseSpeed;

    //Run Getter and Setter
    public float TimeRun { get { return timeRun; } set { timeRun = value; } }
    public float Speed { get { return speed; } set { speed = value; } }

    [Header("Jump")]
    private float timeJump;
    private float jumpPower;
    public int jumpLeft = 0;

    public bool _isGrounded;
    private bool _isJumpPress;
    private bool _isMovementPress;
    public bool _isNeedToResetJump = false;
    public float baseJumpPower;
    [HideInInspector]
    public Vector3 moveVectorDir;

    [Header("Cam")]
    public Camera _cam;

    public float mouseSensitivity = 1f;
    public float maxVertical;
    float verticalHorizontal = 0;
    private void Update()
    {
        CamController();

        GroundCheck();
        GetInput();
    }
    void GroundCheck()
    {
        Debug.DrawLine(transform.position, transform.position - new Vector3(0, 1.06f, 0), Color.blue);

        if (Physics.Raycast(transform.position, -transform.up, 1.01f, GroundLayer))
        {
            _isGrounded = true;
            _isNeedToResetJump = false;
        }
        else
            _isGrounded = false;
    }
    void GetInput()
    {
        if (Input.GetKey(KeyCode.Space) && _isGrounded && !_isNeedToResetJump)
        {
            _isJumpPress = true;
            HandleJump();
        }
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        moveVectorDir = this.gameObject.transform.forward * y + this.gameObject.transform.right * x;

        if (_isGrounded)
            if (x != 0 || y != 0)
            {
                _isMovementPress = true;

                move();
            }
            else
                _isMovementPress = false;
        else
            _isMovementPress = false;
    }
    public void move()
    {
        timeRun += Time.deltaTime;
        speed = speedCurve.Evaluate(timeRun);
        _rb.velocity = moveVectorDir * speed * baseSpeed;
    }
    private void HandleJump()
    {
        _isJumpPress = false;
        _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
         _rb.AddForce(Vector3.up * baseJumpPower);
        _isNeedToResetJump = true;
    }
    private void CamController()
    {
        float rotationHorizontal = Input.GetAxis("Mouse X");
        verticalHorizontal += -Input.GetAxis("Mouse Y");
        verticalHorizontal = Mathf.Clamp(verticalHorizontal, -maxVertical, maxVertical);

        transform.RotateAround(this.gameObject.transform.position, -Vector3.up, -rotationHorizontal * mouseSensitivity);
        _cam.transform.localRotation = Quaternion.Euler(verticalHorizontal, 0, 0);
    }
}
*/