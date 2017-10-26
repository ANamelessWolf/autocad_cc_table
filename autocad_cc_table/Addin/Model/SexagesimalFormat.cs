using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Flareon.Model
{
    /// <summary>
    /// Defines the format in which a sexagesimal number is display
    /// </summary>
    public enum SexagesimalFormat
    {
        NONE = -1,
        DEGREE_MINUTES = 0,
        DEGREE_MINUTES_SECONDS = 1,
        DEGREE_MINUTES_SECONDS_DECIMAL = 2,
    }
}
