using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//listens for buttons and input field, transfers player name to main manager

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class MenuHandler : MonoBehaviour
{
    public InputField inputField;
    private Transform titleScreen;
    private Transform instructionsPage;

    private void Start()
    {
        EnterInput();
        titleScreen = transform.Find("Title Screen");
        instructionsPage = transform.Find("Instructions");
    }

    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    public void OpenInstructions()
    {
        instructionsPage.gameObject.SetActive(true);
        titleScreen.gameObject.SetActive(false);
    }

    public void CloseInstructions()
    {
        instructionsPage.gameObject.SetActive(false);
        titleScreen.gameObject.SetActive(true);
    }

    //function is called when we press "Start" button. gives main manager information about input field
    public void ReadPlayerName()
    {
        MainManager.Instance.playerName = inputField.text;

        bool playerExists = false;
        // Check if player already exists
        foreach (PlayerData player in MainManager.Instance.loadedPlayerList)
        {
            if (player.name == inputField.text)
            {
                MainManager.Instance.highscore = player.highscore;
                playerExists = true;
            }
        }
        if (!playerExists)
        {
            MainManager.Instance.highscore = 0;
        }
    }

    public void EnterInput()
    {
        if (MainManager.Instance.playerName != "Player") //for when game is run for the first time on a new device
        {
            inputField.text = MainManager.Instance.playerName;
        }
    }
}
