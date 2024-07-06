using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXSupport : MonoBehaviour
{
    [SerializeField] List<AudioClip> sfxs;

    public void PlaySound()
    {
        int num = Random.Range(0, sfxs.Count);
        AudioController.Instance.PlaySoundEffect(sfxs[num]);
    }
}
