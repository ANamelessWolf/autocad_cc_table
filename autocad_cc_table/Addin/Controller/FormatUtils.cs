using Autodesk.AutoCAD.Geometry;
using Nameless.Flareon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Nameless.Flareon.Assets.Constants;
namespace Nameless.Flareon.Controller
{
    /// <summary>
    /// Defines a group of AutoCAD application utils
    /// </summary>
    public static partial class AutoCADUtils
    {
        /// <summary>
        /// Gets the number in string format
        /// </summary>
        /// <param name="value">The number value.</param>
        /// <returns>The number value formatted</returns>
        public static string AsNumberFormat(this double value)
        {
            string format = "{0:N" + Commands.App.NumberOfDecimals.Value.ToString() + "}";
            return String.Format(format, value);
        }
        /// <summary>
        /// Converts a radian angle to a degree angle.
        /// </summary>
        /// <param name="radians">The angle in radians.</param>
        /// <returns>The angle in degree</returns>
        public static double ToDegree(this double radians)
        {
            double degrees = radians * (180 / Math.PI);
            if (degrees < 0)
                while (degrees < 0)
                    degrees += 360;
            else if (degrees >= 360)
                while (degrees >= 360)
                    degrees -= 360;
            return degrees;
        }
        /// <summary>
        /// Converts a degree angle to the sexagesimal format.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in sexagesimal format</returns>
        public static string ToSexagesimal(this double degrees)
        {
            int intDegree, minutes;
            double seconds, factor;
            string format;
            intDegree = (int)degrees;
            //minutes
            factor = (degrees - (double)intDegree) * 60d;
            minutes = (int)factor;
            //seconds
            seconds = (factor - (double)minutes) * 60d;
            //format
            if (Commands.App.AzimutFormat == Model.SexagesimalFormat.DEGREE_MINUTES_SECONDS_DECIMAL)
                format = String.Format(FORMAT_SEXAG_FULL, intDegree, minutes, Math.Round(seconds, 2));
            else if (Commands.App.AzimutFormat == Model.SexagesimalFormat.DEGREE_MINUTES_SECONDS)
                format = String.Format(FORMAT_SEXAG_SEC, intDegree, minutes, Math.Round(seconds, 0));
            else
                format = String.Format(FORMAT_SEXAG_MIN, intDegree, minutes);
            return format;
        }
        /// <summary>
        /// Converts a degree angle to the rumbo format.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <returns>The angle in Rumbo format</returns>
        public static string ToRumbo(this double degrees)
        {
            //Primero leemos la parte entera de los grados
            int intDegrees = (int)degrees;
            if (intDegrees <= 90)
                degrees = 90.0 - degrees;
            else if ((intDegrees > 90) && (intDegrees <= 180))
                degrees -= 90.0;
            else if ((intDegrees > 180) && (intDegrees <= 270))
                degrees = 270.0 - degrees;
            else
                degrees -= 270.0;
            degrees = Math.Abs(degrees);
            return intDegrees.ToGeographicCoordinates(degrees.ToSexagesimal());
        }
        /// <summary>
        /// Converts a degree angle into geographic coordinates.
        /// </summary>
        /// <param name="degrees">The angle in degrees.</param>
        /// <param name="sexagesimal">The angle in sexagesimal format.</param>
        /// <returns></returns>
        private static string ToGeographicCoordinates(this int degrees, string sexagesimal)
        {
            string str = String.Empty;
            if ((degrees >= 0) && (degrees <= 90))
            {
                str = " N " + sexagesimal + " E ";
                if (degrees == 90)
                    str = " N " + sexagesimal + "   ";
            }
            if ((degrees > 90) && (degrees <= 180))
                str = " N " + sexagesimal + " W ";
            if ((degrees > 180) && (degrees <= 270))
            {
                str = " S " + sexagesimal + " W ";
                if (degrees == 270)
                    str = " S " + sexagesimal + "   ";
            }
            if (degrees > 270)
                str = " S " + sexagesimal + " E ";
            return str;
        }
        /// <summary>
        /// Formats a point
        /// </summary>
        /// <param name="pt">The point to format.</param>
        /// <returns>The point in the string format</returns>
        public static string ToFormat(this Point2d pt)
        {
            return String.Format("({0},{1})", pt.X.AsNumberFormat(), pt.Y.AsNumberFormat());
        }
    }
}
