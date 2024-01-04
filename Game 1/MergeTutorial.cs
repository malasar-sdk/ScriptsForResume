using UnityEngine;
using TMPro;
using Sound;

public class MergeTutorial : MonoBehaviour
{
    [SerializeField]
    private GameObject tuorialScreen;

    [SerializeField]
    private TextMeshProUGUI coinsPrice;

    // Start is called before the first frame update
    void Start()
    {
       // tuorialScreen.SetActive(false);
    }

    public void closeTutor()
    {
        tuorialScreen.SetActive(false);
    }
    public void openTutor()
    {
        SoundManager.sound.playSound(SoundManager.sound.btnSmallUI);
        tuorialScreen.SetActive(true);
    }

    public void setPrice(int price)
    {
        coinsPrice.text = price.ToString();
    }
}
