using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="Recipe",menuName ="Recipe")]
public class Recipe : ScriptableObject
{
    public string Name;
    public FarmProductType type;
    public List<ProductNum> products;
}
