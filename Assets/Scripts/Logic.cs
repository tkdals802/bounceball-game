using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Logic : MonoBehaviour
{
    private int maxStar;
    private int CurrentStarCount;
    public GameObject GameOverScreen;
    private GameObject[] stars;
    // Start is called before the first frame update
    
    void Awake()
    {
        Time.timeScale=1f;
    }
    void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");
        maxStar = stars.Length;
        CurrentStarCount = maxStar;
    }

    public void GetStar() //별을 먹으면 현재 맵의 별의갯수를 줄여줌
    {
        CurrentStarCount-=1;
    }
    // Update is called once per frame
    private void GameClear() //별을 다먹는경우
    {
        if(CurrentStarCount==0)
        {
            GameOverScreen.SetActive(true); //메뉴버튼 활성화
            Time.timeScale=0f; //시간을 멈춰서 일시정지상태로만듬
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //씬이 로드되면 정상시간에 작동
    {
        Time.timeScale=1f;
    }   
    void Update()
    {
        
        Debug.Log("CurrentStarCount : "+CurrentStarCount);
        GameClear(); 
    }


}
