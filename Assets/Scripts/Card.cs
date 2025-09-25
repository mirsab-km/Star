using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] private Image iconImage;  //reference for icon

    [SerializeField] private Sprite hiddenSprite;  //default icon
    public Sprite iconSprite; //showing icon

    public bool isSelected = false; //check if the the card is selected.

    public CardsManager manager;
    public Animator animator;
    void Start()
    {

    }

    void Update()
    {

    }

    public void SetIconSprite(Sprite sp) //method to update cards icon
    {
        iconSprite = sp;
    }
    public void Show()  //Sets the cards icon as revealed image and mark it as selectecd
    {
        animator.SetTrigger("Flip");
        iconImage.sprite = iconSprite;
        isSelected = true;
    }

    public void Hide()  //Resets the card icon to default and mar it as unselected
    {
        animator.SetTrigger("FlipBack");
        iconImage.sprite = hiddenSprite;
        AudioManager.Instance.NotMatchSound();
        isSelected = false;
    }

    public void Match()
    {
        animator.SetTrigger("Match");
        AudioManager.Instance.MatchSound();
    }

    public void DisableCard()
    {
        gameObject.SetActive(false);
    }

    public void CardClick()  //calls when button click and pass card as parameter
    {
        manager.SelectedCard(this);
        AudioManager.Instance.CardSound();
    }
}
