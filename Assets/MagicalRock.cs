using UnityEngine;

public class MagicalRock : MonoBehaviour
{
    public GameObject teleportPrompt;
    public Transform teleportDestination;


    private bool playerInRange = false;

    private void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.X))
        {
            TeleportPlayer();
        }
    }

    private void TeleportPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        player.transform.position = teleportDestination.position;
        // Optionally, you can also adjust the player's rotation or reset other states as needed
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            teleportPrompt.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            teleportPrompt.SetActive(false);
        }
    }


}
