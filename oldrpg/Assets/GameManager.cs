using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public List<int> PlayerList = new List<int>();
    public string GameState = "Initialize {}";
    private int ReadyClicks = 0;

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
        Debug.Log("Set Next Player " + CurrentPlayerTurn);
    }

    [Command]
    public void CmdMoveToNextPlayer()
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



}
