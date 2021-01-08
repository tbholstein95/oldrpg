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
    // Start is called before the first frame update
    void Start()
    {
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
            transform.SetParent(DropZone.transform, false);
            isDraggable = false;
            NetworkIdentity networkIdentity = NetworkClient.connection.identity;
            PlayerManager = networkIdentity.GetComponent<PlayerManager>();
            PlayerManager.PlayCard(gameObject);
        }
        else
        {
            transform.position = startPosition;
        }
    }
}
