using UnityEngine;

public class CatDoorTrigger : MonoBehaviour
{
    public Animator doorAnimator;
    private Collider2D doorZone;
    private Collider2D catCollider;

    private float checkDelay = 0.2f;
    private bool delayDone = false;
    private bool catHasReached = false;

    void Start()
    {
        doorZone = GetComponent<Collider2D>();
        catCollider = GameObject.FindGameObjectWithTag("Cat").GetComponent<Collider2D>();
        Invoke(nameof(EnableCheck), checkDelay);
    }

    void EnableCheck()
    {
        delayDone = true;
    }

    void Update()
    {
        if (!delayDone) return;

        bool isFullyInside = doorZone.bounds.Contains(catCollider.bounds.min) && doorZone.bounds.Contains(catCollider.bounds.max);
        doorAnimator.SetBool("IsOpen", isFullyInside);

        if (isFullyInside && !catHasReached)
        {
            catHasReached = true;
            UnityEngine.Object.FindFirstObjectByType<LevelFinishManager>().CatReachedDoor();

        }

        Debug.Log("Is Cat fully inside: " + isFullyInside);
    }
}
