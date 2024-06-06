using UnityEngine;
using UnityEngine.UI;

public class Music : MonoBehaviour
{
    public Sprite onMusic;
    public Sprite offMusic;
    public GameObject BGMusic1;
    public GameObject[] objs11;


    public Image MusicButton;
    public bool isOn;
    public AudioSource ad;



    void Start()
    {
        isOn = true;
    }
    void Update()
    {
        if (PlayerPrefs.GetInt("music") == 0)
        {
            MusicButton.GetComponent<Image>().sprite = onMusic;
            ad.enabled = true;
            isOn = true;
        }
        else if (PlayerPrefs.GetInt("music") == 1)
        {
            MusicButton.GetComponent<Image>().sprite = offMusic;
            ad.enabled = false;
            isOn = false;
        }
    }

    public void offSaund()
    {
        if (!isOn)
        {
            PlayerPrefs.SetInt("music", 0);

        }
        else if (isOn)
        {
            PlayerPrefs.SetInt("music", 1);

        }
    }
}
