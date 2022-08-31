using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LevelLoader : MonoBehaviour
{
    public Animator transitionBreach;
    public float transitionTime = 1f;
   

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound("FondoMuestra");

    }
    // Update is called once per frame
    void Update()
    {
        
        
       

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {

            FindObjectOfType<AudioManager>().PlaySound("PortalAbierto");


            transitionBreach.SetTrigger("Open");


        }
        
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            FindObjectOfType<AudioManager>().PlaySound("PortalAbierto");
            

            transitionBreach.SetTrigger("Close");


        }


       
          


    }

   
    
}
