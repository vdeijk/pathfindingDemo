using UnityEngine;
using System.Collections;
using Pathfinding.Data;

namespace Pathfinding.Services
{
    public class CameraCenteringMonobService : MonoBehaviour
    {
        private CameraData _data;

        public void Init(CameraData data)
        {
            _data = data;
        }

        // Smoothly moves camera to target position
        public IEnumerator SmoothCentering(Vector3 targetPosition)
        {
            Vector3 startPosition = _data.TrackingTargetTransform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float duration = distance / _data.CenteringSmoothConstant;
            duration = Mathf.Clamp(duration, 0.3f, 1.5f);
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                float smoothT = Mathf.SmoothStep(0, 1, t);
                _data.TrackingTargetTransform.position = Vector3.Lerp(
                    startPosition,
                    targetPosition,
                    smoothT
                );
                elapsedTime += Time.unscaledDeltaTime;
                yield return null;
            }

            _data.TrackingTargetTransform.position = targetPosition;
        }
    }
}
