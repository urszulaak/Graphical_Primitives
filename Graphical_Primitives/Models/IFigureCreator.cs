using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Shapes;

namespace Graphical_Primitives.Models
{
    public interface IFigureCreator
    {
        Shape CreateShape(List<Point> points, Dictionary<string, string> inputs);
    }
}
