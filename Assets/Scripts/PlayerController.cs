using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player player;

    private bool playerIsControllable=true;

    public void SetPlayerIsControllable(bool controllable)
    {
        playerIsControllable = controllable;
    }
    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        if (!player)
            return;

        if (!playerIsControllable)
            return;

        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        player.Move(moveX, moveZ);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.Jump();
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            player.BeDamaged(1);
        }

        /*
        if (Input.GetKey(KeyCode.F))
        {
            player.BeUnderWindInfluence(new Vector3(0.3f,0.5f,0.1f), 30);
        }
        */
    }
}
