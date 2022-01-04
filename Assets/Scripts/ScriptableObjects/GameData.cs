using System.Collections.Generic;
using UnityEngine;

namespace MKTechTest.Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Game Data", menuName = "Game Data")]
    public class GameData : ScriptableObject
    {
        [SerializeField] private int totalAttempts = 5;

        public int TotalAttempts
        {
            get
            {
                return totalAttempts;
            }
        }

        [SerializeField] private RandomColors defaultRandomColors;

        public RandomColors DefaultRandomColors
        {
            get
            {
                return defaultRandomColors;
            }
        }

        [SerializeField] private RandomColors customRandomColors;

        public RandomColors CustomRandomColors
        {
            get
            {
                return customRandomColors;
            }
        }

        [SerializeField] private bool enableDynamicBackground = true;

        public bool EnableDynamicBackground
        {
            get
            {
                return enableDynamicBackground;
            }
            set
            {
                enableDynamicBackground = value;
            }
        }


        /// <summary>
        /// Initializes values, primarily used for testing.
        /// </summary>
        public void Init()
        {
            defaultRandomColors = ScriptableObject.CreateInstance<RandomColors>();
            defaultRandomColors.Init();
            customRandomColors = ScriptableObject.CreateInstance<RandomColors>();
            customRandomColors.Init();
        }

        /// <summary>
        /// Initializes the RGB color values of the custom color data, this does not
        /// affect default color data as those values are never assigned to any text, image or UI element.
        /// </summary>
        public void InitializeCustomRandomColors()
        {
            foreach (ColorData colorData in customRandomColors.ColorList)
            {
                colorData.InitializeColorRGB();
            }
        }

    }
}
