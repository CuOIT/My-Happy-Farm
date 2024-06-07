using _Template.Event;
using UnityEditor;
using UnityEngine;

namespace Assets._Dev.SO._CustomEvent
{
    [CreateAssetMenu(fileName="NewProductNumEvent",menuName ="Event/ProductNumEvent")]
    public class ProductNumEvent : BaseSOEvent<ProductNum>
    {

    }
#if UNITY_EDITOR
    [CustomEditor(typeof(ProductNumEvent), true)]
    public class ProductNumEventEditor : Editor
    {
        ProductNumEvent soEvent;

        public void OnEnable()
        {
            soEvent = target as ProductNumEvent;
        }
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            if (GUILayout.Button("RaiseEvent"))
            {
                soEvent.RaiseTestEvent();
            }
        }
    }


#endif

    [System.Serializable]
    public struct ProductInfo
    {
        public FarmProductType type;
        public int num;
        public Vector3 pos;

        public ProductInfo(FarmProductType type, Vector3 pos, int num)
        {
            this.type = type;
            this.pos = pos;
            this.num = num;
        }
    }
    [System.Serializable]
    public struct ProductNum
    {
        public FarmProductType type;
        public int num;
        public ProductNum(FarmProductType type,int num)
        {

            this.type = type;
            this.num = num;
        }
    }
}