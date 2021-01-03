using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client : MonoBehaviour
    //Client starts a new scenario is one isn't running. Handles UI updates. Delegates player logic tasks.
{
    public Text ScenarioText;

    public Button AttackOption;
    public Button InteractOption;
    public Button GatherInfoOption;
    public Button SupportOption;
    private Player Player;
    public Scenario Scenario;

    void Start()
    {
        Debug.Log("Starting client");
        Player = new Player();

        Scenario = new Scenario();
        Scenario.AddClientToList(this);
        DisplayCards();

        // TODO: Add confirmation all players are connected, let player decide when to start scenario
        Scenario.StartScenario();
    }

    void Update()
    {
    }

    public void SetScenarioText(string text)
    {
        ScenarioText.text = text;
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

    public void DisableButtons()
    {
        Debug.Log("Disabling buttons for client");
        AttackOption.enabled = false;
        InteractOption.enabled = false;
        GatherInfoOption.enabled = false;
        SupportOption.enabled = false;
    }

    public void EnableButtons()
    {
        Debug.Log("Enabling buttons for client");
        AttackOption.enabled = true;
        InteractOption.enabled = true;
        GatherInfoOption.enabled = true;
        SupportOption.enabled = true;
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
        Scenario.Attack(this);
    }

    private void Interact()
    {
        Scenario.Interact(this);
    }

    private void GatherInfo()
    {
        Scenario.GatherInfo(this);
    }

    private void Support()
    {
        Scenario.Support(this);
    }

    public Player GetPlayer()
    {
        return Player;
    }
}
