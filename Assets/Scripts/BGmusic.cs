using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGmusic : MonoBehaviour
{
    public static BGmusic instance;
    private void Awake()
    {

        if (instance != null)
            Destroy(gameObject);
        else
        {
           instance = this;
           DontDestroyOnLoad(this.gameObject);
        }
        
    }
    // Start is called before the first frame update
    //void Start()
    //{
    //    musicSource.clip = background;
    //    musicSource.Play();
    //}

    
}
