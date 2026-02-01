using System;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public Sound[] sounds;
    public float fadeDuration;
    private void Awake()
    {
        ManageSingleton();
        foreach (Sound sound in sounds)
        {
            sound.source = gameObject.AddComponent<AudioSource>();
            sound.source.clip = sound.clip;
            sound.source.volume = sound.volume;
            sound.source.pitch = sound.pitch;
            sound.source.loop = sound.loop;
            sound.source.outputAudioMixerGroup = sound.audioMixerGroup;
        }
    }

    private void Start()
    {
        Play("HeavenMusic");
    }

    private void ManageSingleton()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null) return;
        s.source.Play();
    }
    
    public void PlaySFX(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
    
        if (s == null)
        {
            Debug.LogWarning("Ses efekti bulunamadı: " + name);
            return;
        }

        // PlayOneShot, seslerin birbirini kesmeden üst üste binmesini sağlar.
        // s.volume değerini parametre olarak göndererek ses seviyesini koruruz.
        s.source.PlayOneShot(s.clip, s.volume);
    }
    
    public void SwitchMusic(string offName, string onName)
    {
        Sound offSound = Array.Find(sounds, s => s.name == offName);
        Sound onSound = Array.Find(sounds, s => s.name == onName);

        if (offSound != null && onSound != null)
        {
            StartCoroutine(CrossFade(offSound, onSound, fadeDuration));
        }
    }

    private IEnumerator CrossFade(Sound off, Sound on, float duration)
    {
        // Yeni müziği başlat (eğer çalmıyorsa)
        if (!on.source.isPlaying) on.source.Play();
        
        float currentTime = 0;
        float startVolOff = off.source.volume;
        float targetVolOn = on.volume; // Sound classındaki orijinal volume değeri

        while (currentTime < duration)
        {
            currentTime += Time.deltaTime;
            // Birinin sesini kısarken diğerini aç
            off.source.volume = Mathf.Lerp(startVolOff, 0f, currentTime / duration);
            on.source.volume = Mathf.Lerp(0f, targetVolOn, currentTime / duration);
            yield return null;
        }

        off.source.Stop(); // Tamamen kısıldığında durdur
        off.source.volume = startVolOff; // Bir dahaki çalma için volume'u sıfırla
    }
}
