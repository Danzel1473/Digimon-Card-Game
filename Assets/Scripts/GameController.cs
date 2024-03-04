using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    public enum Phase {TurnStart, ActivePhase, RaisingPhase, MainPhase, EndPhase};
    public Phase currentPhase;

    private void Awake(){
        instance = this;
    }

    public int PlayerMemory;
    public int PlayerMaxMemory = 10;
    public int PlayerMinMemory = -10;

    void Start()
    {
        PlayerMemory = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpendMemory(int amount){
        PlayerMemory -= amount;
    }
}
