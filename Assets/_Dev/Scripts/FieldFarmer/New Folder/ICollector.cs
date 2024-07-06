using Assets._Dev.SO._CustomEvent;
using System.Collections;
using UnityEngine;

namespace Assets._Dev.Scripts
{
    public interface ICollector {

        public void InitProductNum();

        public void Collect(ProductNum productNum);  

        public void Consume(ProductNum productNum);
        public bool HaveProduct(ProductNum productNum);
    }
}