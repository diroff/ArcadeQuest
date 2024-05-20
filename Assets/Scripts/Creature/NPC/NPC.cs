using System.Collections;
using UnityEngine;

public class NPC : Creature
{
    [SerializeField] private Transform[] _points;
    
    [SerializeField] private float _treshold = 1f;
    [SerializeField] private float _waitTime = 2f;

    private IEnumerator _currentState;
    private int _destinationPointIndex;

    protected override void Start()
    {
        base.Start();

        StartState(DoPatrol());
    }

    public IEnumerator DoPatrol()
    {
        while (enabled)
        {
            if (IsOnPoint())
            {
                _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);
                Debug.Log("I'm on the point");
                StartState(Wait());
                yield break;
            }

            var direction = _points[_destinationPointIndex].position - transform.position;
            direction.y = 0;
            SetDirection(direction.normalized);

            yield return null;
        }
    }

    [ContextMenu("Start moving")]
    public void TestMoving()
    {
        StartState(DoPatrol());
    }

    [ContextMenu("Stop moving")]
    public void TestWaiting()
    {
        StartState(Wait());
    }

    private IEnumerator Wait()
    {
        StopMoving();
        yield return new WaitForSeconds(_waitTime);
        StartState(DoPatrol());
    }

    private bool IsOnPoint()
    {
        return (_points[_destinationPointIndex].position - transform.position).magnitude < _treshold;
    }

    private void StartState(IEnumerator coroutine)
    {
        SetDirection(Vector2.zero);

        if (_currentState != null)
            StopCoroutine(_currentState);

        _currentState = coroutine;
        StartCoroutine(coroutine);
    }
}