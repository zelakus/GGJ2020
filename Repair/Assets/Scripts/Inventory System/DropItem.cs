using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour,ISaveable
{

    public ItemType itemtype;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
