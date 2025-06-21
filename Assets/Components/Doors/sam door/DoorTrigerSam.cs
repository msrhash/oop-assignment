using UnityEngine;

public class DoorTriger : MonoBehaviour
{
    public Animator doorAnimator;
    private Collider2D doorZone;
    private Collider2D samCollider;

    private float checkDelay = 0.2f; 
    private bool delayDone = false;
    private bool samHasReached = false;

    void Start()
    {
        doorZone = GetComponent<Collider2D>();
        samCollider = GameObject.FindGameObjectWithTag("Sam").GetComponent<Collider2D>();
        Invoke(nameof(EnableCheck), checkDelay); 
    }

    void EnableCheck()
    {
        delayDone = true;
    }

    void Update()
    {
        if (!delayDone) return;

        bool isFullyInside = doorZone.bounds.Contains(samCollider.bounds.min) && doorZone.bounds.Contains(samCollider.bounds.max);
        doorAnimator.SetBool("IsOpen", isFullyInside);

        if (isFullyInside && !samHasReached)
        {
            samHasReached = true;
            UnityEngine.Object.FindFirstObjectByType<LevelFinishManager>().SamReachedDoor();
        }

        Debug.Log("Is Sam fully inside: " + isFullyInside);
    }
}
