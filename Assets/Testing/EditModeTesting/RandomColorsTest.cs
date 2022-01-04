using System.Collections.Generic;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using NUnit.Framework;
using UnityEngine;

namespace MKTechTest.Assets.Testing.EditModeTesting
{
    public class RandomColorsTest
    {
        [Test] 
        public void Colors_are_shuffled()
        {
            RandomColors randomColors = ScriptableObject.CreateInstance<RandomColors>();
            randomColors.Init();

            List<ColorData> colorList = new List<ColorData>();
            int numColors = 10;

            for (int i = 0; i < numColors; i++)
            {
                ColorData colorData = ScriptableObject.CreateInstance<ColorData>();
                colorData.Init(i.ToString(), true);
                colorList.Add(colorData);
            }

            randomColors.ColorList = colorList;
            int duplicatePositions = 0;
            string[] initialColorArray = new string[randomColors.NumColors];

            for (int i = 0; i < randomColors.NumColors; i++)
            {
                initialColorArray[i] = randomColors.ColorArray[i];
            }

            randomColors.Reset();

            for (int i = 0; i < initialColorArray.Length; i++)
            {
                if (initialColorArray[i] == randomColors.ColorArray[i])
                {
                    duplicatePositions += 1;
                }
            }

            Assert.AreNotEqual(duplicatePositions, initialColorArray.Length);
        }

        [Test]
        public void Colors_are_ignored()
        {
            RandomColors randomColors = ScriptableObject.CreateInstance<RandomColors>();
            randomColors.Init();

            List<ColorData> colorList = new List<ColorData>();
            int numColors = 5;

            for (int i = 0; i < numColors; i++)
            {
                ColorData colorData = ScriptableObject.CreateInstance<ColorData>();
                colorData.Init(i.ToString(), true);
                colorList.Add(colorData);
            }

            randomColors.ColorList = colorList;
            bool colorsIgnored = true;
            List<string> colorsToIgnore = new List<string>();
            int testAmount = 1000;
            randomColors.Reset();

            for (int i = 0; i < randomColors.NumColors - 1; i++)
            {
                colorsToIgnore.Add(randomColors.GetRandomColor().ColorName);
                randomColors.IgnoreColor(colorsToIgnore[i]);

                for (int j = 0; j < testAmount; j++)
                {
                    if (colorsToIgnore.Contains(randomColors.GetRandomColor().ColorName))
                    {
                        colorsIgnored = false;
                    }
                }
            }

            Assert.IsTrue(colorsIgnored);
        }

        [Test]
        public void Colors_are_reset()
        {
            RandomColors randomColors = ScriptableObject.CreateInstance<RandomColors>();
            randomColors.Init();

            List<ColorData> colorList = new List<ColorData>();
            int numColors = 5;

            for (int i = 0; i < numColors; i++)
            {
                ColorData colorData = ScriptableObject.CreateInstance<ColorData>();
                colorData.Init(i.ToString(), true);
                colorList.Add(colorData);
            }

            List<string> colorsIgnored = new List<string>();
            randomColors.ColorList = colorList;

            for (int i = 0; i < randomColors.NumColors; i++)
            {
                colorsIgnored.Add(randomColors.GetRandomColor().ColorName);
                randomColors.IgnoreColor(colorsIgnored[i]);
            }

            string randomColorName = "";
            randomColors.Reset();
            int testAmount = 5000;

            for (int i = 0; i < testAmount; i++)
            {
                randomColorName = randomColors.GetRandomColor().ColorName;
                if (colorsIgnored.Contains(randomColorName))
                {
                    colorsIgnored.Remove(randomColorName);
                }
            }

            Assert.AreEqual(0, colorsIgnored.Count);
        }
    }
}
