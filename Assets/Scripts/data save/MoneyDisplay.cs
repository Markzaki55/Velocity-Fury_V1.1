// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using TMPro;

// public class MoneyDisplay : MonoBehaviour
// {
//     [SerializeField] TMP_Text moneyDisplay;
//     private int money;

//     void Start()
//     {
//         LoadMoney();
//     }

//     public void AddMoney(int amount)
//     {
//         money += amount;
//         if (money < 0)
//         {
//             money = 0;
//         }
//         SaveMoney();
        
//     }

//     public void LoadMoney()
//     {
//         SaveManger.Load();
//         money = SaveManger.currency;
//         moneyDisplay.text = $"{money}$";
//     }

//     private void SaveMoney()
//     {
//         SaveManger.currency = money;
//         SaveManger.Save();
//         moneyDisplay.text = $"{money}$";
//     }

//     void Update()
//     {
//         if (Input.GetKeyDown(KeyCode.E))
//         {
//             AddMoney(100);
//         }
//         else if (Input.GetKeyDown(KeyCode.Q))
//         {
//             AddMoney(-100);
//         }
//     }
// }

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyDisplay : MonoBehaviour
{
    [SerializeField] TMP_Text moneyDisplay;
    static int money;

    void Start()
    {
        LoadMoney();
    }

    public static void AddMoney(int amount)
    {
        money += amount;
        if (money < 0)
        {
            money = 0;
        }
        SaveMoney();
    }

    public void LoadMoney()
    {
        SaveManger.Load();
        money = SaveManger.currency;
        UpdateMoneyDisplay();
    }

    static void SaveMoney()
    {
        SaveManger.currency = money;
        SaveManger.Save();
 
    }

    private void OnDestroy()
    {
        SaveMoney();
    }

    private void UpdateMoneyDisplay()
    {
        moneyDisplay.text = $"{money}$";
    }

    void Update()
    {
               UpdateMoneyDisplay();
        if (Input.GetKeyDown(KeyCode.E))
        {
            AddMoney(100);
        }
        else if (Input.GetKeyDown(KeyCode.Q))
        {
            AddMoney(-100);
        }
    }
}