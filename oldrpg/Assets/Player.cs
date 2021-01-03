using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player
{
    private string Name;
    public int Strength;
    public int Charisma;
    public int Wisdom;

    private int Intelligence;
    private int AttackCards;
    private int InteractCards;
    private int InfoCards;
    private int SupportCards;


    public Player()
    {
        SetTestPlayerStats();
        Debug.Log("Created a player");
    }
    private void SetTestPlayerStats()
    {
        Name = "Franklin Turtle";
        Strength = 1;
        Charisma = 5;
        Intelligence = 10;
    }

    public void test()
    {
        Debug.Log("Hey big man I work");
    }
    

    public int GetStrength()
    {
        return Strength;
    }
    private void SetStrength(int value)
    {
        Strength = value;
    }

    public int GetWisdom()
    {
        return Wisdom;
    }

    public int GetCharisma()
    {
        return Charisma;
    }
}
