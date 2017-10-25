using Nameless.Flareon.Runtime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
