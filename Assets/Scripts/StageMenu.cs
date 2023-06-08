using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    
    
    public void backToSelectMenu()
    {
        SceneManager.LoadScene("SelectStage"); //게임 끝나고 맵선택버튼누를시 다시 맵을 고르게함 
    }

    public void Stage01()
    {
        SceneManager.LoadScene("Stage01");
    }
    public void Stage02()
    {
        SceneManager.LoadScene("Stage02");
    }
    public void Stage03()
    {
        SceneManager.LoadScene("Stage03");
    }
    public void TestStage1()
    {
        SceneManager.LoadScene("TestScene");
    }
    public void TestStage2()
    {
        SceneManager.LoadScene("TestStage");
    }
}
