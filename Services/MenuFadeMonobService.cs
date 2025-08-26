
using UnityEngine;
using System.Collections;

namespace Pathfinding.Services
{
    public class MenuFadeMonobService: MonoBehaviour
    {
        public void StartFade(bool isUIVisible, CanvasGroup canvasGroup)
        {
            if (isUIVisible)
            {
                StartCoroutine(Fade(canvasGroup, 1));
            }
            else
            {
                StartCoroutine(Fade(canvasGroup, 0));
            }
        }

        private IEnumerator Fade(CanvasGroup canvasGroup, float targetValue)
        {
            float time = 0;
            float intialValue = canvasGroup.alpha;

            if (targetValue == 1)
            {
                canvasGroup.gameObject.SetActive(true);
            }

            while (time < .3f)
            {
                canvasGroup.alpha = Mathf.SmoothStep(intialValue, targetValue, time / .3f);

                time += Time.unscaledDeltaTime;
                yield return null;
            }

            canvasGroup.alpha = targetValue;

            if (targetValue == 0)
            {
                canvasGroup.gameObject.SetActive(false);
            }
        }
    }
}

