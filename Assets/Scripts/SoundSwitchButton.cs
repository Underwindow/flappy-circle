using UnityEngine;
using UnityEngine.UI;

public class SoundSwitchButton : MonoBehaviour
{
    [SerializeField] private Image soundOn, soundOff;

    private bool isSoundOn;
    private Button button;
    public delegate void SoundSwitchEvent(bool isSoundOn);
    public event SoundSwitchEvent OnSoundSwitch;

    private void Start()
    {
        button = GetComponent<Button>();

        SoundSetActive(
            isSoundOn = PlayerPrefs.GetInt("IS_SOUND_ON", 1) == 1
        );

        button.targetGraphic = isSoundOn ? soundOn : soundOff;
    }

    public void Switch()
    {
        isSoundOn = !isSoundOn;
        
        PlayerPrefs.SetInt("IS_SOUND_ON", isSoundOn ? 1 : 0);

        SoundSetActive(isSoundOn);

        OnSoundSwitch?.Invoke(isSoundOn);
    }

    private void SoundSetActive(bool isSoundOn)
    {
        soundOn.gameObject.SetActive(isSoundOn);
        soundOff.gameObject.SetActive(!isSoundOn);

        button.targetGraphic = isSoundOn ? soundOn : soundOff;
    }
}
