using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento")]
    public Rigidbody theRB;
    public float moveSpeed = 5f;
    public float jumpForce = 7f; // 🔥 ligeramente más bajo

    private Vector3 moveDirection;

    [Header("Suelo")]
    public LayerMask whatIsGround;
    public Transform groundPoint;
    private bool isGrounded;

    [Header("Animaciones y Sprites")]
    public Animator anim;
    public SpriteRenderer theSR;
    public Animator flipAnim;

    private bool facingRight = true;
    private bool facingFront = true;

    [Header("Vida")]
    public int maxHealth = 100;
    public int currentHealth;

    void Awake()
    {
        if (theRB == null)
            theRB = GetComponent<Rigidbody>();

        theRB.isKinematic = false;
        theRB.constraints = RigidbodyConstraints.FreezeRotation;
    }

    void Start()
    {
        currentHealth = maxHealth;
    }

    void Update()
    {
        float inputX = Input.GetAxisRaw("Horizontal");
        float inputZ = Input.GetAxisRaw("Vertical");

        Vector3 inputVector = new Vector3(inputX, 0f, inputZ);
        if (inputVector.magnitude > 1)
            inputVector.Normalize();

        Vector3 camForward = Camera.main.transform.forward;
        camForward.y = 0;
        camForward.Normalize();

        Vector3 camRight = Camera.main.transform.right;
        camRight.y = 0;
        camRight.Normalize();

        moveDirection = camRight * inputVector.x + camForward * inputVector.z;

        anim.SetFloat("moveSpeed", new Vector3(moveDirection.x, 0f, moveDirection.z).magnitude);

        isGrounded = Physics.Raycast(groundPoint.position, Vector3.down, 0.3f, whatIsGround);
        anim.SetBool("onGround", isGrounded);

        // --- SALTO ---
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Vector3 vel = theRB.linearVelocity;
            vel.y = jumpForce;
            theRB.linearVelocity = vel;
        }

        if (theRB.linearVelocity.y < 0)
        {
            theRB.linearVelocity += Vector3.up * Physics.gravity.y * (6f - 1) * Time.deltaTime;
        }
        else if (theRB.linearVelocity.y > 0 && !Input.GetButton("Jump"))
        {
            theRB.linearVelocity += Vector3.up * Physics.gravity.y * (5f - 1) * Time.deltaTime;
        }

        // --- Flip izquierda/derecha ---
        if (inputX < 0 && facingRight)
        {
            facingRight = false;
            theSR.flipX = true;
            flipAnim.SetTrigger("Flip");
        }
        else if (inputX > 0 && !facingRight)
        {
            facingRight = true;
            theSR.flipX = false;
            flipAnim.SetTrigger("Flip");
        }

        // --- Flip frente/espalda ---
        bool haciaFondo = inputZ > 0;
        if (haciaFondo && facingFront)
        {
            facingFront = false;
            anim.SetBool("movingBackwards", true);
            flipAnim.SetTrigger("Flip");
        }
        else if (!haciaFondo && !facingFront)
        {
            facingFront = true;
            anim.SetBool("movingBackwards", false);
            flipAnim.SetTrigger("Flip");
        }
    }

    void FixedUpdate()
    {
        Vector3 horizontalVelocity = new Vector3(moveDirection.x * moveSpeed, 0f, moveDirection.z * moveSpeed);
        theRB.linearVelocity = new Vector3(horizontalVelocity.x, theRB.linearVelocity.y, horizontalVelocity.z);
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount;
        Debug.Log("Jugador recibe daño: " + damageAmount + ", vida restante: " + currentHealth);

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            Debug.Log("Jugador ha muerto");
        }
    }
}