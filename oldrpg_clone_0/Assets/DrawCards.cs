using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DrawCards : NetworkBehaviour
{
    public PlayerManager PlayerManager;
    public GameManager GameManager;

    public void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    public void OnClick()
    {
        NetworkIdentity networkIdentity = NetworkClient.connection.identity;
        PlayerManager = networkIdentity.GetComponent<PlayerManager>();
        Debug.Log(GameManager.GameState + "I AM THE GAMESTATE");

        if (GameManager.GameState == "Initialize {}")
        {
            if (PlayerManager.AllowDraw)
            {
                InitializeClick();
            }
        }

        if (GameManager.GameState == "Compile")
        {
            Debug.Log("wooooo doggy");
        }

        else if (GameManager.GameState == "Execute {}")
        {
            ExecuteClick();
        }
    }

    void InitializeClick()
    {
        Debug.Log("Initialize Click");
        PlayerManager.CmdDealCards();
    }

    void ExecuteClick()
    {

    }


}
