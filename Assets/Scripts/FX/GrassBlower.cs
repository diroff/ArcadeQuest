using UnityEngine;

public class GrassBlower : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private ParticleSystem _particle;
    [SerializeField] private float _distanceBehind = 1f;

    private bool _isPlayerOnGrass = false;

    private void Update()
    {
        if (_isPlayerOnGrass)
        {
            Vector3 backward = -_player.transform.forward * _distanceBehind + _player.transform.position;
            _particle.transform.position = backward;

            bool isMoving = _player.RigidBody.velocity.magnitude > 0.1f;
            if (isMoving && !_particle.isPlaying)
                _particle.Play(true);
            else if (!isMoving && _particle.isPlaying)
                _particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Player>() == _player)
        {
            _isPlayerOnGrass = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Player>() == _player)
        {
            _isPlayerOnGrass = false;
            _particle.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        }
    }
}