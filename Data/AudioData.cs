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

        public Coroutine FadeRoutine { get; set; }
        public float TargetVolume { get; set; }
    }
}