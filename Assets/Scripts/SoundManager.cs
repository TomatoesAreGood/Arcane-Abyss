using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
public class SoundManager : MonoBehaviour, IDataPersistance
{
    [SerializeField] Slider BGMVolumeSlider;
    [SerializeField] Slider SFXVolumeSlider;

    public static SoundManager instance;
    public List<AudioSource> SFXSources;
    [SerializeField] private AudioSource BGMAudioSource;
    [SerializeField] private AudioClip _playerDamageSFX, _fireBallSFX, _iceSpellSFX, _windSpellSFX, _magicShotSFX, _coinPickUpSFX, _itemPickUpSFX, _enemyDamageSFX, _sippingSFX, _buyItemSFX, _sellItemSFX;

    // Start is called before the first frame update
    private void Start()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }
    private AudioSource GetAvailAudioSource(){
        for(int i = 0 ; i < SFXSources.Count; i++){
            if(!SFXSources[i].isPlaying){
                return SFXSources[i];
            }
        }
        return SFXSources[0];
    }

    public void ChangeBGMVolume()
    {
        BGMAudioSource.volume = BGMVolumeSlider.value;
    }

    public void ChangeSFXVolume(){

        for(int i = 0; i < SFXSources.Count; i++){
            SFXSources[i].volume = SFXVolumeSlider.value;
        }
    }

    public void LoadData(GameData data)
    {
        BGMVolumeSlider.value = data.musicVolume;
        BGMAudioSource.volume = data.musicVolume;

        SFXVolumeSlider.value = data.SFXVolume;
        for(int i = 0; i < SFXSources.Count; i++){
            SFXSources[i].volume = data.SFXVolume;
        }
    }

    public void SaveData(ref GameData data)
    {
        data.musicVolume = BGMVolumeSlider.value;
        data.SFXVolume = SFXVolumeSlider.value;
    }

    public void PlayPlayerDamageSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _playerDamageSFX;
        audioSource.Play();
    }

    public void PlayFireBallSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _fireBallSFX;
        audioSource.Play();
    }

    public void PlayIceSpellSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _iceSpellSFX;
        audioSource.Play();
    }

    public void PlayWindSpellSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _windSpellSFX;
        audioSource.Play();
    }

    public void PlayMagicShotSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _magicShotSFX;
        audioSource.Play();
    }

    public void PlayCoinPickUpSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _coinPickUpSFX;
        audioSource.Play();
    }

    public void PlayItemPickUpSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _itemPickUpSFX;
        audioSource.Play();
    }

    public void PlayEnemyDamageSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _enemyDamageSFX;
        audioSource.Play();
    }

    public void PlaySippingSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _sippingSFX;
        audioSource.Play();
    }

    public void PlayBuyItemSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _buyItemSFX;
        audioSource.Play();
    }

    public void PlaySellItemSFX()
    {
        AudioSource audioSource = GetAvailAudioSource();
        audioSource.clip = _sellItemSFX;
        audioSource.Play();
    }

}
