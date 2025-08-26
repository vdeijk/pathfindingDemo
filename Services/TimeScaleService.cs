using UnityEngine;

namespace Pathfinding.Services
{
    [DefaultExecutionOrder(100)]
    public class TimeScaleService
    {
        public void SetTimeToNormal()
        {
            Time.timeScale = 1;
        }

        public void SetTimeToZero()
        {
            Time.timeScale = 0;
        }
    }
}