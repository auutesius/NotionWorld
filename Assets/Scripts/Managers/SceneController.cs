﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using  UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Start is called before the first frame update
    public void LoadMainScene(){
        SceneManager.LoadScene("TotalTestScene");
    }
    public void Exit(){
        Application.Quit();
    }
}
