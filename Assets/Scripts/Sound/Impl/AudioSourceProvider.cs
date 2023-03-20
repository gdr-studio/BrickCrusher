using System.Collections.Generic;
using System.Linq;
using Game.Sound.Data;
using Helpers;
using UnityEngine;

namespace Game.Sound.Impl
{
    public class AudioSourceProvider : MonoBehaviour, IAudioSourceProvider
    {
        [SerializeField] private int _intiialCount = 50;
        [SerializeField] private Transform _soundParent;
        [SerializeField] private List<SoundSource> _sources;

        private HashSet<SoundSource> _availableSources = new HashSet<SoundSource>();

        public void Init()
        {
            var start = _availableSources.Count;
            for (var i = 0; i < _intiialCount; i++)
            {
                var id = $"SoundSource_{i + start}";
                CreateNewSource(id);
            }
        }

        private void CreateNewSource(string id)
        {
            var go = new GameObject(id);
            go.transform.parent = _soundParent;
            var audioSource = go.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
            var soundSource = new SoundSource()
            {
                DefaultParent = _soundParent,
                AudSource = audioSource,
                Go = go,
            };
            _sources.Add(soundSource);
            _availableSources.Add(soundSource);
        }
        
        public SoundSource GetFreeAudioSource()
        {
            var source = _availableSources.FirstOrDefault(t => t.AudSource != null);
            if (source != null)
                _availableSources.Remove(source);
            else
            {
                Dbg.Green($"No sources left, extent");
                Init();
                source = _availableSources.FirstOrDefault(t => t.AudSource != null);
            }
            return source;
        }

        public void ReturnSource(SoundSource source)
        {
            source.AudSource.loop = false;
            source.AudSource.spatialBlend = 0f;
            source.AudSource.rolloffMode = AudioRolloffMode.Logarithmic;
            source.AudSource.clip = null;
            _availableSources.Add(source);
        }
    }
}