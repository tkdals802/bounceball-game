using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName = "TestScene";
     public void SelectTestStage()
    {
        SceneManager.LoadScene(sceneName);
    }
}
