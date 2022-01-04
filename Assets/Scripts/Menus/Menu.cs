using System;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using UnityEngine;

namespace MKTechTest.Assets.Scripts.Menus
{
    public abstract class Menu : MonoBehaviour
    {
        [Header("Menu Attributes")]
        public MenuID ID;

        [Header("Color Data")]
        [SerializeField] protected RandomColors visualRandomColors;
        [SerializeField] protected GameData data;

        /// <summary>
        /// Resets the random colors data, such that they are shuffled but also ignores
        /// the background color.
        /// </summary>
        protected void ResetRandomColors()
        {
            data.CustomRandomColors.Reset(); // Randomize random colors
            // Ignore the color of the background to persist into the new game
            data.CustomRandomColors.IgnoreColor(GameManager.Instance.BackgroundColorData.ColorName);
        }

        protected string GetTimeFormatted(float elapsedTime)
        {
            return TimeSpan.FromSeconds(elapsedTime).ToString(@"mm\:ss\:ff");
        }

        /// <summary>
        /// Resets the menu to initial state.
        /// </summary>
        public abstract void Reset();
    }
}
