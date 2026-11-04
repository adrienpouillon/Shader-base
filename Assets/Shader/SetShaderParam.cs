using System;
using UnityEngine;

public class ColorOrTexture : MonoBehaviour
{
    [Header("Config")]
    [SerializeField] string _nameParam;
    [SerializeField] float _start;
    [SerializeField] float _end;
    [SerializeField] float _duration;
    [SerializeField] bool _useMultilaunch;
    [SerializeField] bool _launch;

    [Header("Component required")]
    [SerializeField] Renderer _rendererMesh;

    public Action OnStart;
    public Action OnUpdate;
    public Action OnFinish;

    MaterialPropertyBlock block;

    public bool Launch
    {
        get 
        {
            return _launch;
        }

        set 
        {
            bool former = _launch;
            _launch = value;
            if (_launch && (_useMultilaunch || former == false))
            {
                ParamTwingAsync();
                _launch = false;
            }
        }
    }

    public float StartValue
    {
        get { return _start; }
        set { _start = value; }
    }

    public float EndValue
    {
        get { return _end; }
        set { _end = value; }
    }

    public float DurationValue
    {
        get { return _duration; }
        set { _duration = value; }
    }


    void Reset()
    {
        _start = 0f;
        _end = 1f;
        _duration = 3f;
        Launch = false; // use property
    }

    void Start()
    {
        block = new MaterialPropertyBlock();
        block.SetFloat(_nameParam, _start);
        _rendererMesh.SetPropertyBlock(block);

        Launch = _launch;
    }

    [ContextMenu("LaunchParam")]
    async void ParamTwingAsync()
    {
        
        float startTime = Time.time;
        block.SetFloat(_nameParam, _start);
        OnStart?.Invoke();

        while (Time.time < startTime + _duration)
        {
            float value = Mathf.Lerp(_start, _end, (Time.time - _start) / _duration);
            block.SetFloat(_nameParam, value);
            OnUpdate?.Invoke();

            await Awaitable.NextFrameAsync();
        }

        block.SetFloat(_nameParam, _end);
        OnFinish?.Invoke();
    }

    void ReLaunch()
    {
        _launch = true;
    }

}
