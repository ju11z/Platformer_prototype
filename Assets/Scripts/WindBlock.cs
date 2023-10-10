using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindBlock : MonoBehaviour
{
    public float UpdateWindInterval = 2.0f;
    private float windForce;
    private Vector3 windDirection;
    private void OnCollisionStay(Collision collision)
    {
        
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            player.BeUnderWindInfluence(windDirection, windForce);
            //Debug.Log("player on windblock");
        }
    }
    

    private void Start()
    {
        InvokeRepeating("UpdateWind", 0f, UpdateWindInterval);
    }

    private void UpdateWind()
    {
        //Debug.Log("update win");

        float rX = Random.Range(1, 3);
        float rZ = Random.Range(1, 3);

        float rF = Random.Range(1, 5);

        windDirection = new Vector3(rX, 0, rZ);
        windForce = rF;
    }
}
