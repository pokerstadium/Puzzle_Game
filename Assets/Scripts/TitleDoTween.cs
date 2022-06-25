using DG.Tweening;
using UnityEngine;
using System;
using System.Collections;

public class TitleDoTween : MonoBehaviour
{
    // Start is called before the first frame update
    private void Start()
    {

        if(gameObject.tag == "Fruit1")
        {
            Fruit1();
        }
        else
        {
            Fruit2();
        }
    }

    public void Fruit1()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(-0.5f, 1f).SetEase(Ease.OutSine)).SetRelative();

        seq.Append(transform.DOMoveY(1f, 2f).SetEase(Ease.InOutSine)).SetRelative();

        seq.Append(transform.DOMoveY(-0.5f, 1f).SetEase(Ease.InSine)).SetRelative();

        // 無限ループ
        seq.SetLoops(-1);
    }

    public void Fruit2()
    {
        var seq = DOTween.Sequence();

        seq.Append(transform.DOMoveY(0.5f, 1f).SetEase(Ease.OutSine)).SetRelative();

        seq.Append(transform.DOMoveY(-1f, 2f).SetEase(Ease.InOutSine)).SetRelative();

        seq.Append(transform.DOMoveY(0.5f, 1f).SetEase(Ease.InSine)).SetRelative();

        // 無限ループ
        seq.SetLoops(-1);
    }

    public void OnClick()
    {
        StartCoroutine(OnClickFruit());
    }

    IEnumerator OnClickFruit()
    {
        Debug.Log("クリック検知");
        transform.DOScale(new Vector3(3, 3, 3), 1f);
        yield return new WaitForSeconds(1f);
        transform.DOScale(new Vector3(1.5f, 1.5f, 1), 1f);
    }


    // Update is called once per frame
    private void Update()
    {
    }
}
