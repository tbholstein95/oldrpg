using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{   
    public SyncList<int> PlayerList = new SyncList<int>();

    public string GameState = "Initialize {}";
    private int ReadyClicks = 0;

    [SyncVar]
    public int CurrentPlayerTurn;

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
        Debug.Log("Set Next Player " + CurrentPlayerTurn);
    }

    public void MoveToNextPlayer()
    {
        int curIndex = PlayerList.IndexOf(CurrentPlayerTurn);
        int lengthOfList = PlayerList.Count;
        int nextIndex = (curIndex += 1);
        Debug.Log(lengthOfList + "Length of LIst");
        Debug.Log(curIndex + "Curr Index");
        Debug.Log((curIndex += 1) + "Cur Index + 1");
        Debug.Log(PlayerList[1] + "Playerlist + 1");
        foreach(int x in PlayerList)
        {
            Debug.Log(x + "Debugging move to next player");
        }
        if (nextIndex < lengthOfList)
        {
            Debug.Log("Attempting to move");
            CurrentPlayerTurn = PlayerList[1];
            Debug.Log("It moved");
        }
        else
        {
            Debug.Log("Ruh roh, something went wrong with changin it");
            SetNextPlayer(0);
        }
    }

    public void ChangeGameState(string stateRequest)
    {
        Debug.Log("Total number of ready clicks = " + ReadyClicks);
        if (stateRequest == "Initialize {}")
        {
            ReadyClicks = 0;
            GameState = "Initialize {}";
        }

        else if (stateRequest == "Compile")
        {
            Debug.Log("CHange game state made it to stateRequest of Compile");
            if (ReadyClicks == (PlayerList.Count))
            {
                Debug.Log("ChangeGameState to Compile");
                GameState = "Compile";
                SetFirstPlayer();
            }
        }

        else if (stateRequest == "Execute {}")
        {
            GameState = "Execute {}";
        }
    }

    public void ChangeReadyClicks()
    {
        ReadyClicks += 1;
        Debug.Log("Adding a ready click");
        Debug.Log(ReadyClicks);
    }

    public int returnCurrPlayer()
    {
        return PlayerList.IndexOf(CurrentPlayerTurn);
    }
}
