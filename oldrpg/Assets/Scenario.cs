using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scenario
// *****************Things Players Can Do *******************
/*Attack / Defend / "Spells" / "Weapons"
* Interact / "Social"
* Gather Info / "Investigate" / "Perceive"
* Support / "Buff" / "Debuff"*/
// ***********************************************************
{
    /// <summary>
    /// References Player to their distance up the tree
    /// </summary>
    ///
    Dictionary<Client, int> ClientList = new Dictionary<Client, int>();

    private bool working;
    

    //ScenarioInfo
    private int CatHP;
    private int CatCharisma;
    private bool IsCatSaved = false;
    private bool IsCatInTree = true;

    private string CatName;
    private int CatInTreeHeight = 20;

    //Mock Player Info

    private int PlayerHeightUpTree;
    private Client PlayerInRangeOfCat = null;
    private Client ClientClimbingTree = null;
    private Client ClientGrabbedCat = null;
    private Client ClientOnGroundWithCat = null;
    private bool ReceiveActiveClient = false;
    private Client ActiveClient = null;

    public string QuestIntro = "There is a cat stuck up in the dang ol tree down the hollar";

    public Scenario()
    {
    }

    public void StartScenario()
    {
        Debug.Log("Starting scenario");

        CatHP = 4;
        CatCharisma = 9;
        CatName = "Todward";
        /*Broadcast("There is a cat stuck up in the dang ol tree down the hollar");*/

        StartGame();
    }

    private void StartGame()
    {
        //TODO: Create initiative order. Perhaps toggle bools that toggle a blank canvas that prevents clicking for those whose turn it is not.  Assign initiative to clients and select them.

        foreach (Client client in ClientList.Keys)
        {
            working = true;
            GiveTurn(client);
            /*EndTurn(client);*/
            /*ScenarioUpdate();*/

        }
    }

    private void GiveTurn(Client client)
    {
        UpdateCards(client);

        client.EnableButtons();
        // TODO: Wait for and act on player input


        TestMe();



        /*        client.DisableButtons();*/
    }

    public void EndTurn(Client client)
    {
        Debug.Log("End Turn Disable Buttons");
        client.DisableButtons();
    }

    private void UpdateCards(Client client)
    {
        if (PlayerInRange(client))
        {
            client.SetInteractOptionText("Grab Kitty");
        }

        if (ClientGrabbedCat == client)
        {
            client.SetSupportOptionText("Climb back down tree with one arm");
        }

        if (ClientOnGroundWithCat == client)
        {
            client.SetSupportOptionText("Set the kitty down");
        }

    }

    public void ScenarioUpdate()
    {
        Debug.Log("ScenarioUpdate");
        if (!isCatAlive())
        {
            ScenarioFailure();
        }
        if (IsCatSaved)
        {
            ScenarioSuccess();
        }
    }

    public void Attack(Client client)
    {
        Debug.Log("Player Attacking Target");
        if (PlayerInRangeOfCat == null)
        {
            client.SetScenarioText("The cat is up in the tree away from your attack.");
        }
        else
        {
            /*Player getPlayer = client.GetPlayer();*/

            /*CatHP = CatHP - getPlayer.GetStrength();*/
            if (isCatAlive())
            {
                client.SetScenarioText("Cat has lived. He coming at you now. Run.");
            }
        }
    }

    public void Interact(Client client)
    {
        /*Player getPlayer = client.GetPlayer();*/
        Debug.Log("Player Interacting with Target");

        if (PlayerInRange(client))
        {
            client.SetScenarioText("Player grabs the cat by the scruff and puts them in their arms.  Now what?\n");
            ClientGrabbedCat = client;
            return;
        }
        
/*        else if (PassDC(CatCharisma, getPlayer.GetCharisma())){
            client.SetScenarioText("You've persuaded the cat to climb down. He purrs warmly in your arms.\n");
            IsCatSaved = true;
        }*/
        else
        {
            client.SetScenarioText("Player attempts to get the kitty to come down. Alas, the cat is stubborn and isn't leaving");
        }
    } 

    public void GatherInfo(Client client)
    {
        /*Player getPlayer = client.GetPlayer();*/
        if (ClientClimbingTree == null)
        {
            client.SetScenarioText("The cat appears to be 20 feet up at the top branch of the tree\n");
        }
        /*Debug.Log("Player Gathering Info On Cat");*/
/*        if (PassDC(CatCharisma, getPlayer.GetWisdom()) && PlayerInRangeOfCat == client)
        {
            client.SetScenarioText(client.ScenarioText.text += string.Format("The cat's name is {0}\n", CatName));
        }*/
        else
        {
            client.SetScenarioText(client.ScenarioText.text += "There's not much else to know\n");
        }
    }

    public void Support(Client client)
    {
        if((PlayerHeightUpTree < CatInTreeHeight) && (ClientGrabbedCat == null))
        {
            Debug.Log("Climbed Tree!");
            ClientClimbingTree = client;
            PlayerHeightUpTree += 10;
            client.SetScenarioText(string.Format("Player climbed 10 feet up the tree. They are {0} feet away from the cat\n", (CatInTreeHeight - PlayerHeightUpTree)));
            return;
        }

        if (ClientGrabbedCat != null && ClientOnGroundWithCat == client)
        {
            if (PlayerHeightUpTree <= 5)
            {
                Debug.Log("MADE IT DOWN");
                client.SetScenarioText("Player has reached the ground with the kitty!");
                ClientOnGroundWithCat = client;
                return;
            }
            else
            {
                Debug.Log("Climbing Down");
                int HeightFromGround = PlayerHeightUpTree - 5;
                client.SetScenarioText(string.Format("Careful as she goes, Player climbs down 5 feet.  They are {0} feet away from the ground now", (HeightFromGround)));
                PlayerHeightUpTree -= 5;
               
                return;
            }
        }

        if (ClientOnGroundWithCat == client)
        {
            client.SetScenarioText("You gently place the cat on the ground.");
            IsCatSaved = true;
        }
        else
        {
            client.SetScenarioText("Player climbed to the top of the tree! They cannot go any higher!");
            return;
        }
        
    }

    private bool isCatAlive()
    {
        return CatHP <= 0;
    }

    private void ScenarioFailure()
    {
        Broadcast("The cat has died.  You have failed the quest. Better luck next time.");
        EndGame();
    }

    private void ScenarioSuccess()
    {
        Broadcast("You've saved the kitty from the tree. and hands you a bag of 50 gold from his pouch");
        EndGame();
        return;
    }

    private void EndGame()
    {
        IsCatSaved = false;
        
        foreach (Client client in ClientList.Keys)
        {
            client.DisableButtons();
        }
    }

    private bool PassDC(int dc, int playerStat)
    {
        return playerStat >= dc;
    }

    private bool PlayerInRange(Client client)
    {   
        return ClientList[client] >= 20;
    }

    public void Broadcast(string text)
    {
        foreach (Client client in ClientList.Keys)
        {
            client.SetScenarioText(text);
        }
    }




    public void AddClientToList(Client client)
    {
        ClientList.Add(client, 0);
    }

    public IEnumerator DoLast(bool working)
    {
        while (working)
        {
            yield return new WaitForSeconds(0.1f);
        }
        Debug.Log("Workin");
    }

    public void TestMe()
    {
        Debug.Log("Dang");
    }
}
