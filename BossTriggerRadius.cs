using UnityEngine;

//each boss is going to need a collider of some sort to interact with the player box
// each animator controller will need a trigger variable call StartBossAnimation
// each collider will need the istrigger checked as well


public class BossTrigger : MonoBehaviour
{
    public GameObject bossObject; //boss game object
    public Animator bossAnimator; //animator compnoent

    private bool bossTriggered = false; 3

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player and the boss hasn't been triggered yet
        if (other.CompareTag("Player") && !bossTriggered)
        {
            // Trigger boss animation
            if (bossObject != null && bossAnimator != null)
            {
                bossAnimator.SetTrigger("StartBossAnimation"); //each animator needs one of these triggers
                bossTriggered = true; //triggers only once per cycle
            }
        }
    }
}
