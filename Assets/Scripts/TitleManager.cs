using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject descriptionPage1;
    [SerializeField] private GameObject descriptionPage2;
    [SerializeField] private GameObject descriptionPage3;

    public void OnDescriptionPage1()
    {
        descriptionPage1.SetActive(true);
    }

    public void OnDescriptionPage2()
    {
        descriptionPage2.SetActive(true);
        descriptionPage1.SetActive(false);
    }
    public void OnDescriptionPage3()
    {
        descriptionPage3.SetActive(true);
        descriptionPage2.SetActive(false);
    }

    public void OnTitle()
    {
        descriptionPage1.SetActive(false);
        descriptionPage2.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
    }
}
