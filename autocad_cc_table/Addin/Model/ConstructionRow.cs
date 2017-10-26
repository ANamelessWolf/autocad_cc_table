using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using Nameless.Flareon.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nameless.Flareon.Model
{
    /// <summary>
    /// Defines a Construction Table Row
    /// </summary>
    public class ConstructionRow
    {
        /// <summary>
        /// The side start point
        /// </summary>
        public readonly Point2d Start;
        /// <summary>
        /// The side end point
        /// </summary>
        public readonly Point2d End;
        /// <summary>
        /// The segment distance
        /// </summary>
        public readonly Double Distance;
        /// <summary>
        /// The polyline cumulative distance
        /// </summary>
        public readonly Double CumulativeDistance;
        /// <summary>
        /// Gets side direction
        /// </summary>
        public readonly Angle Angle;
        /// <summary>
        /// The vertex index
        /// </summary>
        public readonly int Index;
        /// <summary>
        /// The Segment
        /// </summary>
        public readonly Curve2d Segment;
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructionRow"/> class.
        /// </summary>
        /// <param name="line">The segment as line.</param>
        /// <param name="index">The vertex index.</param>
        /// <param name="cumulativeDistance">The cumulative distance.</param>
        public ConstructionRow(LineSegment2d line, int index, Double cumulativeDistance = 0) :
            this((line as Curve2d), index, cumulativeDistance)
        {
            this.Distance = line.Length;
            this.CumulativeDistance = cumulativeDistance + this.Distance;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructionRow"/> class.
        /// </summary>
        /// <param name="arc">The arc segment taken from a polyline.</param>
        /// <param name="index">The vertex index.</param>
        /// <param name="cumulativeDistance">The cumulative distance.</param>
        public ConstructionRow(CircularArc2d arc, int index, Double cumulativeDistance = 0) :
            this((arc as Curve2d), index, cumulativeDistance)
        {
            Arc a = new Arc(arc.Center.ToPoint3d(), arc.Radius, arc.StartAngle, arc.EndAngle);
            this.Distance = a.Length;
            this.CumulativeDistance = cumulativeDistance + this.Distance;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="ConstructionRow"/> class.
        /// </summary>
        /// <param name="segment">The segment taken from a polyline.</param>
        /// <param name="index">The vertex index.</param>
        /// <param name="cumulativeDistance">The cumulative distance.</param>
        public ConstructionRow(Curve2d segment, int index, Double cumulativeDistance = 0)
        {
            this.Segment = segment;
            this.Start = segment.StartPoint;
            this.End = segment.EndPoint;
            this.Index = index;
            this.Angle = new Angle(this.Start, this.End);
        }
        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return String.Format("V({0}) L:{1}-{2}", this.Index, this.Start.ToFormat(), this.End.ToFormat());
        }
    }
}
