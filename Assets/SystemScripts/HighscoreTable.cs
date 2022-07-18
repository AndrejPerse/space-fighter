using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//gets player list from main manager and transforms data to high score entries

// Sets the script to be executed later than all default scripts
// This is helpful for UI, since other things may need to be initialized before setting the UI
[DefaultExecutionOrder(1000)]
public class HighscoreTable : MonoBehaviour
{
    private Transform entryContainer;
    private Transform entryTemplate;
    private List<Transform> highscoreEntryList;

    private void Awake()
    {
        MainManager.Instance.SortPlayerList();

        //find highscore entry template, save it to variable and then hide it
        entryContainer = transform.Find("Highscore Entry Container");
        entryTemplate = entryContainer.Find("Highscore Entry Template");
        entryTemplate.gameObject.SetActive(false);

        //make a list of top ten players
        highscoreEntryList = new List<Transform>();
        int entryNum = 0;
        int entryMax = 10;
        foreach (PlayerData player in MainManager.Instance.loadedPlayerList)
        {
            CreateHighscoreEntry(player, entryContainer, highscoreEntryList);
            entryNum++;
            if (entryNum == entryMax)
            {
                break;
            }
        }
    }

    private void CreateHighscoreEntry(PlayerData player, Transform container, List<Transform> playerList)
    {
        float templateHeight = 70.0f;
        //duplicate entry template, move it down for every new entry
        Transform entry = Instantiate(entryTemplate, container);
        RectTransform entryRecTransform = entry.GetComponent<RectTransform>();
        entryRecTransform.anchoredPosition = new Vector2(0, -templateHeight * playerList.Count);
        entry.gameObject.SetActive(true);

        //replace preset values with data
        int rank = playerList.Count + 1;
        string rankString;
        switch (rank)
        {
            case 1:
                rankString = "1ST";
                break;
            case 2:
                rankString = "2ND";
                break;
            case 3:
                rankString = "3RD";
                break;
            default:
                rankString = rank + "TH";
                break;
        }

        entry.Find("Position Text").GetComponent<Text>().text = rankString;

        int score = player.highscore;
        entry.Find("Score Text").GetComponent<Text>().text = score.ToString();

        string name = player.name;
        entry.Find("Player Text").GetComponent<Text>().text = name;

        playerList.Add(entry);
    }
}
