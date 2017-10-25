using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Clockwise = System.Windows.Media.SweepDirection;
namespace Nameless.Flareon.Controller
{
    /// <summary>
    /// Defines a group of AutoCAD application utils
    /// </summary>
    public static partial class AutoCADUtils
    {
        /// <summary>
        /// Creates a polyline with its vertices on inverse order
        /// </summary>
        /// <param name="pl">The polyline to invert</param>
        public static Polyline InvertPolyline(this Polyline pl)
        {
            Polyline inversPl = new Polyline();
            for (int i = pl.NumberOfVertices - 1, j = 0; i >= 0; i--, j++)
                inversPl.AddVertexAt(j, pl.GetPoint2dAt(i), pl.GetBulgeAt(i), pl.GetStartWidthAt(i), pl.GetEndWidthAt(i));
            return inversPl;
        }
        /// <summary>
        /// Checks the clockwise direction.
        /// </summary>
        /// <param name="pl">The polyline.</param>
        /// <returns>True if the direction is Clockwise</returns>
        public static bool IsClockwise(this Polyline pl)
        {
            Point3dCollection pts = pl.GetVertices();
            Double dx = 0, dy = 0;
            Point3d middlePt = pl.GeometricExtents.MinPoint.MiddleTo(pl.GeometricExtents.MaxPoint);
            dx = middlePt.X;
            dy = middlePt.Y;
            Double area = 0, sumLft = 0, sumRgt = 0;
            List<Double> sumLftList = new List<Double>(), sumRgtList = new List<Double>();
            for (int i = 0; i < pts.Count - 1; i++)
            {
                sumLftList.Add((pts[i].X - dx) * (pts[i + 1].Y - dy));
                sumRgtList.Add((pts[i + 1].X - dx) * (pts[i].Y - dy));
            }
            for (int i = 0; i < sumLftList.Count - 1; i++)
            {
                sumLft += sumLftList[i];
                sumRgt += sumRgtList[i];
            }
            area = 0.5d * (sumLft - sumRgt);
            var direction = (area > 0) ? Clockwise.Counterclockwise : Clockwise.Clockwise;
            return direction == Clockwise.Clockwise;
        }
        /// <summary>
        /// Gets the vertices.
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <returns>The polyline vertices</returns>
        public static Point3dCollection GetVertices(this Polyline polyline)
        {
            Point3dCollection vertexColl = new Point3dCollection();
            for (int i = 0; i < polyline.NumberOfVertices; i++)
                vertexColl.Add(polyline.GetPoint3dAt(i));
            return vertexColl;
        }
        /// <summary>
        /// Get the Middle point from this point to another point
        /// </summary>
        /// <param name="pt0">The initial point</param>
        /// <param name="ptf">The end point</param>
        /// <returns>The middle point</returns>
        public static Point3d MiddleTo(this Point3d pt0, Point3d ptf)
        {
            return new Point3d((pt0.X + ptf.X) / 2, (pt0.Y + ptf.Y) / 2, (pt0.Z + ptf.Z) / 2);
        }
    }
}
