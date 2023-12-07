using System.Collections;
using UnityEngine;
using UnityEngine.ParticleSystemJobs;
using Cinemachine;
using UnityEngine.UI;

public class ThirdPersonMovement : MonoBehaviour
{

    //Components
    [SerializeField] private CinemachineVirtualCamera _camera;
    CharacterController controller;
    Animator anim;
    [SerializeField]private Slider sliderSens;

    [Header("Player Settings")]
    public float speed = 5;
    public float sprint = 8;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    public float sensitivity;
    public bool isGrounded;
    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    [Header("World Settings")]
    public Transform cam;
    public Transform groundCheck;
    public float groundDistance = 0.2f;
    public LayerMask groundMask;

    [Header("Particles")]
    [SerializeField] private ParticleSystem Steps;
    
    public bool isMoving;
    [SerializeField] private Glasses glasses;


   

    private void Start()
    {
        controller = GetComponent<CharacterController>();
        anim= GetComponent<Animator>();
        glasses.LentesOn = false;
        sliderSens.value = 150;
        Steps.Stop();
        

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
        if (glasses.LentesOn == false)
        {
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            //Gravity
            velocity.y += gravity * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);


            //walk 
            if (direction.magnitude >= 0.1f)
            {
                Steps.Play();
                isMoving = true;
                anim.SetBool("isWalking", true);
                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * speed * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isWalking", false);
                isMoving = false;
            }

            //Run
            if (Input.GetKey(KeyCode.LeftShift) && isMoving == true && isGrounded)
            {
                Steps.Play();
                anim.SetBool("isRunning", true);

                float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, angle, 0f);

                Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                controller.Move(moveDir.normalized * sprint * Time.deltaTime);
            }
            else
            {
                anim.SetBool("isRunning", false);

            }

            //Jump
            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }

            if (Input.GetKey(KeyCode.Space) && isGrounded)
            {
                Debug.Log("Salta");
                FindObjectOfType<AudioManager>().Play("Jump");
                anim.SetBool("isJumping", true);



                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);

            }
            else
            {

                anim.SetBool("isJumping", false);

            }

            

            if (isGrounded)
            {
                anim.SetBool("isFalling", false);
            }
            else
            {
                anim.SetBool("isFalling", true);
            }
        }

        Sensitivity();

    }

   
    void Sensitivity()
    {
        sensitivity = sliderSens.value;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_VerticalAxis.m_MaxSpeed = sensitivity * 2;
        _camera.GetCinemachineComponent<CinemachinePOV>().m_HorizontalAxis.m_MaxSpeed = sensitivity * 2;
    }


   
}


