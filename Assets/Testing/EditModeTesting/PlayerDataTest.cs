using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace MKTechTest.Assets.Testing.EditModeTesting
{
    public class PlayerDataTest
    {
        [Test] 
        public void Successful_attempts_added_correctly()
        {
            PlayerData expectedPlayerData = ScriptableObject.CreateInstance<PlayerData>();
            float timePerGuess = 1.0f;
            expectedPlayerData.NumAttempts = 50;
            expectedPlayerData.SuccessfulAttempts = expectedPlayerData.NumAttempts;
            expectedPlayerData.FailedAttempts = 0;
            expectedPlayerData.TotalTime = expectedPlayerData.NumAttempts * timePerGuess;

            PlayerData testPlayerData = ScriptableObject.CreateInstance<PlayerData>();
            float elapsedTime = timePerGuess;

            for (int i = 0; i < expectedPlayerData.NumAttempts; i++)
            {
                testPlayerData.AddAttempt(true, elapsedTime);
                elapsedTime += timePerGuess;
            }

            Assert.IsTrue(testPlayerData.IsEqual(expectedPlayerData));
        }

        [Test]
        public void Failed_attempts_added_correctly()
        {
            PlayerData expectedPlayerData = ScriptableObject.CreateInstance<PlayerData>();
            float timePerGuess = 1.0f;
            expectedPlayerData.NumAttempts = 50;
            expectedPlayerData.SuccessfulAttempts = 0;
            expectedPlayerData.FailedAttempts = expectedPlayerData.NumAttempts;
            expectedPlayerData.TotalTime = expectedPlayerData.NumAttempts * timePerGuess;

            PlayerData testPlayerData = ScriptableObject.CreateInstance<PlayerData>();
            float elapsedTime = timePerGuess;

            for (int i = 0; i < expectedPlayerData.NumAttempts; i++)
            {
                testPlayerData.AddAttempt(false, elapsedTime);
                elapsedTime += timePerGuess;
            }

            Assert.IsTrue(testPlayerData.IsEqual(expectedPlayerData));
        }

        [Test]
        public void Data_reset_correctly()
        {
            PlayerData expectedPlayerData = ScriptableObject.CreateInstance<PlayerData>();
            PlayerData testPlayerData = ScriptableObject.CreateInstance<PlayerData>();

            int numAttempts = 50;
            float timerPerGuess = 1.0f;
            float elapsedTime = timerPerGuess;
            for (int i = 0; i < numAttempts; i++)
            {
                testPlayerData.AddAttempt(true, elapsedTime);
                testPlayerData.AddAttempt(false, elapsedTime);
                elapsedTime += timerPerGuess;
            }
            testPlayerData.ResetResults();

            Assert.IsTrue(testPlayerData.IsEqual(expectedPlayerData));
        }
    }
}
