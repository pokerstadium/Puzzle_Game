using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Method : MonoBehaviour
{
    public GameObject method;

    public void OnMethod()
    {
        method.SetActive(true);
    }

    public void OnStart()
    {
        method.SetActive(false);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
