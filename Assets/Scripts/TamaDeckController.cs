using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TamaDeckController : MonoBehaviour
{
    public static TamaDeckController instance;
    void Awake(){
        instance = this;
    }
    public List<CardScriptableObject> deckToUse = new List<CardScriptableObject>();
    private List<CardScriptableObject> activeCards = new List<CardScriptableObject>();
    public Card cardToSpawn;
    // Start is called before the first frame update
    void Start()
    {
        SetupDeck();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Y)){
            OpenCardToRaisingArea();
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
    public void OpenCardToRaisingArea(){
        if(activeCards.Count > 0 && RaisingAreaController.instance.settedCard == null){
            Card newCard = Instantiate(cardToSpawn, transform.position + new Vector3(0, .3f, 0), transform.rotation);
            newCard.cardSO = activeCards[0];
            newCard.SetupCard();

            activeCards.RemoveAt(0);

            RaisingAreaController.instance.SetCardOnRaisingArea(newCard);
        }
        
    }
}
