using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    private PlayerMovement player;
    [SerializeField] Swapper seconedSwapper;
    [SerializeField] private bool isAdmin;
    private bool ready = false;
    public PlayerMovement GetPlayer()
    {
        return player;
    }
    public bool GetReady()
    {
        return ready;
    }
    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            ready = true;
            if (seconedSwapper.GetReady())
            {
                PlayerMovement player2 = seconedSwapper.GetPlayer();
                Vector3 temp = player2.transform.position;
                player2.transform.position = player.transform.position;
                player.transform.position = temp;
                ready = false;
            }
        }
        else
        {
            ready = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        ready = false;
    }
}
