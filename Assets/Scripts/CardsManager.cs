using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardsManager : MonoBehaviour //this scripts handles the pairing logic
{
    public static CardsManager Instance;

    public int rows;
    public int columns;
    [SerializeField] private Sprite[] sprites; //to store icons
    [SerializeField] private List<Sprite> spritePairs = new List<Sprite>(); //list to hold paired icons
    [SerializeField] private Card cardPrefab;  //card prefab which has Card Script
    [SerializeField] private Transform gridTransform;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private Card firstCard;
    private Card secondCard;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        gridLayoutGroup = gridTransform.GetComponent<GridLayoutGroup>();
    }

    void Start()
    {
        PrepareCards();
        PrepareGrid();
        GameManager.Instance.totalPairs = (rows * columns) / 2;
    }

    void Update()
    {
        
    }

    void PrepareCards() //prepare pairs by adding each icon twice to create matching pairs
    {
        int totalCards = rows * columns;
        int pairsNeeded = totalCards / 2;

        for (int i = 0; i < pairsNeeded; i++)
        {
            //Adding sprites 2 times to make it pair
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        ShuffleSprites(spritePairs);

        //Create cards 
        for (int i = 0; i < totalCards; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform); //Instantiating prefab using a generic overload
            card.SetIconSprite(spritePairs[i]);
            card.manager = this;
        }
    }

    void PrepareGrid()
    {
        if (gridLayoutGroup == null)
            gridLayoutGroup = gridTransform.GetComponent<GridLayoutGroup>();

        RectTransform rt = gridTransform.GetComponent<RectTransform>();

        // total available size of the grid panel
        float gridWidth = rt.rect.width;
        float gridHeight = rt.rect.height;

        // padding & spacing from inspector
        RectOffset padding = gridLayoutGroup.padding;
        Vector2 spacing = gridLayoutGroup.spacing;

        // calculate available width/height after padding and spacing
        float availableWidth = gridWidth - padding.left - padding.right - (columns - 1) * spacing.x;
        float availableHeight = gridHeight - padding.top - padding.bottom - (rows - 1) * spacing.y;

        // calculate cell size
        float cellWidth = availableWidth / columns;
        float cellHeight = availableHeight / rows;

        // choose the smaller value so cards are always square-ish
        float cardSize = Mathf.Min(cellWidth, cellHeight);

        // apply scale factor (to prevent edge-to-edge stretching)
        float scaleFactor = 0.85f; // 85% of the calculated size
        cardSize *= scaleFactor;

        // clamp to max size so 2x2 doesn’t look oversized
        float maxCardSize = 220f; // pixels (tweak as you like)
        cardSize = Mathf.Min(cardSize, maxCardSize);

        // apply to grid layout
        gridLayoutGroup.constraint = GridLayoutGroup.Constraint.FixedColumnCount;
        gridLayoutGroup.constraintCount = columns;
        gridLayoutGroup.cellSize = new Vector2(cardSize, cardSize);
    }

    void ShuffleSprites(List<Sprite> list)  //Fisher - Yates Shuffle Method to shuffle the paired List
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            //Swap the elements
            Sprite temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public void SelectedCard(Card card)
    {
        if (card == firstCard || card == secondCard)  //prevents selecting the same card twice
        {
            return;
        }

        card.Show();

        if (firstCard == null)
        {
            firstCard = card;
            return;
        }

        if (secondCard == null)
        {
            secondCard = card;
            StartCoroutine(CardMatch(firstCard, secondCard));

            // Reset references immediately so new cards can be selected
            firstCard = null;
            secondCard = null;
        }
    }



    private IEnumerator CardMatch(Card a, Card b)
    {

        yield return new WaitForSeconds(0.6f); // wait to show cards

        if (a.iconSprite == b.iconSprite)
        {
            a.Match();
            b.Match();
            GameManager.Instance.CardMatched(10);
        }
        else
        {
            GameManager.Instance.CardMismatched(5);
            a.Hide();
            b.Hide();
        }
    }
}
