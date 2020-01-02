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
    public Transform point;
    float speed_v,accelration_v,handling_v,nitro_v;
    public Image speed_i,acceleration_i,handling_i,nitro_i;
    public TextMeshProUGUI  buytext,buytext2;
    public TextMeshProUGUI costs, costs2,cost3,cost4;
    int cost, selectedcharacter;
    bool sold;
    GameObject spawnned;
    float health, fuel,speed;

    public void Start()
    {   
        selectedcharacter = PlayerPrefs.GetInt("Skin",0);
        onoff();
    }
    private void Update()
    {
        money = PlayerPrefs.GetInt("gold", 0);
        checkcost();
        cost3.text = PlayerPrefs.GetInt("gold", 0).ToString();
        cost4.text = PlayerPrefs.GetInt("gold", 0).ToString();
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
    public void character3()
    {
        if (PlayerPrefs.GetInt("Sold3", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    public void character4()
    {
        if (PlayerPrefs.GetInt("Sold4", 0) < 1)
        {
            sold = false;
        }
        else
        {
            sold = true;
        }
    }
    public void character5()
    {
        if (PlayerPrefs.GetInt("Sold5", 0) < 1)
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
            case 3:
                character3();
                break;
            case 4:
                character4();
                break;
            case 5:
                character5();
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
            vehcial.SetActive(false);
            menu.SetActive(true);
            PlayerPrefs.SetFloat("fuel", fuel);
            PlayerPrefs.SetFloat("health", health);
            PlayerPrefs.SetFloat("highestspeed", speed);
            
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
            case 3:
                PlayerPrefs.SetInt("Sold3", 2);
                break;
            case 4:
                PlayerPrefs.SetInt("Sold4", 2);
                break;
            case 5:
                PlayerPrefs.SetInt("Sold5", 2);
                break;
        }
        PlayerPrefs.SetInt("Skin", selectedcharacter);
        PlayerPrefs.SetFloat("speed", speed_v);
        PlayerPrefs.SetFloat("acceleration", accelration_v);
        PlayerPrefs.SetFloat("handling", handling_v);
        PlayerPrefs.SetFloat("nitro", nitro_v);
        PlayerPrefs.SetFloat("fuel", fuel);
        PlayerPrefs.SetFloat("health", health);
        PlayerPrefs.SetFloat("highestspeed", speed);
    }
    public void left()
    {
        selectedcharacter -= 1;
        if (selectedcharacter<0)
        {
            selectedcharacter = 5;
        }
        Destroy(spawnned);
        onoff();
    }
    public void right()
    {
        selectedcharacter += 1;
        if (selectedcharacter > 5)
        {
            selectedcharacter = 0;
        }
        Destroy(spawnned);
        onoff();
    }
    public void onoff()
    {
        spawnned= Instantiate(cars[selectedcharacter], point.position, point.rotation);
        money_needed.SetActive(false);
    }
    void checkcost()
    {
        switch(selectedcharacter)
        {
            case 0:
                buytext.text = "SELECT";
                buytext2.text = "SELECT";
                costs.text = "";
                costs2.text = "";
                speed_v = .6f;
                handling_v = .5f;
                accelration_v = .5f;
                nitro_v = .4f;
                fuel = 200;
                health = 50;
                speed = 20;
                break;
            case 1:
                if (PlayerPrefs.GetInt("Sold1", 0) > 1)
                {
                    costs.text = "";
                    costs2.text = "";
                    buytext.text = "SELECT";
                    buytext2.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 500";
                    costs2.text = "cost : 500";
                    buytext.text = "buy";
                    buytext2.text = "buy";
                }
                cost = 500;
                speed_v = .7f;
                handling_v = .5f;
                accelration_v = .6f;
                nitro_v = .6f;
                fuel = 200;
                health = 50;
                speed = 30;
                break;
            case 2:

                if (PlayerPrefs.GetInt("Sold2", 0) > 1)
                {
                    costs.text = "";
                    costs2.text = "";
                    buytext.text = "SELECT";
                    buytext2.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 1000";
                    costs2.text = "cost : 1000";
                    buytext.text = "buy";
                    buytext2.text = "buy";
                }
                cost = 1000;
                speed_v = .6f;
                handling_v = .8f;
                accelration_v = .5f;
                nitro_v = .8f;
                fuel = 250;
                health = 70;
                speed = 25;
                break;
            case 3:

                if (PlayerPrefs.GetInt("Sold3", 0) > 1)
                {
                    costs.text = "";
                    costs2.text = "";
                    buytext.text = "SELECT";
                    buytext2.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 1500";
                    costs2.text = "cost : 1500";
                    buytext.text = "buy";
                    buytext2.text = "buy";
                }
                cost = 1500;
                speed_v = .7f;
                handling_v = .7f;
                accelration_v = .8f;
                nitro_v = .7f;
                fuel = 250;
                health = 80;
                speed = 30;
                break;
            case 4:

                if (PlayerPrefs.GetInt("Sold4", 0) > 1)
                {
                    costs.text = "";
                    costs2.text = "";
                    buytext.text = "SELECT";
                    buytext2.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 1500";
                    costs2.text = "cost : 1500";
                    buytext.text = "buy";
                    buytext2.text = "buy";
                }
                cost = 1500;
                speed_v = .9f;
                handling_v = .8f;
                accelration_v = .9f;
                nitro_v = .8f;
                fuel = 300;
                health = 100;
                speed = 35;
                break;

            case 5:

                if (PlayerPrefs.GetInt("Sold5", 0) > 1)
                {
                    costs.text = "";
                    costs2.text = "";
                    buytext.text = "SELECT";
                    buytext2.text = "SELECT";
                }
                else
                {
                    costs.text = "cost : 2500";
                    costs2.text = "cost : 2500";
                    buytext.text = "buy";
                    buytext2.text = "buy";
                }
                cost = 2500;
                speed_v = .8f;
                handling_v = .9f;
                accelration_v = .9f;
                nitro_v = .9f;
                fuel = 400;
                health = 120;
                speed = 35;
                break;
        }
        speed_i.fillAmount = speed_v;
        acceleration_i.fillAmount = accelration_v;
        handling_i.fillAmount = handling_v;
        nitro_i.fillAmount = nitro_v;
    }

}
