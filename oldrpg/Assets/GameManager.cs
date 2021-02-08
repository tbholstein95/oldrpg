using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
{
    public SyncList<uint> UList = new SyncList<uint>();

    public string GameState = "Initialize {}";

    private int ReadyClicks = 0;

    [SyncVar]
    public uint CurrentPlayerTurn;
    public uint GetCurrentPlayer()
    {
        return CurrentPlayerTurn;
    }
    public void SetFirstPlayer()
    {
        CurrentPlayerTurn = UList[0];
    }

    public void SetNextPlayer(int index)
    {
        CurrentPlayerTurn = UList[index];
        Debug.Log("Set Next Player " + CurrentPlayerTurn);
    }

    [Command]
    public void CmdMoveToNextPlayer()
    {
        int curIndex = UList.IndexOf(CurrentPlayerTurn);
        int lengthOfList = UList.Count;
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
            if (ReadyClicks == (UList.Count))
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
