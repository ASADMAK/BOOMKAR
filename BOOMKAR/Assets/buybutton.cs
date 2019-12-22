using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class buybutton : MonoBehaviour {

    
    public enum Itemtype
    {
        Gold1000,
        Gold5000,
        Noads,
    }
    public Itemtype itemtype;

    public Text pricetext;

    private string defaultText;

    private void Start()
    {
        defaultText = pricetext.text;
        StartCoroutine(LoadPriceRoutine());
    }

    public void Clickbuy()
    {
        switch(itemtype)
        {
            case Itemtype.Gold1000:
                IAPmanager.Instance.BUY_1000();
                break;
            case Itemtype.Gold5000:
                IAPmanager.Instance.BUY_5000();
                break;
            case Itemtype.Noads:
                IAPmanager.Instance.NO_ADS();
                break;
        }
    }

    private IEnumerator LoadPriceRoutine()
    {
        while (!IAPmanager.Instance.IsInitialized())
            yield return null;
        string loadedprice = "";

        switch (itemtype)
        {
            case Itemtype.Gold1000:
                loadedprice=IAPmanager.Instance.GetProfuctPricefromstore(IAPmanager.Instance.Gold_1000);
                break;
            case Itemtype.Gold5000:
                loadedprice=IAPmanager.Instance.GetProfuctPricefromstore(IAPmanager.Instance.Gold_5000);
                break;
            case Itemtype.Noads:
                loadedprice=IAPmanager.Instance.GetProfuctPricefromstore(IAPmanager.Instance.NOADS);
                break;
        }

        pricetext.text = defaultText + "" + loadedprice;

    }
}
