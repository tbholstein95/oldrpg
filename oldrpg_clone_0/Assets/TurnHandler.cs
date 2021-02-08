using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TurnHandler : MonoBehaviour
{
    Dictionary<Client, int> ClientList = new Dictionary<Client, int>();

    public Button AttackOption;
    public Button InteractOption;
    public Button GatherInfoOption;
    public Button SupportOption;

    public Text ScenarioText;

    public enum GameState { 
        Idle,
        StartGame,
        PlayerStartTurn, 
        PlayerSelectCard, 
        EndTurn, 
        OtherPlayersTurn 
    }

    [SerializeField]
    protected Client player1;

    [SerializeField]
    protected Player currentTurnPlayer;

    [SerializeField]
    protected Scenario scenario;

    protected void Awake()
    {
        Debug.Log("Awake");
    
    }

    [SerializeField]
    protected GameState gameState = GameState.Idle;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Starting");
        scenario = new Scenario();
        player1 = new Client();
        player1.CreatePlayer();
        ClientList.Add(player1, 0);
        UpdateQuestText(scenario.QuestIntro);
        gameState = GameState.StartGame;
        DisplayCards();
        GameFlow();
        
    }



    public virtual void GameFlow()
    {
        if (gameState > GameState.StartGame)
        {
            Debug.Log("Time to add more");
        }

        switch (gameState)
        {
            case GameState.StartGame:
                Debug.Log("Starting Game");
                OnGameStarted();
                break;

            case GameState.PlayerStartTurn:
                Debug.Log("Player Starting Turn");
                break;

            case GameState.PlayerSelectCard:
                Debug.Log("Waiting For Player to Select Card");
                break;

            case GameState.EndTurn:
                Debug.Log("Player Ending Turn");
                break;

            case GameState.OtherPlayersTurn:
                Debug.Log("It is now the other player's turn.");
                break;
        }
    }

    protected virtual void OnGameStarted()
    {
        Debug.Log("Starting Game in TurnHandler");
        gameState = GameState.PlayerStartTurn;
    }

    protected virtual void PlayerStartTurn()
    {
        Debug.Log("Starting Player's Turn");

    }

    public void DisplayCards()
    {
        Debug.Log("Drawing Cards");
        //Get number of cards for each time, then generate on the screen what their options are.
        AttackOption.onClick.AddListener(Attack);
        SetAttackOptionText("Attack");
        InteractOption.onClick.AddListener(Interact);
        SetInteractOptionText("Interact");
        GatherInfoOption.onClick.AddListener(GatherInfo);
        SetGatherInfoOptionText("Gather Info");
        SupportOption.onClick.AddListener(Support);
        SetSupportOptionText("Support");
    }

    public void SetAttackOptionText(string text)
    {
        AttackOption.GetComponentInChildren<Text>().text = text;
    }

    public void SetInteractOptionText(string text)
    {
        InteractOption.GetComponentInChildren<Text>().text = text;
    }

    public void SetGatherInfoOptionText(string text)
    {
        GatherInfoOption.GetComponentInChildren<Text>().text = text;
    }

    public void SetSupportOptionText(string text)
    {
        SupportOption.GetComponentInChildren<Text>().text = text;
    }

    private void Attack()
    {
        scenario.Attack(player1);
    }

    private void Interact()
    {
        scenario.Interact(player1);
    }

    private void GatherInfo()
    {
        scenario.GatherInfo(player1);
    }

    private void Support()
    {
        scenario.Support(player1);
    }

    private void UpdateQuestText(string text)
    {
        ScenarioText.text = text;
    }

    // Update is called once per frame
    void Update()
    {
/*        switch (gameState)
        {
            case GameState.StartGame:
                Debug.Log("Starting Game");
                OnGameStarted();
                break;

            case GameState.PlayerStartTurn:
                Debug.Log("Player Starting Turn");
                break;

            case GameState.PlayerSelectCard:
                Debug.Log("Waiting For Player to Select Card");
                break;

            case GameState.EndTurn:
                Debug.Log("Player Ending Turn");
                break;

            case GameState.OtherPlayersTurn:
                Debug.Log("It is now the other player's turn.");
                break;
        }*/
    }
}
