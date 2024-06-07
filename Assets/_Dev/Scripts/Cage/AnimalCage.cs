using Assets._Dev.SO._CustomEvent;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;


namespace Cage
{

    public class AnimalCage : MonoBehaviour,ICage
    {
        [SerializeField] GameObject canvas;
        [SerializeField] GameObject countDownTime;
        [SerializeField] GameObject needToFeed;
        [SerializeField] StringData lastTimeFeed;
        [SerializeField] ProductNum productNum;
        [SerializeField] ProductNum foodRequire;
        [SerializeField] int timeToHungry;
        private DateTime lastTime;
        List<IAnimal> animals;

        [SerializeField] TextMeshProUGUI timeTxt;

        [SerializeField] GameObject animalUIGO;
        private IAnimalUI animalUI;
        private bool isHungry;

        private const string FORMAT = "yyyy/MM/dd HH:mm:ss";
        public void Awake()
        {
            if(lastTimeFeed!=null)
            lastTime = DateTime.ParseExact(lastTimeFeed.Value, FORMAT,CultureInfo.InvariantCulture);
            animalUI = animalUIGO.GetComponent<IAnimalUI>();
            animals=GetComponentsInChildren<IAnimal>().ToList();
        }

        public bool EnoughBarn()
        {
            return farmer.HaveProduct(foodRequire);
        }

        [Button]
        public void Feed()
        {
            isHungry = false;
            lastTime = DateTime.Now;
            lastTimeFeed.Value =lastTime.ToString(FORMAT);
            Harvest();
            ShowTime();
            AnimalFeed();
            farmer.Consume(foodRequire);
        }

        public void Update()
        {
            if (!isHungry)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = now - lastTime;
                int time = (int)timeSpan.TotalSeconds;
                if (time >= timeToHungry)
                {
                    isHungry = true;
                    ShowUINeedToFeed();
                }
                else
                {
                    int timeToNextHungry = timeToHungry - time;
                    int minute = (int)timeToNextHungry/60;
                    int second = timeToNextHungry - 60*minute;
                    timeTxt.SetText(minute+":"+second);
                    ShowTime();
                }
            }
            
        }

        public void ShowTime()
        {
            countDownTime.SetActive(true);
            needToFeed.SetActive(false);
        }

        public void ShowUINeedToFeed()
        {
            needToFeed.SetActive(true);
            countDownTime.SetActive(false);
        }

        private IHarvest farmer;

        [Button]
        public void Harvest()
        {
            GameObject product = GameManager.Instance.pooler.SpawnFromPool(this.productNum.type.ToString(), transform.position, Quaternion.identity);
            GameObject product2 = GameManager.Instance.pooler.SpawnFromPool(this.productNum.type.ToString(), transform.position, Quaternion.identity);
            farmer.Harvest(product, productNum);
            farmer.Harvest(product2, productNum);
        }

        public void AnimalShowHungry()
        {
            foreach(var animal in animals)
            {
                animal.ShowHungry();
            }
        }

        public void AnimalShowGreet()
        {
            foreach (var animal in animals)
            {
                animal.Greet();
            }

        }

        public void AnimalFeed()
        {
            foreach(var animals in animals)
            {
                animals.OnFeed();
            }
        }
        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (isHungry)
                {
                    farmer = other.GetComponent<IHarvest>();
                    animalUI.Show();
                    animalUI.Init(foodRequire.type, this);
                    AnimalShowHungry();
                }
                else
                {
                    AnimalShowGreet();
                }
            }
        }

        public void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                animalUI.Hide();
            }
        }
    }


    public interface ICage
    {     

        void Feed();

        bool EnoughBarn();
    }
}