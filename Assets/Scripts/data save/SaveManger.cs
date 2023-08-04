using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class SaveManger 
{
    public static int currency;
    public static int[] carPrices;
    public static bool[] carsUnlocked;
    public static bool[] mapsUnlocked;
    public static int currentCar;
    public static int currentMap;
    public static int highScore;

    private static readonly string SAVE_FILE_NAME = "playerInfo.dat";

    public static void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/" + SAVE_FILE_NAME))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + SAVE_FILE_NAME, FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);

            currency = data.currency;
            carPrices = data.carPrices;
            carsUnlocked = data.carsUnlocked;
            mapsUnlocked = data.mapsUnlocked;
            currentCar = data.currentCar;
            currentMap = data.currentMap;
            highScore = data.highScore;

            file.Close();
        }
        else
        {     
            currency = 0;
            carPrices = new int[] { 0, 100, 200 ,500};
            carsUnlocked = new bool[] { true, false, false ,false};
            currentCar = 0;
           // mapsUnlocked = new bool[] { true, false, false };
         //   currentMap = 0;
       //     highScore = 0;

            Save();
        }
    }

    public static void Save()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/" + SAVE_FILE_NAME);
        PlayerData data = new PlayerData();

        data.currency = currency;
        data.carPrices = carPrices;
        data.carsUnlocked = carsUnlocked;
        data.currentCar = currentCar;
        // carPrices = new int[] { 0, 100, 200 ,500};
        //     carsUnlocked = new bool[] { true, false, false ,false};
        
        //  data.mapsUnlocked = mapsUnlocked;
        //data.currentMap = currentMap;
        //data.highScore = highScore;

        bf.Serialize(file, data);
        file.Close();
    }
}

[System.Serializable]
class PlayerData
{
    public int currency;
    public int[] carPrices;
    public bool[] carsUnlocked;
    public bool[] mapsUnlocked;
    public int currentCar;
    public int currentMap;
    public int highScore;
}