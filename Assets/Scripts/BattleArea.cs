using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleArea : MonoBehaviour
{
    public List<Card> placedCards = new List<Card>();
    int placedCardsCount;
    [SerializeField] public bool isMine;
    public Transform minPos, maxPos;

    public List<Vector3> cardPositions = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {
        placedCardsCount = placedCards.Count;
    }

    public void SetCardPositionsInBattleArea(){
        cardPositions.Clear();

        Vector3 distanceBetweenPoints = Vector3.zero;

        if(placedCards.Count > 1){
            distanceBetweenPoints = (maxPos.position - minPos.position) / (placedCards.Count - 1);
        }

        if(placedCards.Count == 1){
            cardPositions.Add((maxPos.position + minPos.position) /2);
            placedCards[0].MoveToPoint(cardPositions[0], minPos.rotation);
            placedCards[0].isOnBattleArea = true;


        } else{
            for(int i = 0; i < placedCards.Count; i++){
            cardPositions.Add(minPos.position + (distanceBetweenPoints * i));

            placedCards[i].MoveToPoint(cardPositions[i], minPos.rotation);

            placedCards[i].isOnBattleArea = true;

            placedCards[i].fieldPosition = i;
        }
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if(placedCards.Count != placedCardsCount){
            placedCardsCount = placedCards.Count;
            SetCardPositionsInBattleArea();
        }
    }
}
