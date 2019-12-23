using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class OptionMenu : MonoBehaviour
{
    public Toggle fullScreenTog, VsyncTog;
    public resolutionItems[] resItems;
    private int selectedRes;
    public Text resLabel;

    public AudioMixer Mixer;
    public Slider masterSli, musicSli, SFXSli;
    public Text masterLabel, musicLabel, SFXLabel;
    public AudioSource SFXLoop;
    // Start is called before the first frame update
    void Start()
    {
        fullScreenTog.isOn = Screen.fullScreen;
        if(QualitySettings.vSyncCount == 0)
        {
            VsyncTog.isOn = false;
        }
        else
        {
            VsyncTog.isOn = true;
        }

        bool foundRes = false;
        for (int i= 0;i<resItems.Length;i++)
        {
            if(Screen.width==resItems[selectedRes].horizontal && Screen.height==resItems[selectedRes].vertical)
            {
                foundRes = true;
                selectedRes = i;
                updateRes();
            }
        }
        if(!foundRes)
        {
            resLabel.text = Screen.width.ToString() + " X" + Screen.height.ToString();
        }

        if(PlayerPrefs.HasKey("MasterVol"))
        {
            Mixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));
            masterSli.value = PlayerPrefs.GetFloat("MasterVol");
        }
        if (PlayerPrefs.HasKey("MusicVol"))
        {
            Mixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));
            musicSli.value = PlayerPrefs.GetFloat("MusicVol");
        }
        if (PlayerPrefs.HasKey("SFXVol"))
        {
            Mixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));
            SFXSli.value = PlayerPrefs.GetFloat("SFXVol");
        }

        masterLabel.text = (masterSli.value + 80).ToString();
        musicLabel.text = (musicSli.value + 80).ToString();
        SFXLabel.text = (SFXSli.value + 80).ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void resLeft()
    {
        selectedRes--;
        if(selectedRes<0)
        {
            selectedRes = resItems.Length - 1;
        }
        updateRes();
    }

    public void resRight()
    {
        selectedRes++;
        if(selectedRes>resItems.Length-1)
        {
            selectedRes = 0;
        }
        updateRes();
    }

    public void updateRes()
    {
        resLabel.text = resItems[selectedRes].horizontal.ToString() + " X " + resItems[selectedRes].vertical.ToString();
    }
    public void applyChange()
    {
        //full screen
        //Screen.fullScreen = fullScreenTog.isOn;
        //vsync
        if(VsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        }
        else
        {
            QualitySettings.vSyncCount = 0;
        }
        //resolutions + fullscreen
        Screen.SetResolution(resItems[selectedRes].horizontal, resItems[selectedRes].vertical, fullScreenTog.isOn);
    }

    public void setMasterVol()
    {
        masterLabel.text = (masterSli.value + 80).ToString();
        Mixer.SetFloat("MasterVol", masterSli.value);
        PlayerPrefs.SetFloat("MasterVol", masterSli.value);
    }
    public void setMusicVol()
    {
        musicLabel.text = (musicSli.value + 80).ToString();
        Mixer.SetFloat("MusicVol", musicSli.value);
        PlayerPrefs.SetFloat("MusicVol", musicSli.value);
    }
    public void setSFXVol()
    {
        SFXLabel.text = (SFXSli.value + 80).ToString();
        Mixer.SetFloat("SFXVol", SFXSli.value);
        PlayerPrefs.SetFloat("SFXVol", SFXSli.value);
    }
    public void SFXStart()
    {
        SFXLoop.Play();
    }
    public void SFXStop()
    {
        SFXLoop.Stop();
    }
}

[System.Serializable]
public class resolutionItems
 {
    public int horizontal, vertical;
 }
