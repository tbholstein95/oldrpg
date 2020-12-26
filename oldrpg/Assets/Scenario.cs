using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario : MonoBehaviour
// *****************Things Players Can Do *******************
/*Attack / Defend / "Spells" / "Weapons"
* Interact / "Social"
* Gather Info / "Investigate" / "Perceive"
* Support / "Buff" / "Debuff"*/
// **********************************************************

{
    //Goes in TextBox during Scenario. Changes as players interact.
    public Text ScenarioText;

    //UI Buttons
    public Button Option1;
    public Button Option2;
    public Button Option3;
    public Button Option4;


    //ScenarioInfo
    private int CatHP;
    private int CatCharisma;
    private bool IsCatSaved = false;
    private string CatName;

    //Mock Player Info
    private int PlayerStrength;
    private int PlayerCharisma;
    private int PlayerWisdom;
    private int PlayerAttackCards;
    private int PlayerInteractCards;
    private int PlayerInfoCards;
    private int PlayerSupportCards;
    // Start is called before the first frame update
    void Start()
    {
        CatHP = 1;
        CatCharisma = 9;
        CatName = "Todward";

        PlayerStrength = 5;
        PlayerCharisma = 20;
        PlayerWisdom = 10;
        PlayerAttackCards = 0;
        PlayerInteractCards = 2;
        PlayerInfoCards = 2;
        PlayerSupportCards = 0;
        ScenarioText.text = "Man there is a dang ol' cat stuck in the tree\n";
        DrawCards();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isCatAlive())
        {
            ScenarioFailure();

        }
        if (IsCatSaved){
            ScenarioSuccess();            
        }
    }

    public void DrawCards()
    {
        Debug.Log("Drawing Cards");
        //Get number of cards for each time, then generate on the screen what their options are.
        Option1.onClick.AddListener(Attack);
        Option2.onClick.AddListener(Interact);
        Option3.onClick.AddListener(GatherInfo);
        Option1.GetComponentInChildren<Text>().text = "Attack";
        Option2.GetComponentInChildren<Text>().text = "Interact";
        Option3.GetComponentInChildren<Text>().text = "Gather Info";
        Option4.GetComponentInChildren<Text>().text = "Support";
    }
    private void Attack()
    {
        Debug.Log("Player Attacking Target");
        Debug.Log("Cat HP = " + CatHP);
        Debug.Log("Player Strength = " + PlayerStrength);
        CatHP = CatHP - PlayerStrength;
        if (isCatAlive())
        {
            ScenarioText.text = "Cat has lived. He coming at you now. Run.";
        }
    }

    private void Interact()
    {
        Debug.Log("Player Interacting with Target");
        Debug.Log("Cat Charsima = " + CatCharisma);
        Debug.Log("Player Charisma =" + PlayerCharisma);
        if (PassDC(CatCharisma, PlayerCharisma)){
            ScenarioText.text = "You've persuaded the cat to climb down. He purrs warmly in your arms.\n";
            IsCatSaved = true;
            
        }
        else
        {
            ScenarioText.text = "The cat is stubborn and isn't leaving";
        }
    } 

    private void GatherInfo()
    {
        Debug.Log("Player Gathering Info On Cat");
        if (PassDC(CatCharisma, PlayerWisdom))
        {
            ScenarioText.text += string.Format("The cat's name is {0}", CatName);
        }
        else
        {
            ScenarioText.text += "You don't know what this cat's name is";
        }
    }

    private void Support()
    {
        return;
    }

    private bool isCatAlive()
    {
        return CatHP > 0;
    }

    private void ScenarioFailure()
    {
        ScenarioText.text = "The cat has died.  You have failed the quest. Better luck next time.";
        EndGame();
    }

    private void ScenarioSuccess()
    {
        ScenarioText.text += "You've saved the kitty from the tree. and hands you a bag of 50 gold from his pouch";
        EndGame();
        return;
    }

    private void EndGame()
    {
        IsCatSaved = false;
    }

    private bool PassDC(int dc, int playerStat)
    {
        return playerStat >= dc;
    }
}
