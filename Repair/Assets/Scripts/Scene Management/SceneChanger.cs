using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // Start is called before the first frame update
   public void NewGame()
    {
        SavingWrapper wrapper = new SavingWrapper();
        wrapper.Delete();
        SceneManager.LoadScene(1);
    }

    
}
