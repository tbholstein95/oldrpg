using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.UI;

public class GameManager : NetworkBehaviour
<<<<<<< Updated upstream:oldrpg/Assets/Scripts/GameManager.cs
{   
    public SyncList<int> PlayerList = new SyncList<int>();
=======
{
    public SyncList<uint> UList = new SyncList<uint>();
>>>>>>> Stashed changes:oldrpg/Assets/GameManager.cs

    public string GameState = "Initialize {}";

    private int ReadyClicks = 0;

    [SyncVar]
<<<<<<< Updated upstream:oldrpg/Assets/Scripts/GameManager.cs
    public int CurrentPlayerTurn;

    public int GetCurrentPlayer()
=======
    public uint CurrentPlayerTurn;
    public uint GetCurrentPlayer()
>>>>>>> Stashed changes:oldrpg/Assets/GameManager.cs
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

    public void MoveToNextPlayer()
    {
<<<<<<< Updated upstream:oldrpg/Assets/Scripts/GameManager.cs
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
=======
        int curIndex = UList.IndexOf(CurrentPlayerTurn);
        int lengthOfList = UList.Count;
        if((curIndex += 1) < lengthOfList)
>>>>>>> Stashed changes:oldrpg/Assets/GameManager.cs
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
<<<<<<< Updated upstream:oldrpg/Assets/Scripts/GameManager.cs

    public int returnCurrPlayer()
    {
        return PlayerList.IndexOf(CurrentPlayerTurn);
    }
=======
>>>>>>> Stashed changes:oldrpg/Assets/GameManager.cs
}
