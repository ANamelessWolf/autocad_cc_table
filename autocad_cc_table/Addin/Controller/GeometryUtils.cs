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
        /// Fix a polyline
        /// </summary>
        /// <param name="polyline">The polyline to fix.</param>
        /// <returns>The fixed polyline</returns>
        public static Polyline Fix(this Polyline polyline)
        {
            Polyline pl = new Polyline();
            List<Point2d> vertices = new List<Point2d>();
            Point2d v;
            double b;
            Boolean isClockwise = polyline.IsClockwise();
            for (int i = 0; i < polyline.NumberOfVertices; i++)
            {
                v = polyline.GetPoint2dAt(i);
                b = polyline.GetBulgeAt(i);
                if (vertices.Count(pt => pt.X == v.X && pt.Y == v.Y) == 0)
                {
                    if (isClockwise)
                        pl.AddVertexAt(i, v, b, 0, 0);
                    else
                        pl.AddVertexAt(0, v, b, 0, 0);
                    vertices.Add(v);
                }
            }
            pl.Closed = true;
            return pl;
        }
        /// <summary>
        /// Gets the vertices without repetitions
        /// </summary>
        /// <param name="polyline">The polyline.</param>
        /// <returns>The polyline vertices</returns>
        public static Point3dCollection GetVertices(this Polyline polyline)
        {
            Point3dCollection vertexColl = new Point3dCollection();
            Point3d v;
            for (int i = 0; i < polyline.NumberOfVertices; i++)
            {
                v = polyline.GetPoint3dAt(i);
                if (vertexColl.OfType<Point3d>().Count(pt => pt.X == v.X && pt.Y == v.Y) == 0)
                    vertexColl.Add(v);
            }
            return vertexColl;
        }
        /// <summary>
        /// Gets the polyline vertices in order.
        /// </summary>
        /// <param name="pl">The polyline.</param>
        /// <param name="startIndex">The start index of the polyline.</param>
        /// <returns>The index in which the points are procesed</returns>
        public static int[] GetVerticesInOrder(this Polyline pl, int startIndex)
        {
            int[] plIndexes = new int[pl.NumberOfVertices];
            int j = 0;
            for (int i = 0; i < pl.NumberOfVertices; i++)
            {
                plIndexes[i] = startIndex;
                if (startIndex <= pl.NumberOfVertices - 1)
                    startIndex++;
                else
                {
                    plIndexes[i] = 0 + j;
                    j++;
                }
            }
            return plIndexes;
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
        /// <summary>
        /// Converts a Point 2d to a point 3d.
        /// </summary>
        /// <param name="pt">The point.</param>
        /// <param name="z">The z default value.</param>
        /// <returns>The point converted to a point 3d</returns>
        public static Point3d ToPoint3d(this Point2d pt, Double z = 0)
        {
            return new Point3d(pt.X, pt.Y, z);
        }
    }
}
