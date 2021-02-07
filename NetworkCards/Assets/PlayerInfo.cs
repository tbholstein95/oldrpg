using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class PlayerInfo : NetworkBehaviour
{
    public int ID;
    // Start is called before the first frame update
    void Start()
    {
        int randNum = Random.Range(0, 100);
        ID = randNum;
        Debug.Log(randNum);
        Debug.Log(ID);
    }

    public override void OnStartClient()
    {
        DontDestroyOnLoad(this);
        base.OnStartClient();
        Debug.Log("Client Network Player Start");
        NewNetworkManager.
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
