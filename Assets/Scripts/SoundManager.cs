using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SoundManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] Slider VolumeSlider;
    public static SoundManager instance;


    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip _playerDamageSFX, _fireBallSFX, _iceSpellSFX, _windSpellSFX, _magicShotSFX, _coinPickUpSFX, _itemPickUpSFX, _enemyDamageSFX;

    // Start is called before the first frame update
    private void Start()
    {
        if(instance == null){
            instance = this;
        }else{
            Destroy(gameObject);
        }
        
    }
     private List<Slider> FindSliders(){
        IEnumerable<Slider> slidersInScene = FindObjectsOfType<MonoBehaviour>().OfType<Slider>();
        
        return new List<Slider>(slidersInScene);
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

    public void PlayPlayerDamageSFX()
    {
        _source.clip = _playerDamageSFX;
        _source.Play();
    }

    public void PlayFireBallSFX()
    {
        _source.clip = _fireBallSFX;
        _source.Play();
    }

    public void PlayIceSpellSFX()
    {
        _source.clip = _iceSpellSFX;
        _source.Play();
    }

    public void PlayWindSpellSFX()
    {
        _source.clip = _windSpellSFX;
        _source.Play();
    }

    public void PlayMagicShotSFX()
    {
        _source.clip = _magicShotSFX;
        _source.Play();
    }

    public void PlayCoinPickUpSFX()
    {
        _source.clip = _coinPickUpSFX;
        _source.Play();
    }

    public void PlayItemPickUpSFX()
    {
        _source.clip = _itemPickUpSFX;
        _source.Play();
    }

    public void PlayEnemyDamageSFX()
    {
        _source.clip = _enemyDamageSFX;
        _source.Play();
    }

}
