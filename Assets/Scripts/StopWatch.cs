using System;
using UnityEngine;

namespace MKTechTest.Assets.Scripts
{
    public class StopWatch : MonoBehaviour
    {
        private bool active;
        private bool paused = false;
        private float timeOffset; // Offset for Time.time to reach 0.0f;
        private float elapsedTime = 0.0f;

        public float ElapsedTime
        {
            get { return elapsedTime; }
        }

        /// <summary>
        /// Turns on stop watch.
        /// If stop watch was paused, stop watch will resume from last time it was paused.
        /// </summary>
        public void TurnOn()
        {
            if (!paused)
                timeOffset = Time.time;
            paused = false;
            active = true;
        }

        /// <summary>
        /// Resets stop watch to initial state.
        /// </summary>
        public void Reset()
        {
            elapsedTime = 0.0f;
            timeOffset = 0.0f;
            active = false;
            paused = false;
        }

        /// <summary>
        /// Pauses stop watch.
        /// Call TurnOn() to resume using stopwatch.
        /// </summary>
        public void Pause()
        {
            active = false;
            paused = true;
        }

        private void Update()
        {
            if (active)
            {
                elapsedTime = Time.time - timeOffset;
            }
        }

    }
}
