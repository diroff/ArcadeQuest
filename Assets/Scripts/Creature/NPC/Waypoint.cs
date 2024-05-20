using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    [SerializeField] private float _waitTime;

    public float WaitTime => _waitTime;
}
