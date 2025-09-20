using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.VisualScripting;
using UnityEngine;

public class CardsManager : MonoBehaviour //this scripts handles the pairing logic
{
    [SerializeField] private Sprite[] sprites; //to store icons
    [SerializeField] private List<Sprite> spritePairs = new List<Sprite>(); //list to hold paired icons

    [SerializeField] private Card cardPrefab;  //card prefab which has Card Script
    [SerializeField] private Transform gridTransform;
    void Start()
    {
        PrepareSprites();
        CreateCards();
    }

    void Update()
    {

    }

    void PrepareSprites() //prepare pairs by adding each icon twice to create matching pairs
    {
        for (int i = 0; i < sprites.Length; i++)
        {
            //Adding sprites 2 times to make it pair
            spritePairs.Add(sprites[i]);
            spritePairs.Add(sprites[i]);
        }
        ShuffleSprites(spritePairs);
    }

    void ShuffleSprites(List<Sprite> spriteList)  //Fisher - Yates Shuffle Method to shuffle the paired List
    {
        for (int i = spriteList.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);

            //Swap the elements
            Sprite temp = spriteList[i];
            spriteList[i] = spriteList[randomIndex];
            spriteList[randomIndex] = temp;
        }
    }

    void CreateCards()  //loops through the spritePairs List and create a card prefab for each sprite and assign the corresponding sprite icon
    {
        for (int i = 0; i < spritePairs.Count; i++)
        {
            Card card = Instantiate(cardPrefab, gridTransform); //Instantiating prefab using a generic overload
            card.SetIconSprite(spritePairs[i]);
        }
    }
}
