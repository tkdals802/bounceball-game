using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneName = "TestScene";
    
    public void backToSelectMenu()
    {
        SceneManager.LoadScene("SelectStage"); //게임 끝나고 맵선택버튼누를시 다시 맵을 고르게함 
    }
    public void SelectTestStage()
    {
        SceneManager.LoadScene(sceneName);
    }
}
