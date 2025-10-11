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
    public class CircleCreator : IFigureCreator
    {
        public Shape CreateShape(List<Point> points, Dictionary<string, string> inputs, string? color)
        {
            double centerX, centerY, radius;
            Brush brush = Brushes.Transparent;
            if (color != null && color != "None")
            {
                brush = (Brush)new BrushConverter().ConvertFromString(color);
            }

            if (points.Count >= 2)
            {
                var helperPoint = new Point(points[0].X, points[1].Y);
                var a = Math.Abs(points[0].Y - helperPoint.Y);
                var b = Math.Abs(points[1].X - helperPoint.X);
                radius = Math.Sqrt(a * a + b * b);
                centerX = points[0].X;
                centerY = points[0].Y;
            }
            else
            {
                centerX = Convert.ToInt32(inputs["x1"]);
                centerY = Convert.ToInt32(inputs["y1"]);
                radius = Convert.ToInt32(inputs["r"]);
            }

            var circle = new Ellipse
            {
                Width = radius * 2,
                Height = radius * 2,
                Stroke = Brushes.Black,
                Fill = brush,
                StrokeThickness = 2
            };

            Canvas.SetLeft(circle, centerX - radius);
            Canvas.SetTop(circle, centerY - radius);
            return circle;
        }
    }
}
