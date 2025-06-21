using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelFinishManager : MonoBehaviour
{
    public GameObject winTextObject;
    private bool samAtDoor = false;
    private bool catAtDoor = false;

    public void SamReachedDoor()
    {
        samAtDoor = true;
        CheckWinCondition();
    }

    public void CatReachedDoor()
    {
        catAtDoor = true;
        CheckWinCondition();
    }

    private void CheckWinCondition()
    {
        if (samAtDoor && catAtDoor)
        {
            winTextObject.SetActive(true);
            Time.timeScale = 0f; // Pause the game
        }
    }
}

