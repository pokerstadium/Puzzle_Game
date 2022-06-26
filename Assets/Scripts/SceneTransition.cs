using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
   　public void OnNextStage(string loadScene)
    {
        //PlayerPrefs.SetInt("SCORE", 0);
        ////PlayerPrefsをセーブする
        //PlayerPrefs.Save();
        SceneManager.LoadScene(loadScene);
    }

}
