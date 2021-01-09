using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Client
    //Client starts a new scenario is one isn't running. Handles UI updates. Delegates player logic tasks.
{
    public Text ScenarioText;

    public Button AttackOption;
    public Button InteractOption;
    public Button GatherInfoOption;
    public Button SupportOption;
    private Player Player1;
    private Player Player2;
    public Scenario Scenario;
    public Button testMe;
    bool choicemade;

/*    void Start()
    {
        Debug.Log("Starting client");
        Player = new Player();
        Scenario = new Scenario();
        Scenario.AddClientToList(this);
        
        DisplayCards();

        // TODO: Add confirmation all players are connected, let player decide when to start scenario
        Scenario.StartScenario();

    }*/

    public void CreatePlayer()
    {
        Player1 = new Player();
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

/*    public Player GetPlayer()
    {
        return Player;
    }*/


    public IEnumerator Dialog(bool working)
    {
        // ...
        Debug.Log("Dialoging");
        choicemade = false;
        var waitForButton = new WaitForUIButtons(AttackOption, InteractOption, GatherInfoOption, SupportOption);
        yield return new WaitForUIButtons(AttackOption, InteractOption, GatherInfoOption, SupportOption);

        /*yield return waitForButton.Reset();*/

        if (waitForButton.PressedButton == AttackOption)
        {
            Debug.Log("Attack from Wait For Button");
            Attack();
            working = false;
            Scenario.EndTurn(this);
        }
        if (waitForButton.PressedButton == InteractOption)
        {
            Debug.Log("Interact from Wait For Button");
            Interact();
            working = false;

        }
        if (waitForButton.PressedButton == GatherInfoOption)
        {
            Debug.Log("GatherInfo from Wait For Button");
            GatherInfo();
            working = false;
        }
        if (waitForButton.PressedButton == SupportOption)
        {
            Debug.Log("Supported from Wait For Button");
            Support();
            working = false;
        }

        while (working)
        {
            yield return null;
        }

        
        // ...
    }

    public IEnumerator DoLast(bool working)
    {
        while (working)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Workin");
    }
}
