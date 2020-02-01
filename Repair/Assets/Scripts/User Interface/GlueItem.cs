using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlueItem : MonoBehaviour
{
    public void UseGlue()
    {
        InventoryManager.UseGlue(transform.parent);
    }
}
