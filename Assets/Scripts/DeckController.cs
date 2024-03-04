using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.XR;

public class DeckController : MonoBehaviour
{

    public static DeckController instance;

    void Awake(){
        instance = this;
    }

    public List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();
    private List<CardScriptableObject> activeCards = new List<CardScriptableObject>();
    public Card cardToSpawn;
    public int startingCardAmount = 5;
    private float waitBetweenDrawingCards = .2f;

    void Start()
    {
        SetupDeck();
        DrawCardToHand(startingCardAmount);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T)){
            DrawCardToHand();
        }
    }

    public void SetupDeck(){
        activeCards.Clear();

        List<CardScriptableObject> tempDeck = new List<CardScriptableObject>();
        tempDeck.AddRange(deckToUse);

        int iterations = 0;
        while(tempDeck.Count > 0 && iterations < 500){
            int selected = Random.Range(0, tempDeck.Count);
            activeCards.Add(tempDeck[selected]);
            tempDeck.RemoveAt(selected);

            iterations++;
        }
    }

    public void DrawCardToHand(){
        if(activeCards.Count == 0){
            return;
        }
        Card newCard = Instantiate(cardToSpawn, transform.position + new Vector3(0, .3f, 0), transform.rotation);
        newCard.cardSO = activeCards[0];
        newCard.SetupCard();

        activeCards.RemoveAt(0);

        AudioController.instance.PlayDrawSound();
        HandController.instance.AddCardToHand(newCard);
        
    }

    public void DrawCardToHand(int amount){
        StartCoroutine(DrawMultipleCards(amount));
    }

    IEnumerator DrawMultipleCards(int amount){
         for(int i = 0; i < amount; i++){
            AudioController.instance.PlayDrawSound();

            DrawCardToHand();


            yield return new WaitForSeconds(waitBetweenDrawingCards);
        }
    }
}
