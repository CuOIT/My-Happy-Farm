using _Template.Event;
using Assets._Dev.SO._CustomEvent;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CookingZone : BaseContainer
{
    [SerializeField] List<Recipe> recipes;

    private Dictionary<ItemBoxUI, Recipe> boxToRecipes;

    [SerializeField] TextMeshProUGUI t_name;

    [SerializeField] MaterialController materialController;
    [SerializeField] SimpleEvent OnClickCookEvent;
    [SerializeField] ProductNumEvent CookEvent;

    [SerializeField] Button cookBtn;
    
    public override void GetAllProduct()
    {
        mapProductInfo = new();
        boxToRecipes = new();
        var itemBoxs = GetComponentsInChildren<ItemBoxUI>();
        int index = 0;
        foreach(var recipe in recipes)
        {
            ProductInfo productInfo = productInfos.GetProductInfoOfType(recipe.type);
            ItemBoxUI itemBoxUI = itemBoxs[index];
            itemBoxUI.AddListener(() => SetCurrentItem(itemBoxUI));
            index++;
            itemBoxUI.Init(productInfo.sprite);
            boxToRecipes.Add(itemBoxUI, recipe);
        }
    }
    public override void SetCurrentItem(ItemBoxUI item)
    {
        base.SetCurrentItem(item);
        if (item == null) return;
        Recipe recipe = boxToRecipes[item];
        t_name.SetText(recipe.Name.ToString());
        materialController.InitMaterials(recipe.products);
        OnClickCookEvent.RaiseEvent();
        if (CheckEnoughMaterial(recipe))
        {
            cookBtn.GetComponent<Image>().color= Color.white;
            cookBtn.enabled=true; 
        }
        else
        {
            cookBtn.GetComponent<Image>().color = Color.gray;
            cookBtn.enabled=false;
        }

    }

    public bool CheckEnoughMaterial(Recipe recipe)
    {
        foreach(var productNum in recipe.products)
        {
            if(productNum.num> productData.Value[productNum.type])
            {
                GameManager.Instance.Notice("Not Enough " + productNum.type);
                return false;
            }
        }
        return true;
    }
    public void Cook()
    {
        Recipe recipe = boxToRecipes[currentItem];
        ProductNum productNumb = new ProductNum(recipe.type,1);
        
        foreach(var productNum in recipe.products)
        {
            productData.Consume(productNum);
        }
        productData.Add(productNumb);
        CookEvent.RaiseEvent(productNumb);
        SetCurrentItem(currentItem);
    }
}
