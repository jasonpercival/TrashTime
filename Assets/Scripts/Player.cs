using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{
    CharacterController characterController;

    public float speed = 6.0f;
    private float speedBoost = 0.0f;

    public float gravity = 20.0f;
    public float rotateSpeed = 3.0f;    

    private Vector3 moveDirection = Vector3.zero;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
    }

    void Update()
    {
        // prevent updates if the game is not active
        if (!GameManager.Instance.isGameActive) return;
               
        float vertAxis = Input.GetAxis("Vertical");
        float hortAxis = Input.GetAxis("Horizontal");   
        
        transform.Rotate(0.0f, hortAxis * rotateSpeed, 0.0f);

        float deltaTime = Time.deltaTime;        
        
        if (characterController.isGrounded)
        {
            // We are grounded, so recalculate move direction directly from axes
            moveDirection = transform.forward * vertAxis;
            moveDirection *= (speed + speedBoost);

            // Booster activation
            if ((Input.GetButton("Jump") || Input.GetButton("Fire2")) && GameManager.Instance.speedBoostFuel > 0.0f)
            {
                speedBoost = 5.0f;
                GameManager.Instance.speedBoostFuel -= deltaTime;
            }
            else
            {
                speedBoost = 0.0f;
                GameManager.Instance.speedBoostFuel += deltaTime * 0.5f;
                if (GameManager.Instance.speedBoostFuel > GameManager.Instance.speedBoostFuelMax)
                {
                    GameManager.Instance.speedBoostFuel = GameManager.Instance.speedBoostFuelMax;
                }
            }
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        moveDirection.y -= gravity * Time.deltaTime;

        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);
    }
}