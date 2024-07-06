using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCan : MonoBehaviour
{
    // Start is called before the first frame update
    ParticleSystem _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystem>();
    }
    void OnEnable()
    {
        _particleSystem.Play();
    }

    // Update is called once per frame
    private void OnDisable()
    {
        _particleSystem.Stop();
    }
}
