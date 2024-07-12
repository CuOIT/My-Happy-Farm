using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableSound : MonoBehaviour
{
    [SerializeField] AudioClip clip;
    // Start is called before the first frame update
    private void OnEnable()
    {
        AudioController.Instance.PlaySoundEffect(clip);
    }
}
