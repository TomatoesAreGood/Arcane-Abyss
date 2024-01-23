using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SoundManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] Slider VolumeSlider;
    public static SoundManager instance;
    
    // Start is called before the first frame update
    private void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        
    }

    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
    }

    public void LoadData(GameData data)
    {
        VolumeSlider.value = data.musicVolume;
        AudioListener.volume = data.musicVolume;
    }

    public void SaveData(ref GameData data)
    {
        data.musicVolume = VolumeSlider.value;
    }
}
