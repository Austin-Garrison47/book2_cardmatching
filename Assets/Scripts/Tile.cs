using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool tileRevealed = false;

    public Sprite originalSprite;
    public Sprite hiddenSprite;

    void Start()
    {
        HideCard();
    }

    public void HideCard()
    {
        GetComponent<SpriteRenderer>().sprite = hiddenSprite;
        tileRevealed = false;
    }

    public void RevealCard()
    {
        GetComponent<SpriteRenderer>().sprite = originalSprite;
        tileRevealed = true;
    }

    public void OnMouseDown()
    {
        GameObject.Find("gameManager")
            .GetComponent<ManageCards>()
            .CardSelected(gameObject);
    }

    public void SetOriginalSprite(Sprite newSprite)
    {
        originalSprite = newSprite;
    }
}
