using System.Collections.Generic;
using UnityEngine;

public class TwoWayPipe : MonoBehaviour
{
    public Transform targetPoint;
    public float cooldownTime = 0.5f;
    private bool canTeleport = true;

    // Static list of all pipe scripts
    private static List<TwoWayPipe> allPipes = new List<TwoWayPipe>();

    void Awake()
    {
        allPipes.Add(this);
    }

    void OnDestroy()
    {
        allPipes.Remove(this);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag("Cat"))
        {
            other.transform.position = targetPoint.position;
            StartCoroutine(DisableTeleportOnAllPipes());
        }
    }

    private System.Collections.IEnumerator DisableTeleportOnAllPipes()
    {
        foreach (var pipe in allPipes)
        {
            pipe.canTeleport = false;
        }

        yield return new WaitForSeconds(cooldownTime);

        foreach (var pipe in allPipes)
        {
            pipe.canTeleport = true;
        }
    }
}

