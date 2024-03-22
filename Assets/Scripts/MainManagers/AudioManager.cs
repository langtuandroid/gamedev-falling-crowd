using UnityEngine;

namespace MainManagers
{
    public class AudioManager : MonoBehaviour
    {
        private const string soundKey = "SoundEnabled";
        private bool isSoundEnabled = true;

        void Start()
        {
            LoadSoundState();
            ApplySoundState();
        }
        
        private void LoadSoundState()
        {
            if (PlayerPrefs.HasKey(soundKey))
            {
                isSoundEnabled = PlayerPrefs.GetInt(soundKey) == 1;
            }
            else
            {
                isSoundEnabled = true;
            }
        }
        
        private void ApplySoundState()
        {
            if (isSoundEnabled)
            {
                // Включить звук
                AudioListener.volume = 1f;
            }
            else
            {
                // Отключить звук
                AudioListener.volume = 0f;
            }
        }

      
        public void ToggleSound()
        {
            isSoundEnabled = !isSoundEnabled;
            ApplySoundState();
            // Сохранить состояние звука
            PlayerPrefs.SetInt(soundKey, isSoundEnabled ? 1 : 0);
            PlayerPrefs.Save();
        }
    }
}
