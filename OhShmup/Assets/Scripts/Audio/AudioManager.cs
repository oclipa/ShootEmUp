using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static bool initialized = false;
    static AudioSource audioSource;
    static Dictionary<AudioClipName, AudioClip> audioClips =
        new Dictionary<AudioClipName, AudioClip>();

    /// <summary>
    /// Gets a value indicating whether this <see cref="T:AudioManager"/> is initialized.
    /// </summary>
    /// <value><c>true</c> if initialized; otherwise, <c>false</c>.</value>
    public static bool Initialized
    {
        get { return initialized; }
    }

    /// <summary>
    /// Initialize the <see cref="T:AudioManager"/> using the specified source.
    /// </summary>
    /// <param name="source">Source.</param>
    public static void Initialize(AudioSource source)
    {
        initialized = true;
        audioSource = source;
        audioClips.Add(AudioClipName.Baa, Resources.Load<AudioClip>("fart"));
        audioClips.Add(AudioClipName.Woof, Resources.Load<AudioClip>("woof"));
        audioClips.Add(AudioClipName.GameOver, Resources.Load<AudioClip>("gameover"));
    }

    /// <summary>
    /// Plays the audio clip with the given name
    /// </summary>
    /// <param name="name">Name.</param>
    public static void Play(AudioClipName name)
    {
        audioSource.PlayOneShot(audioClips[name]);
    }
}
