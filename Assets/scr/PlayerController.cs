using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 15f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    private Animator animator;
    private bool isGrounded;
    private int jumpCount; // số lần nhảy hiện tại
    private Rigidbody2D rb;
    private GameManager gameManager;
    private InputAction moveAction;
    private InputAction jumpAction;
    private AudioManager audioManager;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindAnyObjectByType<GameManager>();
        audioManager = FindAnyObjectByType<AudioManager>();
        // Di chuyển ngang
        moveAction = new InputAction(type: InputActionType.Value, binding: "<Gamepad>/leftStick/x");
        moveAction.AddBinding("<Keyboard>/a").WithProcessor("scale(factor=-1)");
        moveAction.AddBinding("<Keyboard>/d");
        moveAction.AddBinding("<Keyboard>/leftArrow").WithProcessor("scale(factor=-1)");
        moveAction.AddBinding("<Keyboard>/rightArrow");

        // Nhảy: Space hoặc mũi tên lên
        jumpAction = new InputAction(type: InputActionType.Button, binding: "<Keyboard>/space");
        jumpAction.AddBinding("<Keyboard>/upArrow");
    }

    private void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
    }

    private void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
    }

    private void Update()
    {
        if (gameManager.IsGameOver()|| gameManager.IsGameWin()) return;
        HandMovement();
        UpdateAnimation();

        // kiểm tra mặt đất
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        // reset số lần nhảy khi chạm đất
        if (isGrounded && rb.linearVelocity.y <= 0)
        {
            jumpCount = 0;
        }

        // nhảy tối đa 2 lần
        if (jumpAction.WasPressedThisFrame() && jumpCount < 2)
        {
            HandleJump();
            jumpCount++;
        }
    }

    private void HandMovement()
    {
        float moveInput = moveAction.ReadValue<float>();
        rb.linearVelocity = new Vector2(moveInput * moveSpeed, rb.linearVelocity.y);

        // lật nhân vật theo hướng di chuyển
        if (moveInput > 0) transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        else if (moveInput < 0) transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
    }

    private void HandleJump()
    {
        audioManager.PlayJumpSound();
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
    }
    private void UpdateAnimation()
    {
        bool isRunning = Mathf.Abs(rb.linearVelocity.x) > 0.1f;
        bool isJumping = !isGrounded;
        animator.SetBool("isRunning", isRunning);
        animator.SetBool("isJumping", isJumping);
    }
}
