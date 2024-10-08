using System.Collections;
using UnityEngine;

public class NPC : Creature
{
    [SerializeField] private CreatureAnimator _animator;

    [Header("Patrol settings")]
    [SerializeField] private Waypoint[] _points;
    [SerializeField] private float _treshold = 1f;

    private IEnumerator _currentState;
    private int _destinationPointIndex;
    private Waypoint _destinationPoint;

    protected override void Start()
    {
        base.Start();

        StartState(DoPatrol());
    }

    public override void StartMove()
    {
        StartState(DoPatrol());
    }

    public override void StopMove()
    {
        if (_currentState != null)
            StopCoroutine(_currentState);

        base.StopMove();
    }

    public IEnumerator DoPatrol()
    {
        _animator.ContinueWalk();

        while (enabled)
        {
            if (IsOnPoint())
            {
                _destinationPoint = _points[_destinationPointIndex];
                _destinationPoint.DoEnterAction();
                _destinationPointIndex = (int)Mathf.Repeat(_destinationPointIndex + 1, _points.Length);

                if (_destinationPoint.WaitTime > 0)
                {
                    StartState(Wait());
                    yield break;
                }
            }

            var direction = _points[_destinationPointIndex].transform.position - transform.position;
            direction.y = 0;
            SetDirection(direction.normalized);

            yield return null;
        }
    }

    private IEnumerator Wait()
    {
        ResetVelocity();
        _destinationPoint.DoWaitAction();
        yield return new WaitForSeconds(_destinationPoint.WaitTime);
        _destinationPoint = null;

        StartState(DoPatrol());
    }

    private bool IsOnPoint()
    {
        return (_points[_destinationPointIndex].transform.position - transform.position).magnitude < _treshold;
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