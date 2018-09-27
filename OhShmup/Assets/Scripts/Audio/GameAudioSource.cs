﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAudioSource : MonoBehaviour
{

    /// <summary>
    /// Awake is called before Start
    /// </summary>
    void Awake()
    {
        // make sure we only have one of this game object in the game
        if (!AudioManager.Initialized)
        {
            // initialize audio manager and persist audio source across
            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioManager.Initialize(audioSource);
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
