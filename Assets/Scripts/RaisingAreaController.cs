using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisingAreaController : MonoBehaviour
{
    public static RaisingAreaController instance;
    void Awake(){
        instance = this;
    }
    public Card settedCard = new Card();

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(settedCard == null){
            return;
        }

        SetupRaisingCard();
    }

    void SetupRaisingCard(){
        settedCard.MoveToPoint(transform.position, transform.rotation);
    }

    public void SetCardOnRaisingArea(Card card){
        settedCard = card;
        settedCard.inHand = false;
        settedCard.isOnRaisingArea = true;
    }
}
