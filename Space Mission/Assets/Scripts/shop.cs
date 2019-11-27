using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class shop : MonoBehaviour
{
    
    int money;
    public GameObject money_needed,vehcial,menu;
    public GameObject[] cars;
    float speed_v,accelration_v,handling_v,nitro_v;
    public Image speed_i,acceleration_i,handling_i,nitro_i;
    public TextMeshProUGUI  buytext;
    public TextMeshProUGUI costs;
    int cost, selectedcharacter;
    bool sold;

    public void Start()
    {
    
        selectedcharacter = PlayerPrefs.GetInt("Skin", 0);
        onoff();

    }
    private void Update()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        Debug.Log(PlayerPrefs.GetInt("gold"));
        Debug.Log(PlayerPrefs.GetInt("Skin"));
        checkcost();

    }
    public void maincharacter()
    {
        sold = true;     
    }

    public void character1()
    {
        if (PlayerPrefs.GetInt("Sold1", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    public void character2()
    {
        if (PlayerPrefs.GetInt("Sold2", 0) < 1)
        {
            sold = false;            
        }
        else
        {
            sold = true;
        }
    }
    
    public void buybutton()
    {
        switch (selectedcharacter)
        {
            case 0:
                maincharacter();
                break;
            case 1:
                character1();
                break;
            case 2:
                character2();
                break;
        }
        if (sold == false)
        {
             if (money >= cost)
             {
                    money -= cost;
                    purchasing();
                    PlayerPrefs.SetInt("gold", money);
                    PlayerPrefs.SetInt("Skin", selectedcharacter);
              }
             else
            {
                money_needed.SetActive(true);
            }
               
        }
        else if (sold == true)
        {
            PlayerPrefs.SetInt("Skin", selectedcharacter);
            PlayerPrefs.SetFloat("speed", speed_v);
            PlayerPrefs.SetFloat("acceleration", accelration_v);
            PlayerPrefs.SetFloat("handling", handling_v);
            PlayerPrefs.SetFloat("nitro", nitro_v);
            PlayerPrefs.SetInt("Skin", selectedcharacter);
            vehcial.SetActive(false);
            menu.SetActive(true);
        }

    }
    void purchasing()
    {
        switch (selectedcharacter)
        {
            case 1:
                PlayerPrefs.SetInt("Sold1", 2);
                break;
            case 2:
                PlayerPrefs.SetInt("Sold2", 2);
                break;
        }
        PlayerPrefs.SetInt("Skin", selectedcharacter);
        PlayerPrefs.SetFloat("speed", speed_v);
        PlayerPrefs.SetFloat("acceleration", accelration_v);
        PlayerPrefs.SetFloat("handling", handling_v);
        PlayerPrefs.SetFloat("nitro", nitro_v);
    }
    public void left()
    {
        selectedcharacter -= 1;
        if (selectedcharacter<0)
        {
            selectedcharacter = 2;
        }
        onoff();
    }
    public void right()
    {
        selectedcharacter += 1;
        if (selectedcharacter > 2)
        {
            selectedcharacter = 0;
        }
        onoff();
    }
    public void onoff()
    {
        for (int i = 0; i < cars.Length; i++)
        {
            cars[i].SetActive(false);
        }
        cars[selectedcharacter].SetActive(true);
        money_needed.SetActive(false);
    }
    void checkcost()
    {
        switch(selectedcharacter)
        {
            case 0:
                buytext.text = "select";
                speed_v = .6f;
                handling_v = .5f;
                accelration_v = .5f;
                nitro_v = .4f;
                break;
            case 1:
                if (PlayerPrefs.GetInt("Sold1", 0) > 1)
                {
                    costs.text = "";
                    buytext.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 200";
                    buytext.text = "buy";
                }
                cost = 200;
                speed_v = .7f;
                handling_v = .5f;
                accelration_v = .6f;
                nitro_v = .6f;
                break;
            case 2:

                if (PlayerPrefs.GetInt("Sold2", 0) > 1)
                {
                    costs.text = "";
                    buytext.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 500";
                    buytext.text = "buy";
                }
                cost = 500;
                speed_v = .8f;
                handling_v = .5f;
                accelration_v = .5f;
                nitro_v = .8f;
                break;
        }
        speed_i.fillAmount = speed_v;
        acceleration_i.fillAmount = accelration_v;
        handling_i.fillAmount = handling_v;
        nitro_i.fillAmount = nitro_v;
    }

}
