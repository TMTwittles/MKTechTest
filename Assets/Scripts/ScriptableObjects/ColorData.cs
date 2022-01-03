using UnityEngine;

namespace MKTechTest.Assets.Scripts.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Color", menuName = "Color Data")]
    public class ColorData : ScriptableObject
    {
        [SerializeField] private bool canBeModified;

        public bool CanBeModified
        {
            get { return canBeModified; }
            set { canBeModified = value; }
        }

        [SerializeField] private string colorName;
        public string ColorName
        {
            get { return colorName; }
            set { colorName = value; }
        }

        [SerializeField] private Color colorRGB;

        public Color ColorRGB
        {
            get { return colorRGB; }
        }

        /// <summary>
        /// Factory method for creating default color data, used primarily for testing.
        /// </summary>
        /// <param name="_colorName"></param>
        /// <param name="_canBeModified"></param>
        public void Init(string _colorName, bool _canBeModified)
        {
            canBeModified = _canBeModified;
            colorName = _colorName;
            colorRGB = Color.black;
        }

        /// <summary>
        /// Sets the RGB value of the Color Data, will return an error if the color data
        /// cannot be modified. 
        /// </summary>
        /// <Note>
        /// Does not allocate new memory, only changes respective RGB values. 
        /// </Note>
        /// <param name="colorName"> String value, name of color to change </param>
        /// /// <param name="colorRGB"> Color value, color value to change color to </param>
        /// <returns> Color data of the specified color. </returns>
        public void SetColorRGB(Color color)
        {
            if (CanBeModified)
            {
                colorRGB.r = color.r;
                colorRGB.g = color.g;
                colorRGB.b = color.b;
            }
            else
                Debug.LogError("ERROR: Selected color data can not be modified");
        }

        /// <summary>
        /// Inits colorRGB value by allocating memory to it.  
        /// </summary>
        public void InitializeColorRGB()
        {
            colorRGB = new Color(colorRGB.r, colorRGB.g, colorRGB.b);
        }
    }
}


