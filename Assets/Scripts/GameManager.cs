using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public StageManager stageManager;
    int currentStage;

    // ステージテキストとパネルの設置
    private void Start()
    {
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();

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
        currentStage++;
        stageManager.DestroyStage();
        stageManager.LoadStageFromText(currentStage);
        stageManager.CreateStage();
    }

}
