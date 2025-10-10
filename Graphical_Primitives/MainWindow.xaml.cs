using Graphical_Primitives.Services;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Graphical_Primitives
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private bool firstSelection = false;
        private List<Point> points = new List<Point>();
        private List<Shape> figures = new List<Shape>();
        private Shape? selectedShape = null;
        private Point lastMousePosition;
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CanvaMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(FigureCanva);
            MousePositionText.Text = $"X: {position.X:0}, Y: {position.Y:0}";

            if (selectedShape == null || e.LeftButton != MouseButtonState.Pressed)
                return;

            Point currentPoint = e.GetPosition(FigureCanva);
            double offsetX = currentPoint.X - lastMousePosition.X;
            double offsetY = currentPoint.Y - lastMousePosition.Y;

            if (selectedShape is Line line)
            {
                line.X1 += offsetX;
                line.X2 += offsetX;
                line.Y1 += offsetY;
                line.Y2 += offsetY;
            }
            else if (selectedShape is Polygon polygon)
            {
                for (int i = 0; i < polygon.Points.Count; i++)
                {
                    polygon.Points[i] = new Point(
                        polygon.Points[i].X + offsetX,
                        polygon.Points[i].Y + offsetY
                    );
                }
            }
            else
            {
                double left = Canvas.GetLeft(selectedShape);
                double top = Canvas.GetTop(selectedShape);

                if (double.IsNaN(left)) left = 0;
                if (double.IsNaN(top)) top = 0;

                Canvas.SetLeft(selectedShape, left + offsetX);
                Canvas.SetTop(selectedShape, top + offsetY);
            }

            lastMousePosition = currentPoint;
        }
        private void AddFigure(string type, List<Point>? points = null)
        {
            var creator = ShapeFactory.GetCreator(type);
            var inputs = new Dictionary<string, string>
            {
                ["x1"] = x1Value.Text,
                ["y1"] = y1Value.Text,
                ["x2"] = x2Value.Text,
                ["y2"] = y2Value.Text,
                ["x3"] = y3Value?.Text ?? "0",
                ["y3"] = y3Value?.Text ?? "0",
                ["r"] = rValue?.Text ?? "0"
            };

            var shape = creator.CreateShape(points ?? new List<Point>(), inputs);
            FigureCanva.Children.Add(shape);
            figures.Add(shape);
        }
        private void Figure_Checked(object sender, RoutedEventArgs e)
        {

            X2Position.Visibility = Visibility.Collapsed;
            Y2Position.Visibility = Visibility.Collapsed;
            X3Position.Visibility = Visibility.Collapsed;
            Y3Position.Visibility = Visibility.Collapsed;
            RLength.Visibility = Visibility.Collapsed;
            CheckGenerate();

            if (!firstSelection)
            {
                firstSelection = true;
                X1Position.Visibility = Visibility.Visible;
                Y1Position.Visibility = Visibility.Visible;
                Generate.Visibility = Visibility.Visible;
            }
            if (Square.IsChecked == true || Line.IsChecked == true || Triangle.IsChecked == true)
            {
                X2Position.Visibility = Visibility.Visible;
                Y2Position.Visibility = Visibility.Visible;
                
                if (Triangle.IsChecked == true)
                {
                    X3Position.Visibility = Visibility.Visible;
                    Y3Position.Visibility = Visibility.Visible;
                }
            }
            else if (Circle.IsChecked == true)
            {
                RLength.Visibility = Visibility.Visible;
            }
        }
        private bool CheckGenerate()
        {
            var allFilled = new[] { x1Value, y1Value }.All(tb =>
                !string.IsNullOrWhiteSpace(tb.Text) &&
                int.TryParse(tb.Text, out _)
            );
            var allFilledAdditional = false;

            if (Square.IsChecked == true || Line.IsChecked == true || Triangle.IsChecked == true)
            {
                allFilledAdditional = new[] { x2Value, y2Value }.All(tb =>
                !string.IsNullOrWhiteSpace(tb.Text) &&
                int.TryParse(tb.Text, out _)
                );

                if (Triangle.IsChecked == true)
                {
                    allFilledAdditional = new[] { x3Value, y3Value }.All(tb =>
                    !string.IsNullOrWhiteSpace(tb.Text) &&
                    int.TryParse(tb.Text, out _)
                    );
                }
            }
            else if (Circle.IsChecked == true)
            {
                allFilledAdditional = new[] { rValue }.All(tb =>
                !string.IsNullOrWhiteSpace(tb.Text) &&
                int.TryParse(tb.Text, out _)
                );
            }
            return allFilled && allFilledAdditional;
        }
        private void RequiredValue(object sender, TextChangedEventArgs e)
        {
            Generate.IsEnabled = CheckGenerate();
        }
        private void Figure_Keyboard(object sender, RoutedEventArgs e)
        {
            if (Line.IsChecked == true ) AddFigure("Line");
            else if (Square.IsChecked == true) AddFigure("Square");
            else if (Triangle.IsChecked == true) AddFigure("Triangle");
            else if (Circle.IsChecked == true) AddFigure("Circle");
        }
        private void CanvaMouseDown(object sender, MouseButtonEventArgs e)
        {
            if (firstSelection)
            {
                Point clickPoint = e.GetPosition(FigureCanva);
                selectedShape = FigureCanva.InputHitTest(clickPoint) as Shape;

                if (selectedShape != null)
                {
                    if (Painting.IsChecked == true)
                    {

                    }
                    selectedShape.Stroke = Brushes.Red;
                    lastMousePosition = clickPoint;
                    FigureCanva.CaptureMouse();
                }
                else
                {
                    points.Add(e.GetPosition(FigureCanva));
                    Generate.IsEnabled = false;
                    if (Line.IsChecked == true && points.Count == 2)
                    { 
                        AddFigure("Line", points); 
                        points.Clear(); 
                        Generate.IsEnabled = CheckGenerate();
                    }
                    else if (Square.IsChecked == true && points.Count == 2)
                    {
                        AddFigure("Square", points);
                        points.Clear();
                        Generate.IsEnabled = CheckGenerate();
                    }
                    else if (Circle.IsChecked == true && points.Count == 2)
                    {
                        AddFigure("Circle", points); 
                        points.Clear();
                        Generate.IsEnabled = CheckGenerate();
                    }
                    else if (Triangle.IsChecked == true && points.Count == 3)
                    { 
                        AddFigure("Triangle", points);
                        points.Clear();
                        Generate.IsEnabled = CheckGenerate();
                    }
                }
            }
        }
        private void CanvaMouseUp(object sender, MouseButtonEventArgs e)
        {
            {
                if(selectedShape != null) 
                {
                    selectedShape.Stroke = Brushes.Black;
                    selectedShape = null;
                    FigureCanva.ReleaseMouseCapture();
                }
            }
        }
    }
}