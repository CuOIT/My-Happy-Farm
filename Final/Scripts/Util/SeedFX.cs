using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedFX : MonoBehaviour
{
    [SerializeField] ParticleSystem part;


    public void PlaySeedFx()
    {
        part.Play();
    }
}
