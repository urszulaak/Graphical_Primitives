using Graphical_Primitives.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graphical_Primitives.Services
{
    public static class ShapeFactory
    {
        public static IFigureCreator GetCreator(string figureType)
        {
            return figureType switch
            {
                "Line" => new LineCreator(),
                "Square" => new SquareCreator(),
                "Circle" => new CircleCreator(),
                "Triangle" => new TriangleCreator(),
                _ => throw new ArgumentException("Unknown figure")
            };
        }
    }
}
