using UnityEngine;
using UnityEngine.SceneManagement;

public class TimedGasTrap : MonoBehaviour
{
    public float activeDuration = 2f;   // Time gas is visible/dangerous
    public float inactiveDuration = 2f; // Time gas is gone
    private bool isActive = true;

    private SpriteRenderer sr;
    private Collider2D gasCollider;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        gasCollider = GetComponent<Collider2D>();
        StartCoroutine(GasCycle());
    }

    private System.Collections.IEnumerator GasCycle()
    {
        while (true)
        {
            // ACTIVE: Show gas and enable trigger
            isActive = true;
            sr.enabled = true;
            gasCollider.enabled = true;
            yield return new WaitForSeconds(activeDuration);

            // INACTIVE: Hide gas and disable trigger
            isActive = false;
            sr.enabled = false;
            gasCollider.enabled = false;
            yield return new WaitForSeconds(inactiveDuration);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isActive) return;

        if (other.CompareTag("Sam") || other.CompareTag("Cat"))
        {
            Debug.Log("Gas touched by: " + other.name);
            ReloadLevel();
        }
    }

    void ReloadLevel()
    {
        // Reload current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}

