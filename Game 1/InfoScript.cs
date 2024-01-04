using UnityEngine;
using TMPro;
using Sound;

public class InfoScript : MonoBehaviour
{
    [SerializeField] private GameObject btnReport, btnRate, btnReturn;
    [SerializeField] private TextMeshProUGUI version;

    private SoundManager sound;

    // Start is called before the first frame update
    void Start()
    {
        sound = FindObjectOfType<SoundManager>();
        version.text = "v " + Application.version;
    }

    public void Rate()
    {
        Application.OpenURL("market://details?id=com.LyandaGames.Goblinsdungeon.cardsmerge");
        sound.playSound(sound.btnNiceUI);
    }

    public void Feedback()
    {
        Application.OpenURL("mailto:hello.lyanda@gmail.com?subject=FoundBug");
        sound.playSound(sound.btnNiceUI);
    }

    public void Return()
    {
        this.gameObject.SetActive(false);
        sound.playSound(sound.btnNiceUI);
    }
}
