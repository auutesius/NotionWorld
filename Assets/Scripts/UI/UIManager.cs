using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject mainCanvas;
    public GameObject joyStick;
    public GameObject die;
    public GameObject success;
    public void DisplayDiedUI(){
        mainCanvas.SetActive(false);
        joyStick.SetActive(false);
        die.SetActive(true);
        success.SetActive(false);
    }
    public void DisplaySuccessfulUI(){
        mainCanvas.SetActive(false);
        joyStick.SetActive(false);
        die.SetActive(false);
        success.SetActive(true);
    }
}
