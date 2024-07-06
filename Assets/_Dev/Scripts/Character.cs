using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] GameObject maleChar;
    [SerializeField] GameObject femaleChar;
    [SerializeField] IntData charData;


        private void OnEnable()
    {
        bool fe = (charData.Value == 0);
        femaleChar.SetActive(fe);
        maleChar.SetActive(!fe);

    }
}
