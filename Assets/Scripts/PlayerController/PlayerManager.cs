using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    #region singleton
    public static PlayerManager Ins;
    private void Awake()
    {
        Ins = this;
    }
    #endregion

    public Rigidbody _rb;
    [Header("LayerMask")]
    public LayerMask GroundLayer;
    public LayerMask WallCanRun;

    [Header("Move ")]
    public AnimationCurve speedCurve;
    public AnimationCurve jumpCurve;

    [HideInInspector]
    public float timeRun;
    [HideInInspector]
    public float timeJump;
    [HideInInspector]
    public float jumpPower;

    public float baseJumpPower;
    [HideInInspector]
    public float speed;

    public float baseSpeed;
    [HideInInspector]
    public bool _isPlaned;
    [HideInInspector]
    public bool _isJumping;

    public int jumpLeft = 0;

    [HideInInspector]
    public Vector3 moveVectorDir;

   /* private void Update()
    {
        //Move script
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");
        Vector3 moveVector = _rb.transform.forward * y + _rb.transform.right * x;
        if (x != 0 || y != 0)
            if (!_isPlaned)
                return;
            else
                move(moveVector);
        else
            timeRun = 0;

        if (Input.GetKey(KeyCode.Space) && jumpLeft > 0 && _isPlaned)
        {
            Jump();
            return;
        }

        //Planed check
        if (_isPlaned)
            return; 
        if (Physics.Raycast(transform.position, -transform.up, 1.05f, GroundLayer))
        {
            _isPlaned = true;
            timeJump = 0;
            jumpLeft += 1;
        }
    }

    private void Jump()
    {
        _isJumping = true;

        timeJump += Time.deltaTime;
        jumpPower = jumpCurve.Evaluate(timeJump);
        
        if (timeJump >= 1)
        {
            jumpLeft -= 1;
            _isJumping = false;
            _isPlaned = false;
        }

        _rb.AddForce(Vector3.up * jumpPower * baseJumpPower );
    }*/

    public void CheckCrrState()
    {
        if (Physics.Raycast(transform.position, -transform.up, 1.05f, GroundLayer))
        {
            
        }
    }

    //Move class use velocity
    public void move()
    {
        timeRun += Time.deltaTime;
        speed = speedCurve.Evaluate(timeRun);
        _rb.velocity = moveVectorDir * speed * baseSpeed;
    }


}
