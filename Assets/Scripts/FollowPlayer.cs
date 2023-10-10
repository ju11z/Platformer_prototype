using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform target;

    public Vector3 offset = new Vector3(0, 2, -10);


    private void LateUpdate()
    {
        if (!target)
            return;
        transform.position = target.position + offset;
    }
}
