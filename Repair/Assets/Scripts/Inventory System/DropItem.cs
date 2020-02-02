using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour, ISaveable
{
    public AudioClip sound;
    public ItemType itemtype;

    Transform mainCam;
    void Start()
    {
        mainCam = Camera.main.transform;
    }

    void Update()
    {
        transform.parent.LookAt(mainCam);
    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (itemtype == ItemType.Coin)
                {
                    PlayerController.Coins++;
                    Destroy(transform.parent.gameObject);
                }
                else if (Inventory.AddItem(itemtype))
                {
                    Destroy(transform.parent.gameObject);
                    InventoryManager.UpdateUI();
                }
                else
                    return;

                PlayerController.audio.PlayOneShot(sound, 0.5f);
            }
        }
    }


    public object CaptureState()
    {
        return new DropItemInfo()
        {
            pos = (transform.position),
            type = (int)itemtype
        };
    }
    public void RestoreState(object state)
    {
            DropItemInfo item = (DropItemInfo)state;
            transform.position = item.pos;
        itemtype = (ItemType)item.type;
     }
}
