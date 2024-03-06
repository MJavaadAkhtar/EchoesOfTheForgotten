using UnityEngine;

public class NPCBehavior : MonoBehaviour
{
    public float speed = 2.0f;
    public Transform player;
    private bool isFollowing = false;
    private Vector3 targetPosition;
    public GameObject choicePanel;
    private bool playerInRange = false; // Flag to check if player is in range for interaction
    private bool allowRandomMovement = true;
    private bool aLreadySaidYes = false;
    public LayerMask terrainLayerMask;


    void Start()
    {
        terrainLayerMask = LayerMask.GetMask("Terrain");
        ChooseNewTargetPosition();
    }

    void Update()
    {
        if (playerInRange && choicePanel.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Y))
            {
                AcceptFollow();
            }
            else if (Input.GetKeyDown(KeyCode.N))
            {
                DeclineFollow();
            }
        }
        else if (!isFollowing)
        {
            if (allowRandomMovement)
            {
                AdjustHeight();
                MoveRandomly();
            }
            
        }
        else
        {
            AdjustHeight();
            FollowPlayer();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !aLreadySaidYes)
        {
            // Stop moving randomly
            //isFollowing = true; // Assume yes for simplicity
            // Here you would typically display a UI prompt for the player to choose
            allowRandomMovement = false; // Stop the NPC from moving
            playerInRange = true;
            choicePanel.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && !aLreadySaidYes)
        {
            // Stop moving randomly
            //isFollowing = true; // Assume yes for simplicity
            // Here you would typically display a UI prompt for the player to choose
            playerInRange = false;
            choicePanel.SetActive(false);
            if (!isFollowing) // Only resume random movement if not following the player
        {
            allowRandomMovement = true;
            ChooseNewTargetPosition(); // Optionally reset the target position to ensure randomness
        }
        }
    }

    void MoveRandomly()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 1f)
        {
            ChooseNewTargetPosition();
        }
    }

    void FollowPlayer()
    {
        targetPosition = player.position;
        float distanceToPlayer = Vector3.Distance(transform.position, targetPosition);

        Debug.Log(distanceToPlayer);
        Debug.Log(distanceToPlayer > 4f);

        if (distanceToPlayer > 4f)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        }
    }

    void ChooseNewTargetPosition()
    {
        // Randomly choose a new target position within bounds of your scene
        targetPosition = new Vector3(Random.Range(-5, 5), transform.position.y, Random.Range(-5, 5));
    }

    public void AcceptFollow()
    {
        isFollowing = true;
        choicePanel.SetActive(false); // Hide the choice panel
        aLreadySaidYes = true;
    }

    public void DeclineFollow()
    {
        isFollowing = false;
        ChooseNewTargetPosition(); // Possibly choose a new random position to move to
        choicePanel.SetActive(false); // Hide the choice panel
    }

    void AdjustHeight()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position + Vector3.up * 10, Vector3.down, out hit, Mathf.Infinity, terrainLayerMask))
        {
            // Adjust the y position to match the terrain height, adding a slight offset to ensure it's "touching" the ground
            float desiredHeight = hit.point.y + 0.5f; // Adjust this offset based on your NPC's height and the desired effect
            Vector3 currentPosition = transform.position;
            currentPosition.y = desiredHeight;
            transform.position = currentPosition;
        }
    }
}
