using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMotor : MonoBehaviour {
    #region Variables
    private CharacterController m_controller;

    private float _jumpForce = 5.0f;

    private int desiredPostion = 1;

    //difficulty varaibles
    private float m_Speed = 10.0f;
    private float m_speedLast;
    private float timeElapsed = 2.5f;
    private float difficulty_speed = 0.1f;

    private const float LANEDISTANCE = 3.0f;
    private const float PLAYERTURN = 0.05f;

    //private bool GROUNDED = true;
    private float _mGravity;
    public float gravity = 12.0f;

    //AnimationController
    private Animator _mAnim;

    private bool isLoaded = false;
    private Scene scene;
    #endregion



    private void Start()
    {
        m_controller = GetComponent<CharacterController>();
        _mAnim = GetComponent<Animator>();
        scene = SceneManager.GetActiveScene();

    }
    private void Update()
    {
        if(!isLoaded)
            return;
        
        //difficulty handling
        if(Time.time - m_speedLast > timeElapsed)
        {
            m_speedLast = Time.time;
            m_Speed += difficulty_speed;


        }
        //death handling
        if (GameManager.Instance.HP < 1)
            Death();

        //input handling
        if (MobileInput.Instance.SwipeLeft)
            MoveLane(false);
        if (MobileInput.Instance.SwipeRight)
            MoveLane(true);
        //movement logic
        Vector3 targetPosition = transform.position.z * Vector3.forward;
        if (desiredPostion == 0)
        {
            targetPosition += Vector3.left * LANEDISTANCE;

        }
        else if (desiredPostion == 2)
            targetPosition += Vector3.right * LANEDISTANCE;

        //Actual movement of player
        Vector3 moveVector = Vector3.zero;
        moveVector.x = (targetPosition - transform.position).normalized.x * m_Speed;

        //if there is jump to be implemented later if I have time
        bool isGrounded = _mIsGrouded();
        _mAnim.SetBool("Grounded", isGrounded);
        if(_mIsGrouded())
        {
            _mGravity = -0.1f;
            if(MobileInput.Instance.SwipeUp)
            {
                _mAnim.SetTrigger("Jump");
                _mGravity = _jumpForce;
            }
            else if (MobileInput.Instance.SwipeDown)
            {
                //dive underwater
                DiveUnderWater();
                Invoke("StopDiving",1.0f);
            }
        }
        else{
            _mGravity -= (gravity * Time.deltaTime);
            //fast fall? for swipe down-> implement if have time
            if(MobileInput.Instance.SwipeDown)
            {
                _mGravity = -_jumpForce;
            }

        }
        moveVector.y = _mGravity;
        moveVector.z = m_Speed;

        //move the player;
        m_controller.Move(moveVector * Time.deltaTime);
        //aesthetic rotation
        Vector3 _mRotation = m_controller.velocity;
        _mRotation.y = 0;
        transform.forward = Vector3.Lerp(transform.forward, _mRotation, PLAYERTURN);


    }
    //#region MyRegion
    private void MoveLane(bool goingRight)
    {
        desiredPostion += (goingRight) ? 1 : -1;
        desiredPostion = Mathf.Clamp(desiredPostion, 0, 2);
    }
    //#endregion
    private bool _mIsGrouded()//purely because I like using late update the the inbuilt isGrounded from the character controller can return false values sometimes
    {
        Ray _mRay = new Ray(new Vector3(m_controller.bounds.center.x, (m_controller.bounds.center.y-m_controller.bounds.extents.y) + 0.2f, m_controller.bounds.center.z),Vector3.down);
        return Physics.Raycast(_mRay, 0.2f + 0.1f);
    }

    public void StartRunningGame()
    {
        isLoaded = true;
        _mAnim.SetTrigger("BeginRun");
    }

    private void DiveUnderWater()
    {
        _mAnim.SetBool("Dive", true);
        m_controller.height /= 2;
        m_controller.center = new Vector3(m_controller.center.x, m_controller.center.y / 2, m_controller.center.z);
    }
    private void StopDiving()
    {
        _mAnim.SetBool("Dive", false);
        m_controller.height *= 2;
        m_controller.center = new Vector3(m_controller.center.x, m_controller.center.y * 2, m_controller.center.z);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        switch(hit.gameObject.tag)
        {
            case "Debris":
                GameManager.Instance.HP -=1;
                hit.gameObject.SetActive(false);
                break;
        }
    }
    private void Death()
    {
        _mAnim.SetTrigger("Death");
        isLoaded = false;
        GameManager.Instance.PostScore();
        GameManager.Instance.isGameStarted = false;
        SceneManager.LoadScene(scene.name);
        PlayerPrefs.SetFloat("highscore", GameManager.Instance.HighScore);

    }
}
