using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
   　public void OnPanel(string loadScene)
    {
        SceneManager.LoadScene(loadScene);
    }

}
