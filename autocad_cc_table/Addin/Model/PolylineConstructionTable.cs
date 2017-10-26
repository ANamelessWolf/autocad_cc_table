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
    public class PolylineConstructionTable
    {
        /// <summary>
        /// The polyline curve used to create the construction table
        /// </summary>
        public readonly Polyline Curve;
        /// <summary>
        /// The polyline area
        /// </summary>
        public readonly Double Area;
        /// <summary>
        /// The polyline construction rows
        /// </summary>
        public List<ConstructionRow> Rows;
        /// <summary>
        /// Initializes a new instance of the <see cref="PolylineConstructionTable"/> class.
        /// </summary>
        /// <param name="pl">The polyline.</param>
        public PolylineConstructionTable(Polyline pl)
        {
            this.Area = pl.Area;
            this.Curve = pl.Fix();
            this.Rows = new List<ConstructionRow>();
            this.ProcessPolyline(pl.GetVerticesInOrder(0));
        }
        /// <summary>
        /// Processes the polyline.
        /// </summary>
        /// <param name="v">The polyline vertices.</param>
        private void ProcessPolyline(int[] vertices)
        {
            SegmentType segTp;
            Double dist = 0;
            for (int i = 0; i < vertices.Length; i++)
            {
                int v = vertices[i];
                segTp = this.Curve.GetSegmentType(v);
                if (segTp == SegmentType.Arc)
                    this.Rows.Add(new ConstructionRow(this.Curve.GetArcSegment2dAt(v), v, dist));
                else
                    this.Rows.Add(new ConstructionRow(this.Curve.GetLineSegment2dAt(v), v, dist));
                dist += this.Rows.LastOrDefault().Distance;
            }
        }
    }
}
