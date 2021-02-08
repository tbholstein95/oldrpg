using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DragDrop : NetworkBehaviour
{
    private bool isDragging = false;
    private bool isDraggable = true;
    private Vector2 startPosition;
    private bool isOverDropZone = false;
    public GameObject DropZone;
    public PlayerManager PlayerManager;
    public GameManager GameManager;
    // Start is called before the first frame update
    void Start()
    {
        GameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        DropZone = GameObject.Find("DropZone");
        if (!hasAuthority)
        {
            isDraggable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isDragging)
        {
            transform.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        isOverDropZone = true;
        DropZone = collision.gameObject;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        isOverDropZone = false;
        DropZone = null;
    }

    public void StartDrag()
    {
        if (!isDraggable)
        {
            return;
        }
        startPosition = transform.position;
        isDragging = true;
    }

    public void EndDrag()
    {
        if (!isDraggable)
        {
            return;
        }
        isDragging = false;
        if (isOverDropZone)
        {
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            NetworkIdentity playerNetworkIdentityObject = PlayerManager.GetComponent<NetworkIdentity>();
            uint playerNetID = playerNetworkIdentityObject.netId;
            Debug.Log(GameManager.GetCurrentPlayer() + "current player id");
            if (playerNetID == GameManager.CurrentPlayerTurn)
            {
                transform.SetParent(DropZone.transform, false);
                isDraggable = false;
                PlayerManager.PlayCard(gameObject);
                if (hasAuthority)
                {
                    PlayerManager.MoveToNextPlayer();
                }
                
            }
            else
            {
                transform.position = startPosition;
            }            
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
