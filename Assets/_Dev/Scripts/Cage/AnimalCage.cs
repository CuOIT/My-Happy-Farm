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
        private bool isHungry;

        [SerializeField] GameObject countDownTime;
        [SerializeField] GameObject needToFeed;
        [SerializeField] TextMeshProUGUI timeTxt;

        //Data
        [SerializeField] DateTimeData lastTimeFeeed;

        [SerializeField] ProductNum productNum;
        [SerializeField] ProductNum foodRequire;
        [SerializeField] int timeToHungry;

        List<IAnimal> animals;
        [SerializeField] ProductNumEvent feedEvent;
        void Awake()
        {
           /* if(lastTimeFeed!=null)
            lastTime = DateTime.ParseExact(lastTimeFeed.Value, FORMAT,CultureInfo.InvariantCulture);*/
            //animalUI = animalUIGO.GetComponent<IAnimalUI>();
            animals=GetComponentsInChildren<IAnimal>().ToList();
        }
        void Update()
        {
            if (!isHungry)
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = now - lastTimeFeeed.Value;
                long time = (long)timeSpan.TotalSeconds;
                if (time >= timeToHungry)
                {
                    isHungry = true;
                    ShowUINeedToFeed();
                }
                else
                {
                    long timeToNextHungry = timeToHungry - time;
                    int minute = (int)timeToNextHungry/60;
                    int second = (int)(timeToNextHungry - 60*minute);
                    timeTxt.SetText(minute+":"+second);
                    ShowTime();
                }
            }
            
        }
        void ShowTime()
        {
            countDownTime.SetActive(true);
            needToFeed.SetActive(false);
        }
        void AnimalFeed()
        {
            foreach(var animals in animals)
            {
                animals.OnFeed();
            }
        }
        void ShowUINeedToFeed()
        {
            needToFeed.SetActive(true);
            countDownTime.SetActive(false);
        }
        void AnimalShowHungry()
        {
            foreach(var animal in animals)
            {
                animal.ShowHungry();
            }
        }
        void AnimalShowGreet()
        {
            foreach (var animal in animals)
            {
                animal.Greet();
            }

        }


        [Button]
        public void Feed()
        {
            isHungry = false;
            lastTimeFeeed.Value = DateTime.Now;
            //Harvest();
            ShowTime();
            AnimalFeed();
            //farmer.Consume(foodRequire);
            feedEvent.RaiseEvent(foodRequire);  
        }

        public bool IsHungry()
        {
            return isHungry;
        }

        public ProductNum GetFoodType()
        {
            return foodRequire;
        }

        public ProductNum GetProductType()
        {
            return productNum;
        }

        public void OnHumanComing()
        {
            if (isHungry)
            {
                AnimalShowHungry();
            }
            else
            {
                AnimalShowGreet();
            }
        }
       
    }


    public interface ICage
    {     
        void Feed();
        bool IsHungry();
        ProductNum GetFoodType();
        ProductNum GetProductType();
        void OnHumanComing();
    }
}