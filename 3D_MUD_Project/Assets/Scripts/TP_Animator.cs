using UnityEngine;
using System.Collections;

public class TP_Animator : MonoBehaviour
{
    public enum Direction
    {
        Stationary, Forward, Backward, Left, Right,
        LeftForward, RightForward, LeftBackward, RightBackward
    }

    public enum CharacterState
    {
        Idle, Running, WalkBackwards, StrafingLeft, StrafingRight, Jumping,
        Falling, Landing, Climbing, Sliding, Using, Dead, ActionLocked, Attacking
    }

    public static TP_Animator Instance;

    private CharacterState lastState;

    public Direction MoveDirection { get; set; }
    public CharacterState State { get; set; }
    public bool IsDead { get; set; }

    void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        DetermineCurrentState();
        ProcessCurrentState();

        // Debug.Log("Current Character State: " + State.ToString());
    }

    public void DetermineCurrentMoveDIrection()
    {
        var forward = false;
        var backward = false;
        var left = false;
        var right = false;

        if (TP_Motor.Instance.MoveVector.z > 0)
        {
            forward = true;
        }
        if (TP_Motor.Instance.MoveVector.z < 0)
        {
            backward = true;
        }
        if (TP_Motor.Instance.MoveVector.x > 0)
        {
            right = true;
        }
        if (TP_Motor.Instance.MoveVector.x < 0)
        {
            left = true;
        }

        if (forward)
        {
            if (left)
            {
                MoveDirection = Direction.LeftForward;
            }
            else if (right)
            {
                MoveDirection = Direction.RightForward;
            }
            else
            {
                MoveDirection = Direction.Forward;
            }
        }
        else if (backward)
        {
            if (left)
            {
                MoveDirection = Direction.LeftBackward;
            }
            else if (right)
            {
                MoveDirection = Direction.RightBackward;
            }
            else
            {
                MoveDirection = Direction.Backward;
            }
        }
        else if (left)
        {
            MoveDirection = Direction.Left;
        }
        else if (right)
        {
            MoveDirection = Direction.Right;
        }
        else
        {
            MoveDirection = Direction.Stationary;
        }

    }

    void DetermineCurrentState()
    {
        if (State == CharacterState.Dead)
        {
            return;
        }

        if (!TP_Controller.CharacterController.isGrounded)
        {
            if (State != CharacterState.Falling && 
                State != CharacterState.Jumping && 
                State != CharacterState.Landing)
            {
                // We should be falling
                Fall();
            }
        }

        if (State != CharacterState.Falling &&
            State != CharacterState.Jumping &&
            State != CharacterState.Landing &&
            State != CharacterState.Attacking &&
            State != CharacterState.Climbing &&
            State != CharacterState.Sliding)
        {
            switch (MoveDirection)
            {
                case Direction.Stationary:
                    State = CharacterState.Idle;
                    break;
                case Direction.Forward:
                    State = CharacterState.Running;
                    break;
                case Direction.Backward:
                    State = CharacterState.WalkBackwards;
                    break;
                case Direction.Left:
                    State = CharacterState.StrafingLeft;
                    break;
                case Direction.Right:
                    State = CharacterState.StrafingRight;
                    break;
                case Direction.LeftForward:
                    State = CharacterState.Running;
                    break;
                case Direction.RightForward:
                    State = CharacterState.Running;
                    break;
                case Direction.LeftBackward:
                    State = CharacterState.WalkBackwards;
                    break;
                case Direction.RightBackward:
                    State = CharacterState.WalkBackwards;
                    break; 
            }
        }
    }

    void ProcessCurrentState()
    {
        switch (State)
        {
            case CharacterState.Idle:
                Idle();
                break;
            case CharacterState.Running:
                Running();
                break;
            case CharacterState.WalkBackwards:
                WalkBackwards();
                break;
            case CharacterState.StrafingLeft:
                StrafingLeft();
                break;
            case CharacterState.StrafingRight:
                StrafingRight();
                break;
            case CharacterState.Jumping:
                Jumping();
                break;
            case CharacterState.Falling:
                Falling();
                break;
            case CharacterState.Landing:
                Landing();
                break;
            case CharacterState.Climbing:
                break;
            case CharacterState.Sliding:
                break;
            case CharacterState.Attacking:
                Attacking();
                break;
            case CharacterState.Dead:
                break;
            case CharacterState.ActionLocked:
                break;
        }
    }

    #region Character State Methods

    void Idle()
    {
        GetComponent<Animation>().CrossFade("Idle");
    }

    void Running()
    {
        GetComponent<Animation>().CrossFade("Run");
    }

    void WalkBackwards()
    {
        GetComponent<Animation>().CrossFade("Walk");
    }

    void StrafingLeft()
    {
        GetComponent<Animation>().CrossFade("Walk");
    }

    void StrafingRight()
    {
        GetComponent<Animation>().CrossFade("Walk");
    }

    void Attacking()
    {
        if (!GetComponent<Animation>().isPlaying)
        {
            Player.Instance.Attack();
            State = CharacterState.Idle;
            GetComponent<Animation>().CrossFade("Idle");
        }
    }

    void Jumping()
    {
        if ((!GetComponent<Animation>().isPlaying && TP_Controller.CharacterController.isGrounded) || TP_Controller.CharacterController.isGrounded)
        {
            if (lastState == CharacterState.Running)
            {
                GetComponent<Animation>().CrossFade("RunLand");
            }
            else
            {
                GetComponent<Animation>().CrossFade("JumpLand");
            }
            State = CharacterState.Landing;
        }
        else if (!GetComponent<Animation>().IsPlaying("Jump"))
        {
            State = CharacterState.Falling;
            GetComponent<Animation>().CrossFade("Falling");
        }
        else
        {
            State = CharacterState.Jumping;
            // Help determine if we fell too far
        }
    }

    void Falling()
    {
        if (TP_Controller.CharacterController.isGrounded)
        {
            if (lastState == CharacterState.Running)
            {
                GetComponent<Animation>().CrossFade("RunLand");
            }
            else
            {
                GetComponent<Animation>().CrossFade("JumpLand");
            }
            State = CharacterState.Landing;
        }
    }

    void Landing()
    {
        if (lastState == CharacterState.Running)
        {
            if (!GetComponent<Animation>().IsPlaying("RunLand"))
            {
                State = CharacterState.Running;
                GetComponent<Animation>().Play("Run");
            }
        }
        else
        {
            if (!GetComponent<Animation>().IsPlaying("JumpLand"))
            {
                State = CharacterState.Idle;
                GetComponent<Animation>().Play("Idle");
            }
        }
    }

    #endregion

    #region Start Action Method

    IEnumerator Wait(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
    }

    public void Attack()
    {
        State = CharacterState.Attacking;
        GetComponent<Animation>().Play("Attack");
    }

    public void Jump()
    {
        if (!TP_Controller.CharacterController.isGrounded || IsDead || State == CharacterState.Jumping)
        {
            return;
        }

        lastState = State;
        State = CharacterState.Jumping;
        GetComponent<Animation>().CrossFade("Jump");
    }

    public void Fall()
    {
        if (IsDead)
        {
            return;
        }

        lastState = State;
        State = CharacterState.Falling;
        // If we are to high do soemthing
        GetComponent<Animation>().CrossFade("Falling");
    }

    #endregion
}