using System;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    public class AudioData
    {
        [field: SerializeField] public float MaxVolume { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public AudioClip AudioClip { get; private set; }
        [field: SerializeField] public float FadeDuration { get; private set; }

        // Used to track the current fade coroutine
        public Coroutine FadeRoutine { get; set; }
        // Target volume for fading
        public float TargetVolume { get; set; }
    }
}