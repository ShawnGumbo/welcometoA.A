using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class goal2 : MonoBehaviour
{
    public AudioSource goalSound;
    public float resetDelay = 5f;

    private bool hasWon = false;
    private PlayerMovement3D playerMovement;
    private Rigidbody playerRb;
    private MinimapRenderer minimap;
    private MazeGenerator3D mazeGenerator;

    private Vector3 resetPosition = new Vector3(2f, 4.5f, 2f);
    private Vector2Int goalCell;

    void Start()
    {
        // Find references
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            playerMovement = player.GetComponent<PlayerMovement3D>();
            playerRb = player.GetComponent<Rigidbody>();
        }

        mazeGenerator = FindObjectOfType<MazeGenerator3D>();
        minimap = FindObjectOfType<MinimapRenderer>();

        if (goalSound == null)
            Debug.LogWarning("Goal sound is not assigned!");

        if (mazeGenerator != null)
        {
            // You can hardcode or use width - 2/depth - 2 if needed
            int exitX = mazeGenerator.width - 2;
            int exitZ = mazeGenerator.depth - 2;
            goalCell = new Vector2Int(exitX, exitZ);

            Vector3 goalWorldPos = new Vector3(exitX * mazeGenerator.cellSize, transform.position.y, exitZ * mazeGenerator.cellSize);
            transform.position = goalWorldPos;

            if (minimap != null)
                minimap.SetGoalPosition(exitX, exitZ);
        }
        else
        {
            Debug.LogWarning("MazeGenerator3D not found!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;

            if (playerMovement != null)
                playerMovement.SetMovementEnabled(false);

            if (goalSound != null)
                goalSound.Play();

            StartCoroutine(LoadNextSceneAfterDelay());
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        yield return new WaitForSeconds(resetDelay);

        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextSceneIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene("Main Menu");
        }
        else
        {
            Debug.LogWarning("No next scene found in Build Settings.");
        }
    }
}
