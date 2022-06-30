using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public StageManager stageManager;
    int currentStage = 0;
    public GameObject clearPanel;
    public AudioClip audioClip;
    AudioSource audioSource;

    // ステージテキストとパネルの設置
    private void Start()
    {
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();
        audioSource = GetComponent<AudioSource>();

        // StageManagerにCleard関数を付与する
        stageManager.stageClear += Cleard;
    }

    // リセット
    public void OnResetButton()
    {
        stageManager.DestroyStage();
        stageManager.CreateStage();
    }

    // クリア処理
    public void Cleard()
    {
        StartCoroutine(ClearStage());
    }

    IEnumerator ClearStage()
    {
        audioSource.PlayOneShot(audioClip);
        clearPanel.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        currentStage++;

        // 最終ステージかどうか
        if (currentStage >= stageManager.stageFiles.Length)
        {
            SceneManager.LoadScene("Clear");
            yield return null;
        }
        stageManager.DestroyStage();
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();
        clearPanel.SetActive(false);
    }
}
