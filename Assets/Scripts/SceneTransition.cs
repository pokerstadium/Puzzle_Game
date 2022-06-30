using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    public AudioClip audioClip;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    public void OnNextStage(string loadScene)
    {
        audioSource.PlayOneShot(audioClip);
        SceneManager.LoadScene(loadScene);
    }

}
