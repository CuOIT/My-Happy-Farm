using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(GridLayoutGroup))]
public class GridSupport : MonoBehaviour
{
    [SerializeField] bool horizontal;
    [SerializeField] bool vertical;
 GridLayoutGroup grid;
 RectTransform rect;
    public void OnEnable()
    {
        rect = GetComponent<RectTransform>();
        grid = GetComponent<GridLayoutGroup>();
        if(horizontal)
        {
            rect.sizeDelta = new(grid.cellSize.x * transform.childCount + (grid.spacing.x) * (transform.childCount - 1),rect.sizeDelta.y);
        }else if (vertical)
        {
            rect.sizeDelta = new(rect.sizeDelta.x, grid.cellSize.y * transform.childCount + (grid.spacing.y) * (transform.childCount - 1) );
        }
    }
}
