using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Card", menuName = "Card", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public string cardNum;
    public string cardName;
    public int DP;
    public int LV;
    public int appearCost;
    public int digivolveCost;


    public Sprite Front;
    public Sprite Back;

    void Start()
    {
        SetupCard();
    }

    void SetupCard(){
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
