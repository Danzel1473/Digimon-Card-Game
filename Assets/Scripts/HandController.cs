using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using UnityEngine.UIElements;

public class HandController : MonoBehaviour
{
    public static HandController instance;

    public void Awake(){
        instance = this;
    }

    public List<Card> heldCards = new List<Card>();
    public Transform minPos, maxPos;
    public List<UnityEngine.Vector3> cardPositions = new List<UnityEngine.Vector3>();

    // Start is called before the first frame update
    void Start()
    {
        SetCardPositionsInHand();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetCardPositionsInHand(){
        cardPositions.Clear();
        UnityEngine.Vector3 distanceBetweenPoints = UnityEngine.Vector3.zero;

        if(heldCards.Count == 0)
            return;

        if(heldCards.Count > 1){
            distanceBetweenPoints = (maxPos.position - minPos.position) / (heldCards.Count - 1);
        }

        for(int i = 0; i < heldCards.Count; i++){
            cardPositions.Add(minPos.position + (distanceBetweenPoints * i));

            heldCards[i].MoveToPoint(cardPositions[i], minPos.rotation);

            heldCards[i].inHand = true;
            heldCards[i].handPosition = i;
        }
        
    }

    public void RemoveCardFromHand(Card cardToRemove){
        if(heldCards[cardToRemove.handPosition] == cardToRemove){
            heldCards.RemoveAt(cardToRemove.handPosition);
        } else{
            Debug.LogError("Card at position " + cardToRemove.handPosition + "is not the card being removed from hand");
        }

        SetCardPositionsInHand();
    }

    public void AddCardToHand(Card cardToAdd){
        heldCards.Add(cardToAdd);
        SetCardPositionsInHand();
    }
}
