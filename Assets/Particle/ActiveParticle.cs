using System;
using UnityEngine;

public class ActiveParticle : MonoBehaviour
{
    [SerializeField] ParticleSystem _particleSystem;
    [SerializeField] bool _active;

    public Action OnPlay;
    public Action OnStop;

    public bool Active
    {
        get
        {
            return _active;
        }
        set
        {
            _active = value;
            ActiveParticleSystem(_active);
        }
    }

    void Start()
    {
        Active = _active;
    }

    public void ActiveParticleSystem(bool active)
    {
        if(active)
        {
            _particleSystem.Play();
            OnPlay?.Invoke();
        }
        else
        {
            _particleSystem.Stop();
            OnStop?.Invoke();
        }
    }
}
