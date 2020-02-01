using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour, IInitable
{
    public static ShopManager Instance;
    public List<Button> Buttons;
    public int[] Prices;
    public int[] Stocks;

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
}
