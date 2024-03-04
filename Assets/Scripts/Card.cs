using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.Mathematics;
using UnityEngine.XR;
using UnityEditor;
using System.Runtime.Serialization.Formatters;
using System.Runtime.CompilerServices;
using UnityEngine.Assertions.Must;

public class Card : MonoBehaviour
{
    public CardScriptableObject cardSO;
    public string cardNum;
    public string cardName;
    public int DP;
    public int appearCost;
    public int digivolveCost;
    public int LV;

    

    public GameObject cardFront;
    public GameObject cardBack;
    private Vector3 targetPoint;
    private Quaternion targetRot;
    public float moveSpeed = 3f;
    public float rotateSpeed = 540f;
    public bool inHand;
    public int handPosition;
    public int fieldPosition;

    private bool isSelected;
    private Collider theCol;
    public LayerMask whatIsDesktop, whatIsBattleArea, whatIsCard;
    public List<CardScriptableObject> inheriteds;
    public bool justPressed;
    public bool isOnBattleArea;
    public bool isOnRaisingArea;



    private HandController theHC;
    
    
    // Start is called before the first frame update
    void Start()
    {
        SetupCard();

        theHC = FindObjectOfType<HandController>();
        theCol = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, targetPoint, moveSpeed * Time.deltaTime);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRot, rotateSpeed * Time.deltaTime);

        if(isSelected){
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if(Physics.Raycast(ray, out hit, 100f, whatIsDesktop)){
                MoveToPoint(hit.point, Quaternion.identity);
            }

            if(Input.GetMouseButtonDown(1)){
                ReturnToHand();
            }

            if(Input.GetMouseButtonDown(0) && !justPressed){
                if(Physics.Raycast(ray, out hit, 1000f, whatIsCard)){
                    EvolveFromDigimon(hit);
                } else if(Physics.Raycast(ray, out hit, 100f, whatIsBattleArea)){
                    AppearOnBattleArea(hit);
                } else {
                    ReturnToHand();
                }
            }
        }
        
        justPressed = false;
    }

    public void SetupCard(){
        cardFront.GetComponent<SpriteRenderer>().sprite = cardSO.Front;
        cardBack.GetComponent<SpriteRenderer>().sprite = cardSO.Back;

        cardName = cardSO.cardName;
        cardNum = cardSO.cardNum;
        
        digivolveCost = cardSO.digivolveCost;
        LV = cardSO.LV;

        appearCost = cardSO.appearCost;
        DP = cardSO.DP;

    }

    public void MoveToPoint(Vector3 PointToMoveTo, Quaternion rotToMatch){
        targetPoint = PointToMoveTo;
        targetRot = rotToMatch;
    }

    private void OnMouseOver(){
        if(inHand){
            MoveToPoint(theHC.cardPositions[handPosition] + new Vector3(0f, 1f, .2f), Quaternion.identity);
        }
    }

    private void OnMouseExit(){
        if(inHand){
            MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
        }
    }

    private void OnMouseDown(){
        if(inHand){
            isSelected = true;
            theCol.enabled = false;

            justPressed = true;
        }
    }

    private void ReturnToHand(){
        isSelected = false;
        theCol.enabled = true;

        MoveToPoint(theHC.cardPositions[handPosition], theHC.minPos.rotation);
    }


    private void EvolveFromDigimon(RaycastHit hit){
        Card card = hit.collider.GetComponent<Card>();

        if(!card.inHand){
            if(card.LV == LV-1 && GameController.instance.PlayerMemory - digivolveCost >= GameController.instance.PlayerMinMemory){

                theHC.RemoveCardFromHand(this);


                card.inheriteds.Add(card.cardSO);
                card.cardSO = cardSO;
                card.SetupCard();

                justPressed = false;

                GameController.instance.SpendMemory(digivolveCost);
                DeckController.instance.DrawCardToHand();

                Destroy(gameObject);
            } else{
                ReturnToHand();
            }
        }
    }

    private void AppearOnBattleArea(RaycastHit hit){
        BattleArea area = hit.collider.GetComponent<BattleArea>();

        if(area.isMine){
            if(GameController.instance.PlayerMemory - appearCost >= GameController.instance.PlayerMinMemory){
                inHand = false;
                isSelected = false;
                theHC.RemoveCardFromHand(this);
                handPosition = 0;

                area.placedCards.Add(this);
                theCol.enabled = true;

                GameController.instance.SpendMemory(appearCost);
            } else{
                ReturnToHand();

            }
        } else{
                ReturnToHand();
        }
    }
}
