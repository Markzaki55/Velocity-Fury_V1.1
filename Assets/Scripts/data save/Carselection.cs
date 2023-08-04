using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Carselection : MonoBehaviour
{
    [SerializeField] Button left;
    [SerializeField] Button right;
    private int currentcar;
    [SerializeField] Transform CarHolder;
    private void Awake()
    {

        SaveManger.Load();
        currentcar = SaveManger.currentCar;
        selectCar(currentcar);
        DisableCam();
        DisableAllCarScripts();
    }

    private void selectCar(int index)
    {
        left.interactable = (index != 0);
        right.interactable = (index != CarHolder.childCount - 1);
        for (int i = 0; i < CarHolder.childCount; i++)
        {
            CarHolder.GetChild(i).gameObject.SetActive(i == index);
        }
    }

    public void Changecar(int Change)
    {
        currentcar += Change;
        SaveManger.currentCar = currentcar;
        SaveManger.Save();
        selectCar(currentcar);
        DisableCam();
    }


    private void DisableAllCarScripts()
    {
        foreach (Transform car in CarHolder)
        {
            MonoBehaviour[] scripts = car.GetComponentsInChildren<MonoBehaviour>();
            foreach (MonoBehaviour script in scripts)
            {
                script.enabled = false;
            }
        }
    }
    void DisableCam()
    {
        Camera[] cameras = Camera.allCameras;
        for (int i = 0; i < cameras.Length; i++)
        {
            if (cameras[i].gameObject.tag != "CarSelectCam")
            {
                cameras[i].transform.parent.gameObject.SetActive(false);
            }
        }
    }
}
