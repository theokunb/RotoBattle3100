using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class Explosion : MonoBehaviour
{
    private ParticleSystem _particleSystem;
    private float _playTime;
    private float _elapsedTime;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
        _playTime = _particleSystem.main.startLifetime.constantMax;
        _elapsedTime = 0;
    }

    private void Update()
    {
        _elapsedTime += Time.deltaTime;

        if(_elapsedTime >= _playTime)
        {
            Destroy(gameObject);
        }
    }
}
