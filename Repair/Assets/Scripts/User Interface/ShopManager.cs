using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//Needs a clean up
public class ShopManager : MonoBehaviour, IInitable
{
    public static ShopManager Instance;
    public List<Button> Buttons; //Should've been generated at runtime depending on given items. (That we didn't actually give...)
    public int[] Prices; //Prices should've been used from item asset infos
    public int[] Stocks; //Should've had such a variable in item asset info

    public void Init()
    {
        Instance = this;
    }

    private static string SavePath
    {
        get
        {
            return Directory.GetParent(Application.dataPath).FullName + "\\shop.dat";
        }
    }

    public static void Deserialize()
    {
        var json = File.ReadAllText(SavePath);
        Instance.Stocks = JsonUtility.FromJson<int[]>(json);
    }

    public static void Serialize()
    {
        File.WriteAllText(SavePath, JsonUtility.ToJson(Instance.Stocks, true));
    }

    public void UpdateUI()
    {
        InventoryManager.UpdateUI();
        for (int i=0;i<Buttons.Count;i++)
        {
            var text = $"{Prices[i]} " + (Prices[i] == 1 ? "Coin" : "Coins") + $" ({Stocks[i]}x)";
            Buttons[i].transform.GetChild(0).GetComponent<TMP_Text>().SetText(text);
            if (Stocks[i] == 0)
            {
                Buttons[i].enabled = false;
                Buttons[i].image.color = Color.red;
            }
            else
            {
                Buttons[i].enabled = true;
                Buttons[i].image.color = Color.white;
            }
        }
    }

    public void Buy1()
    {
        if (PlayerController.Coins >= Prices[0])
        {
            if (Inventory.AddItem(ItemType.Glue))
            {
                PlayerController.Coins -= Prices[0];
            Stocks[0]--;
            UpdateUI();
            }
            else
            {
                //TODO show inventory is full
            }
        }
        else
        {
            //TODO show not enough money
        }
    }

    public void Buy2()
    {
        if (PlayerController.Coins >= Prices[1])
        {
            if (Inventory.AddItem(ItemType.Flower))
            {
                PlayerController.Coins -= Prices[1];
                Stocks[1]--;
            UpdateUI();
            }
            else
            {
                //TODO show inventory is full
            }
        }
        else
        {
            //TODO show not enough money
        }
    }

    public void Buy3()
    {
        if (PlayerController.Coins >= Prices[2])
        {
            if (Inventory.AddItem(ItemType.Sword))
            {
                PlayerController.Coins -= Prices[2];
                Stocks[2]--;
            UpdateUI();
            }
            else
            {
                //TODO show inventory is full
            }
        }
        else
        {
            //TODO show not enough money
        }
    }

    public void Buy4()
    {
        if (PlayerController.Coins >= Prices[3])
        {
            if (Inventory.AddItem(ItemType.ColumnPiece))
            {
                PlayerController.Coins -= Prices[3];
                Stocks[3]--;
            UpdateUI();
            }
            else
            {
                //TODO show inventory is full
            }
        }
        else
        {
            //TODO show not enough money
        }
    }
}
