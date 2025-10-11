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
    public class LineCreator : IFigureCreator
    {
        public Shape CreateShape(List<Point> points, Dictionary<string, string> inputs, string color)
        {
            Brush brush = Brushes.Black;
            if (color != null && color != "None")
            {
                brush = (Brush)new BrushConverter().ConvertFromString(color);
            }

            if (points.Count >= 2)
            {
                return new Line
                {
                    X1 = points[0].X,
                    Y1 = points[0].Y,
                    X2 = points[1].X,
                    Y2 = points[1].Y,
                    Stroke = brush,
                    StrokeThickness = 2
                };
            }

            return new Line
            {
                X1 = Convert.ToInt32(inputs["x1"]),
                Y1 = Convert.ToInt32(inputs["y1"]),
                X2 = Convert.ToInt32(inputs["x2"]),
                Y2 = Convert.ToInt32(inputs["y2"]),
                Stroke = brush,
                StrokeThickness = 2
            };
        }
    }
}
