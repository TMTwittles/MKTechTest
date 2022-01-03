using System.Collections.Generic;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace MKTechTest.Assets.Testing.EditModeTesting
{
    public class GameDataTest
    {
        [Test]
        public void Custom_colors_can_be_modified()
        {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData.Init();

            List<ColorData> customColorList = new List<ColorData>();
            List<ColorData> defaultColorList = new List<ColorData>();
            int numColors = 10;

            for (int i = 0; i < 10; i++)
            {
                ColorData customColorData = ScriptableObject.CreateInstance<ColorData>();
                customColorData.Init(i.ToString(), true);
                customColorList.Add(customColorData);

                ColorData defaultColorData = ScriptableObject.CreateInstance<ColorData>();
                defaultColorData.Init(i.ToString(), false);
                defaultColorList.Add(defaultColorData);
            }

            gameData.CustomRandomColors.ColorList = customColorList;
            gameData.DefaultRandomColors.ColorList = defaultColorList;

            bool colorsCanBeModified = true;
            Color customColor = new Color(1.0f, 1.0f, 1.0f);

            foreach (ColorData colorData in gameData.CustomRandomColors.ColorList)
            {
                customColor.r = Random.Range(0.0f, 1.0f);
                customColor.g = Random.Range(0.0f, 1.0f);
                customColor.b = Random.Range(0.0f, 1.0f);
                customColor.a = Random.Range(0.0f, 1.0f);
                colorData.SetColorRGB(customColor);
            }

            foreach (ColorData colorData in gameData.CustomRandomColors.ColorList)
            {
                if (colorData.ColorRGB == gameData.DefaultRandomColors.GetColorData(colorData.ColorName).ColorRGB)
                {
                    colorsCanBeModified = false;
                }
            }

            Assert.IsTrue(colorsCanBeModified);
        }

        [Test]
        public void Custom_colors_can_be_reset()
        {
            GameData gameData = ScriptableObject.CreateInstance<GameData>();
            gameData.Init();

            List<ColorData> customColorList = new List<ColorData>();
            List<ColorData> defaultColorList = new List<ColorData>();
            int numColors = 10;

            for (int i = 0; i < 10; i++)
            {
                ColorData customColorData = ScriptableObject.CreateInstance<ColorData>();
                customColorData.Init(i.ToString(), true);
                customColorList.Add(customColorData);

                ColorData defaultColorData = ScriptableObject.CreateInstance<ColorData>();
                defaultColorData.Init(i.ToString(), false);
                defaultColorList.Add(defaultColorData);
            }

            gameData.CustomRandomColors.ColorList = customColorList;
            gameData.DefaultRandomColors.ColorList = defaultColorList;

            bool colorsCanBeReset = true;
            Color customColor = new Color(1.0f, 1.0f, 1.0f);

            foreach (ColorData colorData in gameData.CustomRandomColors.ColorList)
            {
                customColor.r = Random.Range(0.0f, 1.0f);
                customColor.g = Random.Range(0.0f, 1.0f);
                customColor.b = Random.Range(0.0f, 1.0f);
                customColor.a = Random.Range(0.0f, 1.0f);
                colorData.SetColorRGB(customColor);
            }

            foreach (ColorData colorData in gameData.CustomRandomColors.ColorList)
            {
                colorData.SetColorRGB(gameData.DefaultRandomColors.GetColorData(colorData.ColorName).ColorRGB);
            }

            foreach (ColorData colorData in gameData.CustomRandomColors.ColorList)
            {
                if (colorData.ColorRGB != gameData.DefaultRandomColors.GetColorData(colorData.ColorName).ColorRGB)
                {
                    colorsCanBeReset = false;
                }
            }

            Assert.IsTrue(colorsCanBeReset);
        }
    }
}
