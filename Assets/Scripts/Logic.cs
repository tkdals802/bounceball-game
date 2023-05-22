using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Logic : MonoBehaviour
{
    private int maxStar;
    private int CurrentStarCount;
    private GameObject[] stars;
    // Start is called before the first frame update
    
    void Awake()
    {
        
    }
    void Start()
    {
        stars = GameObject.FindGameObjectsWithTag("Star");
        maxStar = stars.Length;
        CurrentStarCount = maxStar;
    }

    public void GetStar()
    {
        CurrentStarCount-=1;
    }
    // Update is called once per frame
    void Update()
    {
        Debug.Log("CurrentStarCount : "+CurrentStarCount);
    }


}
