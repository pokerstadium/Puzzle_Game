using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectStageManager : MonoBehaviour
{
    [SerializeField] int stageNum = 1; // スコア変数
    [SerializeField] GameObject stage2;
    [SerializeField] GameObject stage3;
    // Start is called before the first frame update
    void Start()
    {
        stageNum = PlayerPrefs.GetInt("SCORE", 0); // 現在のstageNumを呼び出す
    }

    // Update is called once per frame
    void Update()
    {
        if(stageNum >= 2)
        {
            stage2.SetActive(true);
        }

        if(stageNum >= 3)
        {
            stage2.SetActive(true);
        }
    }
}
