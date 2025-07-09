using UnityEngine;

public class PlayerMovement3D : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 7f;
    public float rotateSpeed = 90f; // Degrees per second
    public LayerMask groundLayer;

    private Rigidbody rb;
    private bool isGrounded;
    private bool movementEnabled = true;

    private MinimapRenderer minimap;
    private Vector2Int lastCell = new Vector2Int(-1, -1);

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        minimap = FindObjectOfType<MinimapRenderer>();
    }

    void Update()
    {
        if (!movementEnabled)
        {
            rb.linearVelocity = new Vector3(0, rb.linearVelocity.y, 0);
            return;
        }

        RotatePlayerWithArrowKeys();
        MovePlayer();
        CheckJump();
        UpdateMinimapPosition();
    }

    private void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 moveDir = (transform.right * moveX + transform.forward * moveZ).normalized * moveSpeed;
        rb.linearVelocity = new Vector3(moveDir.x, rb.linearVelocity.y, moveDir.z);
    }

    private void RotatePlayerWithArrowKeys()
    {
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        transform.Rotate(Vector3.up * rotateSpeed * Time.deltaTime);
    else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        transform.Rotate(Vector3.up * -rotateSpeed * Time.deltaTime);
    }

    private void CheckJump()
    {
        isGrounded = Physics.Raycast(transform.position, Vector3.down, 1.1f, groundLayer);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector3(rb.linearVelocity.x, jumpForce, rb.linearVelocity.z);
        }
    }

    private void UpdateMinimapPosition()
    {
        if (minimap == null) return;

        int x = Mathf.RoundToInt(transform.position.x / 2f);
        int z = Mathf.RoundToInt(transform.position.z / 2f);
        Vector2Int currentCell = new Vector2Int(x, z);

        if (currentCell != lastCell)
        {
            minimap.MarkVisited(x, z);
            minimap.SetPlayerPosition(x, z);
            lastCell = currentCell;
        }
    }

    public void SetMovementEnabled(bool enabled)
    {
        movementEnabled = enabled;
    }
}
