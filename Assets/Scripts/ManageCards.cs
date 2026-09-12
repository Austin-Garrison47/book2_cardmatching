using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ManageCards : MonoBehaviour
{
    public GameObject card;

    public TMP_Text playerNameText;
    public TMP_Text timerText;

    bool firstCardSelected = false;
    bool secondCardSelected = false;

    GameObject card1;
    GameObject card2;

    string rowForCard1 = "";
    string rowForCard2 = "";

    bool timerHasElapsed = false;
    bool timerHasStarted = false;
    float timer = 0;

    AudioSource audioSource;

    int nbMatch = 0;
    int numberOfCards;
    int score = 0;

    float timeRemaining;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        numberOfCards = PlayerPrefs.GetInt("cardCount", 10);

        string playerName = PlayerPrefs.GetString("playerName", "Player");
        playerNameText.text = "Player: " + playerName;

        timeRemaining = PlayerPrefs.GetInt("timeLimit", 30);

        DisplayCards();
    }

    void Update()
    {
        timeRemaining -= Time.deltaTime;

        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);

        if (timeRemaining <= 0)
        {
            PlayerPrefs.SetInt("currentScore", score);
            PlayerPrefs.Save();

            SceneManager.LoadScene("exit");
        }

        if (timerHasStarted)
        {
            timer += Time.deltaTime;

            if (timer >= 1)
            {
                if (card1.tag == card2.tag)
                {
                    audioSource.Play();

                    Destroy(card1);
                    Destroy(card2);

                    nbMatch++;
                    score++;

                    if (nbMatch == numberOfCards)
                    {
                        PlayerPrefs.SetInt("currentScore", score);
                        PlayerPrefs.Save();

                        SceneManager.LoadScene("exit");
                    }
                }
                else
                {
                    card1.GetComponent<Tile>().HideCard();
                    card2.GetComponent<Tile>().HideCard();
                }

                firstCardSelected = false;
                secondCardSelected = false;

                card1 = null;
                card2 = null;

                rowForCard1 = "";
                rowForCard2 = "";

                timer = 0;
                timerHasStarted = false;
                timerHasElapsed = true;
            }
        }
    }

    public void DisplayCards()
    {
        int[] shuffledArray = CreateShuffledArray();
        int[] shuffledArray2 = CreateShuffledArray();

        for (int i = 0; i < numberOfCards; i++)
        {
            AddACard(0, i, shuffledArray[i]);
            AddACard(1, i, shuffledArray2[i]);
        }
    }

    void AddACard(int row, int rank, int value)
    {
        float cardOriginalScale = card.transform.localScale.x;

        float scaleFactor = (500 * cardOriginalScale) / 100.0f;
        float yScaleFactor = (725 * cardOriginalScale) / 100.0f;

        GameObject cen = GameObject.Find("centerOfScreen");

        Vector3 newPosition = new Vector3(
            cen.transform.position.x + ((rank - numberOfCards / 2) * scaleFactor),
            cen.transform.position.y + ((row - 2 / 2) * yScaleFactor),
            cen.transform.position.z
        );

        GameObject c = (GameObject)Instantiate(
            card,
            newPosition,
            Quaternion.identity
        );

        c.tag = "" + (value + 1);

        c.name = "" + row + "_" + value;

        string cardNumber = "";

        if (value == 0)
            cardNumber = "ace";
        else
            cardNumber = "" + (value + 1);

        string nameOfCard = cardNumber + "_of_hearts";

        Sprite s1 = Resources.Load<Sprite>(nameOfCard);

        GameObject.Find("" + row + "_" + value)
            .GetComponent<Tile>()
            .SetOriginalSprite(s1);
    }

    public int[] CreateShuffledArray()
    {
        int[] newArray = new int[numberOfCards];

        for (int i = 0; i < numberOfCards; i++)
        {
            newArray[i] = i;
        }

        int tmp;

        for (int t = 0; t < numberOfCards; t++)
        {
            tmp = newArray[t];

            int r = Random.Range(t, numberOfCards);

            newArray[t] = newArray[r];
            newArray[r] = tmp;
        }

        return newArray;
    }

    public void CardSelected(GameObject card)
    {
        string row = card.name.Substring(0, 1);

        if (!firstCardSelected)
        {
            card1 = card;
            rowForCard1 = row;

            card1.GetComponent<Tile>().RevealCard();

            firstCardSelected = true;
        }
        else if (!secondCardSelected && row != rowForCard1)
        {
            card2 = card;
            rowForCard2 = row;

            card2.GetComponent<Tile>().RevealCard();

            secondCardSelected = true;

            CheckCards();
        }
    }

    public void CheckCards()
    {
        RunTimer();
    }

    public void RunTimer()
    {
        timerHasElapsed = false;
        timerHasStarted = true;
        timer = 0;
    }

    public void StopGame()
    {
        PlayerPrefs.SetInt("currentScore", score);
        PlayerPrefs.Save();

        SceneManager.LoadScene("exit");
    }
}