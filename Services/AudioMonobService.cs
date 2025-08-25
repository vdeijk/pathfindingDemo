using Pathfinding.Data;
using System.Collections;
using UnityEngine;

namespace Pathfinding.Services
{
    public class AudioMonobService: MonoBehaviour
    {
        public void StartFade(AudioData data)
        {
            if (data.FadeRoutine != null) StopCoroutine(data.FadeRoutine);

            data.FadeRoutine = StartCoroutine(Fade(data));
        }

        // Fades the volume of an AudioSource from intialValue to targetValue over duration
        public IEnumerator Fade(AudioData data)
        {
            float time = 0;

            if (data.TargetVolume == 1)
            {
                data.AudioSource.Play();
            }

            while (time < data.FadeDuration)
            {
                data.AudioSource.volume = Mathf.SmoothStep(data.AudioSource.volume, data.TargetVolume, time / data.FadeDuration);
                time += Time.unscaledDeltaTime;
                yield return null;
            }

            data.AudioSource.volume = data.TargetVolume;

            if (data.AudioSource.volume == 0)
            {
                data.AudioSource.Stop();
            }
        }
    }
}

