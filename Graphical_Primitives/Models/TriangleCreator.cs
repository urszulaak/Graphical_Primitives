using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graphical_Primitives.Models
{
    public class TriangleCreator : IFigureCreator
    {
        public Shape CreateShape(List<Point> points, Dictionary<string, string> inputs, string color)
        {
            Brush brush = Brushes.Transparent;
            if (color != null && color != "None")
            {
                brush = (Brush)new BrushConverter().ConvertFromString(color);
            }

            if (points.Count == 3)
            {
                return new Polygon
                {
                    Points = new PointCollection(points),
                    Stroke = Brushes.Black,
                    Fill = brush,
                    StrokeThickness = 2
                };
            }

            return new Polygon
            {
                Points = new PointCollection
                {
                    new Point(Convert.ToInt32(inputs["x1"]), Convert.ToInt32(inputs["y1"])),
                    new Point(Convert.ToInt32(inputs["x2"]), Convert.ToInt32(inputs["y2"])),
                    new Point(Convert.ToInt32(inputs["x3"]), Convert.ToInt32(inputs["y3"]))
                },
                Stroke = Brushes.Black,
                Fill = brush,
                StrokeThickness = 2
            };
        }
    }
}
