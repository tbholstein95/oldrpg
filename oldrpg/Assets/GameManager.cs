using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class GameManager : NetworkBehaviour
{
    public List<int> PlayerList = new List<int>();

    [SyncVar]
    private int CurrentPlayerTurn;

    public int GetCurrentPlayer()
    {
        return CurrentPlayerTurn;
    }

    public void SetFirstPlayer()
    {
        CurrentPlayerTurn = PlayerList[0];
    }

    public void SetNextPlayer(int index)
    {
        CurrentPlayerTurn = PlayerList[index];
    }

    public void MoveToNextPlayer()
    {
        int curIndex = PlayerList.IndexOf(CurrentPlayerTurn);
        int lengthOfList = PlayerList.Count;
        if((curIndex += 1) < lengthOfList)
        {
            SetNextPlayer(curIndex += 1);
        }
        else
        {
            SetNextPlayer(0);
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
