using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingAnimation : MonoBehaviour
{
    public GameObject earthBlowupAnimation;
    public GameObject earth;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartShip() {
        GetComponent<Animator>().SetTrigger("go");
    }

    public void BlowupEarth() {
        earth.SetActive(false);
        earthBlowupAnimation.GetComponent<Animator>().SetTrigger("go");
    }
    
}
