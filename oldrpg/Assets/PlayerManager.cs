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
    List<GameObject> cards = new List<GameObject>();
    public bool AllowDraw = true;
    public GameObject button;

    public override void OnStartClient()
    {
        base.OnStartClient();
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        PlayerArea = GameObject.Find("PlayerArea");
        DropZone = GameObject.Find("DropZone");
        PlayerID = Random.Range(0, 1000);
        
        GameManager.PlayerList.Add(PlayerID);
        foreach (int x in GameManager.PlayerList)
        {
            Debug.Log(x);
        }
        
    }

    [Server]
    public override void OnStartServer()
    {
        Scenario scenario = new Scenario();
        GameObject ScenarioText = GameObject.Find("QuestText");
        ScenarioText.GetComponent<Text>().text = scenario.QuestIntro;
        cards.Add(Card1);
        cards.Add(Card2);

    } 

    [Command]
    public void CmdDealCards()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if (AllowDraw)
        {
            GameManager.ChangeReadyClicks();
            for (int i = 0; i < 4; i++)
            {
                GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);
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
        Debug.Log(GameManager.GetCurrentPlayer());
        if (GameManager.GetCurrentPlayer() == PlayerID)
        {
            RpcShowCard(card, "Played");
        }
        
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
            if (PlayerID == GameManager.GetCurrentPlayer())
            {
                Debug.Log(PlayerID + "player id");
                Debug.Log(GameManager.GetCurrentPlayer() + "current player");
                Debug.Log("Played");
                card.transform.SetParent(DropZone.transform, false);
                GameManager.CmdMoveToNextPlayer();
            }
                
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


}
