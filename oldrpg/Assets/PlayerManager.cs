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


    public override void OnStartClient()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        base.OnStartClient();
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
        for (int i = 0; i < 4; i++)
        {
            GameObject card = Instantiate(cards[Random.Range(0, cards.Count)], new Vector2(0, 0), Quaternion.identity);
            NetworkServer.Spawn(card, connectionToClient);
            RpcShowCard(card, "Dealt");
        }
    }

    public void PlayCard(GameObject card)
    {
        CmdPlayCard(card);
    }

    public void SetMyTurn()
    {
        CmdSetMyTurn();
    }

    [Command]

    void CmdPlayCard(GameObject card)
    {   
        if (GameManager.GetCurrentPlayer() == PlayerID)
        {
            RpcShowCard(card, "Played");
        }
        
    }

    [Command]
    void CmdSetMyTurn()
    {
        isMyTurn = true;
    }

    [ClientRpc]
    void RpcShowCard(GameObject card, string type)
    {
            if (type == "Dealt")
            {
                if (hasAuthority)
                {
                    card.transform.SetParent(PlayerArea.transform, false);
                }
            }

            else if (type == "Played")
            {
                card.transform.SetParent(DropZone.transform, false);
                GameManager.MoveToNextPlayer();
            }
    }



}
