using Autodesk.AutoCAD.Geometry;
using Nameless.Flareon.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Flareon.Model
{
    public class Angle
    {
        /// <summary>
        /// The angle value in radians
        /// </summary>
        public readonly double Radians;
        /// <summary>
        /// Gets the angle in degrees.
        /// </summary>
        /// <value>
        /// The degrees.
        /// </value>
        public double Degrees
        {
            get
            {
                return this.Radians.ToDegree();
            }
        }
        /// <summary>
        /// Gets angle in sexagesimal format.
        /// </summary>
        /// <value>
        /// The angle in sexagesimal.
        /// </value>
        public String Sexagesimal
        {
            get
            {
                return this.Degrees.ToSexagesimal();
            }
        }
        /// <summary>
        /// Gets angle in rumbo format.
        /// </summary>
        /// <value>
        /// The angle in rumbo format.
        /// </value>
        public String Rumbo
        {
            get
            {
                return this.Degrees.ToRumbo();
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> class.
        /// </summary>
        /// <param name="value">The angle value in radians.</param>
        public Angle(double value)
        {
            this.Radians = value;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="Angle"/> class.
        /// </summary>
        /// <param name="start">The segment start point.</param>
        /// <param name="end">The segment end point.</param>
        public Angle(Point2d start, Point2d end) : 
            this(start.GetVectorTo(end).Angle)
        {

        }
    }
}
