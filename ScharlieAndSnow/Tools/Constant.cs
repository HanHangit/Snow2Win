using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScharlieAndSnow
{
    public class Constant
    {
        public static Game1 Gameref {
            get; set; }
        public static int x = 1270;
        public static int y = 720;
        /// <summary>
        /// Gibt die prozentuale Position von dem Windows zurück
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static Vector2 Calculate(float xValue, float yValue)
        {
            float helpx = x / 100 * xValue;
            float helpy = y / 100 * yValue;
            return new Vector2(helpx, helpy);
        }
        /// <summary>
        /// Berechnet ProzentSatz zwischen Wert und Maximum
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static float ValueToPercent(float value, float maxValue)
        {
            return (value / maxValue) * 100;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static float PercentFromValue(float percent, float maxValue)
        {
            return (maxValue / 100) * percent;
        }

        /// <summary>
        /// Berechnet Prozentuelen Wert von X oder Y
        /// </summary>
        /// <param = id> 0 = x Value,  1 = y Value</param>
        /// <returns></returns>
        public static float PercentFromWindow(float value, int id)
        {
            return (id == 0)  ?  x/100 * value :  y / 100 * value;
        }
        /// <summary>
        /// Gibt den prozentualen Wert von (x,y) von xMax/yMax
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        public static Vector2 Calculate(float xValue, float yValue, float xMax, float yMax)
        {
            float helpx = xMax / 100 * xValue;
            float helpy = yMax / 100 * yValue;
            return new Vector2(helpx, helpy);
        }
    }
}
