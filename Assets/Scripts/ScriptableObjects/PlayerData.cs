using UnityEngine;

namespace MKTechTest.Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Data", menuName = "Player Data")]
    public class PlayerData : ScriptableObject
    {
        [SerializeField] private int successfulAttempts = 0;

        public int SuccessfulAttempts
        {
            get { return successfulAttempts; }
            set { successfulAttempts = value; }
        }

        [SerializeField] private int failedAttempts = 0;

        public int FailedAttempts
        {
            get { return failedAttempts; }
            set { failedAttempts = value; }
        }

        [SerializeField] private float totalTime = 0.0f;

        public float TotalTime
        {
            get { return totalTime; }
            set { totalTime = value; }
        }

        [SerializeField] private int numAttempts = 0;

        public int NumAttempts
        {
            get { return numAttempts; }
            set { numAttempts = value; }
        }

        /// <summary>
        /// Add a single attempt to player data, updates data based off success or not. 
        /// </summary>
        /// <param name="success">Success of if they player guessed the color correctly.</param>
        /// <param name="_totalTime">Total time it took the player all together, this is not total time it took for the guess and will overwrite existing time. </param>
        public void AddAttempt(bool success, float _totalTime)
        {
            if (success)
                successfulAttempts += 1;
            else
                failedAttempts += 1;

            totalTime = _totalTime;
            numAttempts += 1;
        }

        /// <summary>
        /// Simple method for checking if another player data is equal to this one. Method primarily used for testing.
        /// </summary>
        /// <param name="playerData"></param>
        /// <returns>Bool</returns>
        public bool IsEqual(PlayerData playerData)
        {
            return (numAttempts == playerData.NumAttempts
                    && successfulAttempts == playerData.SuccessfulAttempts
                    && failedAttempts == playerData.FailedAttempts
                    && totalTime == playerData.TotalTime);
        }

        /// <summary>
        /// Resets player results to initial state.
        /// </summary>
        public void ResetResults()
        {
            successfulAttempts = 0;
            failedAttempts = 0;
            numAttempts = 0;
            totalTime = 0.0f;
        }
    }
}
