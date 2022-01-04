using System.Collections.Generic;
using MKTechTest.Assets.Scripts.ScriptableObjects;
using UnityEngine;
using UnityEngine.UI;

namespace MKTechTest.Assets.Scripts.Menus
{
    public abstract class ColoredMenu : Menu
    {
        [Header("ColorGroups")] 
        [SerializeField] protected List<Text> colorGroup01;
        [SerializeField] protected List<Text> colorGroup02;
        [SerializeField] protected List<Text> colorGroup03;
        [SerializeField] protected List<Text> colorGroup04;
        [SerializeField] protected List<Text> colorGroup05;
        protected List<List<Text>> colorGroups;

        public List<List<Text>> ColorGroups
        {
            get { return colorGroups; }
        }

        /// <summary>
        /// Initializes the color groups, this should be called in Awake() or before
        /// calling UpdateColorGroupsRandomly().
        /// </summary>
        protected void InitializeColorGroups()
        {
            colorGroups = new List<List<Text>>();
            colorGroups.Add(colorGroup01);
            colorGroups.Add(colorGroup02);
            colorGroups.Add(colorGroup03);
            colorGroups.Add(colorGroup04);
            colorGroups.Add(colorGroup05);

            for (int i = 0; i < colorGroups.Count; i++)
            {
                if (colorGroups[i].Count == 0)
                    colorGroups.RemoveAt(i);
            }
        }

        /// <summary>
        /// Updates the color groups to random colors.
        /// </summary>
        protected void UpdateColorGroupsRandomly()
        {
            ResetRandomColors();

            ColorData colorData = null;
            foreach (List<Text> texts in colorGroups)
            {
                colorData = data.CustomRandomColors.GetRandomColor();
                data.CustomRandomColors.IgnoreColor(colorData.ColorName);
                foreach (Text text in texts)
                {
                    text.color = colorData.ColorRGB;
                }
            }
        }

    }
}

