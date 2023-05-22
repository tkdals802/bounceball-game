using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StarController : MonoBehaviour
{
    [SerializeField]
    private Logic lg;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collsion)
    {
        if(collsion.gameObject.CompareTag("Player"))
        {
            lg.GetStar();
            Destroy(gameObject);
        }
    }
}
