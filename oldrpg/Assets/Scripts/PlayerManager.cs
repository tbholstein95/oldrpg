using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class PlayerManager : NetworkBehaviour
{
    public GameManager GameManager;
    public GameObject Card1;
    public GameObject Card2;
    public GameObject PlayerArea;
    public GameObject DropZone;

    public bool isMyTurn = false;

    public int PlayerID;

    [SyncVar]
    public int CurrentPlayer;

    [SyncVar]
    public Scenario Scenario;

    List<GameObject> cards = new List<GameObject>();
    public bool AllowDraw = true;
    public GameObject button;

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerArea = GameObject.Find("PlayerArea");
        DropZone = GameObject.Find("DropZone");

        {
            Debug.Log(x + "hey man im in the Lplayerlist");
        }
    }
    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        Debug.Log("do i make hte erorr");
        
        NetworkIdentity id = this.GetComponent<NetworkIdentity>();
        uint newID = id.netId;
        CmdAddToList(newID);
    }
    [Server]
    public override void OnStartServer()
    {
        Debug.Log("starting server");
        Scenario = new Scenario();
        GameObject ScenarioText = GameObject.Find("QuestText");
/*        ScenarioText.GetComponent<Text>().text = scenario.QuestIntro;*/
        cards.Add(Card1);
        cards.Add(Card2);
    }

    void Update()
    {
        if (GameManager.GameState == "Compile" && NeedSetFirstPlayer)
        {
            RpcSetFirstPlayer();
        }
    }
*/

    [Command]
    public void CmdDealCards()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (AllowDraw)
        {
            GameManager.ChangeReadyClicks();
            for (int i = 0; i < 4; i++)
            {
                /*GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);*/
                GameObject card = Instantiate(cards[0], new Vector2(0, 0), Quaternion.identity);
                NetworkServer.Spawn(card, connectionToClient);
                RpcShowCard(card, "Dealt");
            }
        }
        ChangeAllowDraw();
        RpcGMChangeState("Compile");
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    [Command]
    void CmdPlayCard(GameObject card)
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        /*        Debug.Log(CurrentPlayer);
                if (CurrentPlayer == PlayerID)
                {
                    RpcShowCard(card, "Played");
                }*/
        RpcShowCard(card, "Played");
    }


    [Command]
    public void CmdGMChangeState(string stateRequest)
    {
        RpcGMChangeState(stateRequest);
    }



    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (type == "Dealt")
            {
                if (hasAuthority)
                {
                    card.transform.SetParent(PlayerArea.transform, false);
                }
            }
        else if (type == "Played")
        {



        }

    }

    [ClientRpc]
    void RpcGMChangeState(string stateRequest)
    {
        GameManager.ChangeGameState(stateRequest);

    }

    public void ChangeAllowDraw()
    {
        AllowDraw = false;
    }

    [Command]
    public void CmdAddToList(uint id)
    {
        Debug.Log("let's try adding this chumbo to the list shall we");
        /*PlayerList.Add(this);
        GameManager.PList.Add(this);*/
        GameManager.UList.Add(id);
    }


    /*
        public void SetFirstPlayer()
        {
            RpcSetFirstPlayer();
        }*/

    /*    [ClientRpc]
        public void RpcSetFirstPlayer()
        {
            Debug.Log("Start the show");
            CurrentPlayer = PlayerList[0];
            NeedSetFirstPlayer = false;
        }*/

    public void MoveToNextPlayer()
    {

        CMoveToNextPlayer();
        
    }

    /*    [Command]
        public void CmdMoveToNextPlayer()
        {
            int curIndex = GameManager.UList.IndexOf(GameManager.CurrentPlayerTurn);
            int tempcurIndex = curIndex + 1;
            int lengthOfList = GameManager.UList.Count;

            Debug.Log("curIndex = " + curIndex);
            if ((tempcurIndex) < lengthOfList)
            {
                Debug.Log("tempcurIndex " + tempcurIndex);
                Debug.Log("Trying to move");
                *//*RpcSetNextPlayer(curIndex += 1);*//*
                CmdSetNextPlayer(tempcurIndex);
            }
            else
            {
                Debug.Log("Either 1 player or startin over");
                *//*RpcSetNextPlayer(0);*//*
                CmdSetNextPlayer(0);
            }


        }*/

    public void CMoveToNextPlayer()
    {
        int curIndex = GameManager.UList.IndexOf(GameManager.CurrentPlayerTurn);
        int tempcurIndex = curIndex + 1;
        int lengthOfList = GameManager.UList.Count;

        Debug.Log("curIndex = " + curIndex);
        if ((tempcurIndex) < lengthOfList)
        {
            Debug.Log("tempcurIndex " + tempcurIndex);
            Debug.Log("Trying to move");
/*            RpcSetNextPlayer(curIndex += 1);*/
            SetNextPlayer(tempcurIndex);
        }
        else
        {
            Debug.Log("Either 1 player or startin over");
/*            RpcSetNextPlayer(0);*/
            SetNextPlayer(0);
        }


    }

    public void SetNextPlayer(int index)
    {
        CmdSetNextPlayer(index);
    }

    [Command]
    public void CmdSetNextPlayer(int index)
    {
        Debug.Log("CmdSetNextPlayer made it");
        Debug.Log("Index = " + index);
        Debug.Log(GameManager.UList.Count + "gamemanager list count");
        GameManager.CurrentPlayerTurn = GameManager.UList[index];
    }

    /*    [ClientRpc]
        public void RpcSetNextPlayer(int index)
        {
            CurrentPlayer = PlayerList[index];
            Debug.Log("Set Next Player " + CurrentPlayer);
        }*/

    [ClientRpc]
    public void RpcMoveToNextPlayer()
    {
        int curIndex = GameManager.returnCurrPlayer();
        int tempIndex = curIndex;
        int lengthOfList = GameManager.PlayerList.Count;
        Debug.Log(curIndex.GetType() + "typeof of curIndex");
        int nextIndex = (curIndex + 1);
        Debug.Log(lengthOfList + "Length of LIst");
        Debug.Log(curIndex + "Curr Index");
        Debug.Log(tempIndex + "temp index");
        Debug.Log(nextIndex + "next index");
        if ((nextIndex) < lengthOfList)
        {
            Debug.Log("Attempting to move");
            GameManager.SetNextPlayer(curIndex += 1);
            Debug.Log("It moved");
            CurTurn = GameManager.CurrentPlayerTurn;
            Debug.Log(CurTurn);
        }
        else
        {
            Debug.Log("Ruh roh, something went wrong with changin it");
            GameManager.SetNextPlayer(0);
        }
    }


}
