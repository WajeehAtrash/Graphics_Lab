using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swapper : MonoBehaviour
{
    public enum state
    {
        Idle,
        Ready,
        Off
    }
    private PlayerMovement player;
    [SerializeField] Swapper seconedSwapper;
    [SerializeField] private bool isAdmin;
    private state swapperState = state.Idle;
    public PlayerMovement GetPlayer()
    {
        return player;
    }
    public void SetState(state state)
    {
        this.swapperState = state;
    }
    public state GetReady()
    {
        return (swapperState);
    }
    private void OnCollisionEnter(Collision collision)
    {
        player = collision.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            swapperState = state.Ready;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        swapperState = state.Idle;
    }
    private void Update()
    {
        if(isAdmin==true)
        {
            if(swapperState==state.Ready&& seconedSwapper.GetReady()==state.Ready)
            {
                PlayerMovement player2 = seconedSwapper.GetPlayer();
                Vector3 temp = player2.transform.position;
                player2.transform.position = player.transform.position;
                player.transform.position = temp;
                swapperState = state.Off;
                seconedSwapper.SetState(state.Off);
            }
        }
    }
}
