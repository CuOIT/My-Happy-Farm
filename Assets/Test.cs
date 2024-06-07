using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    private void OnEnable()
    {
        Debug.LogWarning("ENABLE");
    }

    void OnDisable()
    {
        Debug.LogWarning("DISABLE");
    }
}
