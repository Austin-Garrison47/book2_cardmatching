using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ManageButtons : MonoBehaviour
{
    public TMP_InputField playerName;
    public TMP_Dropdown cardDropdown;
    public TMP_Dropdown timeDropdown;

    public void GoToPreferences()
    {
        SceneManager.LoadScene("preferences");
    }

    public void StartGame()
    {
        PlayerPrefs.SetString("playerName", playerName.text);

        int cardCount = int.Parse(cardDropdown.options[cardDropdown.value].text.Split(' ')[0]);
        PlayerPrefs.SetInt("cardCount", cardCount);

        int timeLimit = int.Parse(timeDropdown.options[timeDropdown.value].text.Split(' ')[0]);
        PlayerPrefs.SetInt("timeLimit", timeLimit);

        PlayerPrefs.Save();

        SceneManager.LoadScene("chapterCards");
    }

    public void StopGame()
    {
        SceneManager.LoadScene("exit");
    }

}