using UnityEngine;
using System;
using UnityEditor.IMGUI.Controls;
using UnityEngine.Audio;

namespace Resources.Scripts
{
    public class AudioManager : MonoBehaviour
    {

        private static AudioManager instance;
    
        public Sound[] sounds;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
            {
                Destroy(gameObject);
                return;
            }
        
            DontDestroyOnLoad(gameObject);
        
            foreach (var sound in sounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
            }
        }

        public void Play(string soundName)
        {
            var s = Array.Find(sounds, sound => sound.name == soundName);
            if (s == null)
            {
                Debug.Log("Звук с таким именем не найден!");
                return;
            }
            s.source.Play();
        }
    }
}

