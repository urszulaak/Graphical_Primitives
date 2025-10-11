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
                    new Point(Convert.ToDouble(inputs["x1"]), Convert.ToDouble(inputs["y1"])),
                    new Point(Convert.ToDouble(inputs["x2"]), Convert.ToDouble(inputs["y2"])),
                    new Point(Convert.ToDouble(inputs["x3"]), Convert.ToDouble(inputs["y3"]))
                },
                Stroke = Brushes.Black,
                Fill = brush,
                StrokeThickness = 2
            };
        }
    }
}
