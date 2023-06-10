using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class StageMenu : MonoBehaviour
{
    // Start is called before the first frame update
    
    public void LoadNextScene()
    {
        Scene scene = SceneManager.GetActiveScene();
        int curScene = scene.buildIndex;
        int nextScene = curScene+1;
        SceneManager.LoadScene(nextScene);
    }
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
    public void Stage04()
    {
        SceneManager.LoadScene("Stage04");
    }
    public void Stage05()
    {
        SceneManager.LoadScene("Stage05");
    }
    public void Stage06()
    {
        SceneManager.LoadScene("Stage06");
    }
    public void Stage07()
    {
        SceneManager.LoadScene("Stage07");
    }
    public void Stage08()
    {
        SceneManager.LoadScene("Stage08");
    }
    public void Stage09()
    {
        SceneManager.LoadScene("Stage09");
    }
    public void Stage10()
    {
        SceneManager.LoadScene("Stage10");
    }
    public void Stage11()
    {
        SceneManager.LoadScene("Stage11");
    }
    public void Stage12()
    {
        SceneManager.LoadScene("Stage12");
    }
    public void Stage13()
    {
        SceneManager.LoadScene("Stage13");
    }
    public void Stage14()
    {
        SceneManager.LoadScene("Stage14");
    }
    public void Stage15()
    {
        SceneManager.LoadScene("Stage15");
    }
    public void Stage16()
    {
        SceneManager.LoadScene("Stage16");
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
