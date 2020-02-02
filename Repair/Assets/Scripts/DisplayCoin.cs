using TMPro;
using UnityEngine;

public class DisplayCoin : MonoBehaviour
{

    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = PlayerController.Coins.ToString();
    }
}
