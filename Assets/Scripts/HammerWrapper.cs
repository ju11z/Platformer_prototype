using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerWrapper : MonoBehaviour
{
    public int HammerDamage=2;
    public float OrbitSpeed = 10.0f;
    public Transform rotateAround;
    public Transform hammer;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = hammer.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        Quaternion q = Quaternion.AngleAxis(OrbitSpeed, transform.forward);
        rb.MovePosition(q * (rb.transform.position - rotateAround.position) + rotateAround.position);
        rb.MoveRotation(rb.transform.rotation * q);
    }

    public void CollisionDetected(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("damage");
            player.BeDamaged(HammerDamage);

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Player>(out Player player))
        {
            Debug.Log("damage");
            player.BeDamaged(HammerDamage);

        }

        //Debug.Log(players.Count);
    }
}
