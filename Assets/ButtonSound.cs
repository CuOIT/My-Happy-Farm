using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonSound : MonoBehaviour
{
    [SerializeField] AudioClip audioClip;

    public void PlayBtnSound()
    {
        if (audioClip == null) return;
        AudioController.Instance.PlaySoundEffect(audioClip);
    }
}
