using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    static AudioManager current;
    public AudioMixer audioMixer;

    [Header("Music Audio")]
    public AudioClip menuMusicClip;             // The menu music 
    public AudioClip levelMusicClip;             // The level music 
    public AudioClip battleMusicClip;             // The transition music 

    [Header("Player Audio")]
    public AudioClip doubleJumpClip;                 // The level background music
    public AudioClip damageClip;                 // The level background music
    public AudioClip landClip;           // The level background music
    public AudioClip jumpClip;           // The level background music
    public AudioClip attackClip;           // The level background music
    public AudioClip footStepClip;           // The level background music

    [Header("Interactions Audio")]
    public AudioClip buttonClickClip;           // The sting played when a button is pressed
    public AudioClip typeClip;           // The sting played when a button is pressed
    public AudioClip jumpOrbClip;           // The sting played when a bridge is broken
    public AudioClip coinClip;          // The level background music
    public AudioClip hitClip;          // The level background music
    
    [Header("Mixer Groups")]
    public AudioMixerGroup musicGroup;          // The music mixer group
    public AudioMixerGroup playerGroup;         // The player mixer group
    public AudioMixerGroup interactionsGroup;   // The interactions mixer group

    AudioSource musicSource;        // Reference to the generated music Audio Source
    AudioSource playerSource;       // Reference to the generated player Audio Source
    AudioSource interactionsSource; // Reference to the generated interactions Audio Source

    void Awake()
    {
        // Check there's only one AudioManager active
        if (current != null && current != this)
        {
            Destroy(gameObject);
            return;
        }
        current = this;
        DontDestroyOnLoad(gameObject);

        // Generate the Audio Source "channels" for the game audio
        musicSource         = gameObject.AddComponent<AudioSource>() as AudioSource;
        playerSource        = gameObject.AddComponent<AudioSource>() as AudioSource;
        interactionsSource  = gameObject.AddComponent<AudioSource>() as AudioSource;

        // Assign each audio source to its respective mixer group so that it is routed and controlled by the audio mixer
        musicSource.outputAudioMixerGroup           = musicGroup;
        playerSource.outputAudioMixerGroup          = playerGroup;
        interactionsSource.outputAudioMixerGroup    = interactionsGroup;
    }

    // ---------------------- HELPER METHODS ----------------------

    private static void PlayAudioClip(AudioSource source, AudioClip clip, bool looping)
    {
        source.clip = clip;
        source.loop = looping;
        source.Play();
    }

    public static void StopMusicAudio()
    {
        current.musicSource.Stop();
    }

    public static void StopEffectsAudio()
    {
        current.playerSource.Stop();
        current.interactionsSource.Stop();
    }

    public static void StopPlayerAudio()
    {
        current.playerSource.Stop();
    }

    public static AudioSource GetMusicSource() {
        return current.musicSource;
    }
    
    // ---------------------- MUSIC SOURCE ----------------------

    public static void StartMenuAudio() {
        if (current == null)
            return;
        PlayAudioClip(current.musicSource, current.menuMusicClip, true);
    }

    public static void StartLevelAudio() {
        if (current == null)
            return;
        PlayAudioClip(current.musicSource, current.levelMusicClip, true);
    }

    public static void StartBattleAudio() {
        if (current == null)
            return;
        PlayAudioClip(current.musicSource, current.battleMusicClip, true);
    }

    // ---------------------- PLAYER SOURCE  ----------------------
    public static void PlayJumpAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.playerSource.clip = current.jumpClip;
        current.playerSource.Play();
    }

    public static void PlayAttackAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.playerSource.clip = current.attackClip;
        current.playerSource.Play();
    }

    public static void PlayDoubleJumpAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.playerSource.clip = current.doubleJumpClip;
        current.playerSource.Play();
    }

    public static void PlayLandAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.playerSource.clip = current.landClip;
        current.playerSource.Play();
    }

    public static void PlayFootstepsAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;
        if (current.playerSource.clip != current.footStepClip && current.playerSource.isPlaying) {
            return;
        }
        if (Time.timeScale == 0) { return; }

        current.playerSource.clip = current.footStepClip;
        current.playerSource.Play();
    }

    public static void PlayDamageAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.playerSource.clip = current.damageClip;
        current.playerSource.Play();
    }

    // ----------------------  INTERACTIONS SOURCE  ----------------------
    public static void PlayButtonClickAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.interactionsSource.clip = current.buttonClickClip;
        current.interactionsSource.Play();
    }

    public static void PlayTypeAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.interactionsSource.clip = current.typeClip;
        current.interactionsSource.pitch = UnityEngine.Random.Range(0.3f, 1f);
        current.interactionsSource.Play();
    }

    public static void PlayHitAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.interactionsSource.clip = current.hitClip;
        current.interactionsSource.Play();
    }

    public static void PlayCoinAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.interactionsSource.clip = current.coinClip;
        current.interactionsSource.Play();
    }

    public static void PlayJumpOrbAudio()
    {
        //If there is no current AudioManager, exit
        if (current == null)
            return;

        //Set the level reload sting clip and tell the source to play
        current.interactionsSource.clip = current.jumpOrbClip;
        current.interactionsSource.Play();
    }

}
