
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Buybutton : MonoBehaviour
{
    [SerializeField] Button buybutton;
    [SerializeField] RawImage lockimage;
    [SerializeField] Button GOTOMAP;
    [SerializeField] TMP_Text priceText;

    private int carPrice;

    private void Start()
    {
        carPrice = SaveManger.carPrices[SaveManger.currentCar];
        priceText.text = $"{carPrice}$";

        
        buybutton.onClick.AddListener(OnBuyButtonClicked);
    }

    private void Update()
    {
        UpdateCarPrice();
        if (SaveManger.carsUnlocked[SaveManger.currentCar])
        {
            buybutton.gameObject.SetActive ( false);
            GOTOMAP.gameObject.SetActive(true);
            lockimage.gameObject.SetActive(false);
            SaveManger.Save();
        }
        else
        {
            buybutton.gameObject.SetActive ( true);
            GOTOMAP.gameObject.SetActive(false);
            buybutton.interactable = true;
            lockimage.gameObject.SetActive(true);
        }
    }

    public void OnBuyButtonClicked()
    {
        if (SaveManger.currency >= carPrice)
        {
            MoneyDisplay.AddMoney(-carPrice);
            SaveManger.carsUnlocked[SaveManger.currentCar] = true;
            SaveManger.Save();

            buybutton.interactable = false;
            lockimage.gameObject.SetActive(false);

            Debug.Log($"Bought car {SaveManger.currentCar} for {carPrice}$");
        }
        else
        {
            Debug.Log($"Not enough money to buy car {SaveManger.currentCar}");
        }
    }
    private void UpdateCarPrice()
    {
        carPrice = SaveManger.carPrices[SaveManger.currentCar];
        
        if(SaveManger.carsUnlocked[SaveManger.currentCar]){
            priceText.text = "already bought";
        }else{priceText.text = $"{carPrice}$";}
    }
}