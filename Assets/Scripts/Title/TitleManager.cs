using UnityEngine;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject descriptionPage1;
    [SerializeField] private GameObject descriptionPage2;
    [SerializeField] private GameObject descriptionPage3;
    [SerializeField] private GameObject stageSelect;
    [SerializeField] private GameObject popup;
    [SerializeField] AudioClip audioClip;
    AudioSource audioSource;
    // Start is called before the first frame update
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnDescriptionPage1()
    {
        audioSource.PlayOneShot(audioClip);
        descriptionPage1.SetActive(true);
    }

    public void OnDescriptionPage2()
    {
        audioSource.PlayOneShot(audioClip);
        descriptionPage2.SetActive(true);
        descriptionPage1.SetActive(false);
    }
    public void OnDescriptionPage3()
    {
        audioSource.PlayOneShot(audioClip);
        descriptionPage3.SetActive(true);
        descriptionPage2.SetActive(false);
    }

    public void OnTitle()
    {
        audioSource.PlayOneShot(audioClip);
        descriptionPage1.SetActive(false);
        descriptionPage2.SetActive(false);
        stageSelect.SetActive(false);
    }

    public void OnStageSelect()
    {
        audioSource.PlayOneShot(audioClip);
        stageSelect.SetActive(true);
    }

    public void OnPophp()
    {
        popup.SetActive(true);
    }

    public void DeletePophp()
    {
        popup.SetActive(false);
    }


    // Update is called once per frame
    private void Update()
    {
    }
}
