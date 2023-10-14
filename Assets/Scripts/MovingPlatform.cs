using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] public List<Transform> Waypoints;
    [SerializeField] public float MoveSpeed = 5f;
    private int _currentWaypoint;
    void Start()
    {
        if (Waypoints.Count <= 0) return;
        _currentWaypoint = 0;
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {

        transform.position = Vector3.MoveTowards(transform.position, Waypoints[_currentWaypoint].transform.position,
            (MoveSpeed * Time.deltaTime));

        if (Vector3.Distance(Waypoints[_currentWaypoint].transform.position, transform.position) <= 0)
        {
            _currentWaypoint++;
        }

        if (_currentWaypoint != Waypoints.Count) return;
        Waypoints.Reverse();
        _currentWaypoint = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        other.transform.parent = transform;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.GetComponent<Player>()) return;
        other.transform.parent = null;
    }
}
