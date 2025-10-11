using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Graphical_Primitives.Models
{
    public class SquareCreator : IFigureCreator
    {
        public Shape CreateShape(List<Point> points, Dictionary<string, string> inputs, string color)
        {
            double x1, y1, x2, y2;
            Brush brush = Brushes.Transparent;
            if (color != null)
            {
                if (color != "None") brush = (Brush)new BrushConverter().ConvertFromString(color);
            }

            if (points.Count >= 2)
            {
                x1 = points[0].X;
                y1 = points[0].Y;
                x2 = points[1].X;
                y2 = points[1].Y;
            }
            else
            {
                x1 = Convert.ToDouble(inputs["x1"]);
                y1 = Convert.ToDouble(inputs["y1"]);
                x2 = Convert.ToDouble(inputs["x2"]);
                y2 = Convert.ToDouble(inputs["y2"]);
            }

            double width = Math.Abs(x2 - x1);
            double height = Math.Abs(y2 - y1);
            double left = Math.Min(x1, x2);
            double top = Math.Min(y1, y2);

            var rect = new Rectangle
            {
                Width = width,
                Height = height,
                Stroke = Brushes.Black,
                Fill = brush,
                StrokeThickness = 2
            };

            Canvas.SetLeft(rect, left);
            Canvas.SetTop(rect, top);
            return rect;
        }
    }
}
