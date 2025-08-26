using System;
using System.Collections.Generic;
using UnityEngine;

namespace Pathfinding.Data
{
    [Serializable]
    //Stores data for fadable audio sources
    public class AudioData
    {
        [field: SerializeField] public float MaxVolume { get; private set; }
        [field: SerializeField] public AudioSource AudioSource { get; private set; }
        [field: SerializeField] public float FadeDuration { get; private set; }

        // Target volume for fading
        public float TargetVolume { get; set; }
    }
}