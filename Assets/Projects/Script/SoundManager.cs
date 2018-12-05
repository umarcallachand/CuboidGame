using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public Sound[] Sounds;
    private GameObject _player;
    public static SoundManager Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
            return;
        }


        AddSoundPlayer();
    }

    private void AddSoundPlayer()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        foreach (var s in Sounds)
        {
            s.Source = _player.AddComponent<AudioSource>();
            s.Source.clip = s.Clip;
            s.Source.volume = s.Volume;
            s.Source.pitch = s.Pitch;
            s.Source.playOnAwake = false;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(Sounds, sound => sound.Name == name);
        if (s == null) return;
        if (_player == null)
        {
            AddSoundPlayer();
            Debug.Log(name);
        }
        s.Source.Play();
        s.Source.spatialBlend = 1;
    }
}
