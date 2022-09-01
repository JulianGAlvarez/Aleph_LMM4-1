using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Animator transitionFade;
    public float transitionTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
        
    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            transitionTime = 10f;
            StartCoroutine(StartFade());


        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transitionFade.SetTrigger("End");
          
        }

 
    }

    IEnumerator StartFade()
    {
        yield return new WaitForSeconds(transitionTime * Time.deltaTime);
        transitionFade.SetTrigger("Start");
    }
   
}
