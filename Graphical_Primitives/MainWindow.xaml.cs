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
        private List<Shape> figures = new List<Shape>();
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CanvaMouseMove(object sender, MouseEventArgs e)
        {
            Point position = e.GetPosition(FigureCanva);
            MousePositionText.Text = $"X: {position.X:0}, Y: {position.Y:0}";
        }
        private void Figure_Checked(object sender, RoutedEventArgs e)
        {

            X2Position.Visibility = Visibility.Collapsed;
            Y2Position.Visibility = Visibility.Collapsed;
            X3Position.Visibility = Visibility.Collapsed;
            Y3Position.Visibility = Visibility.Collapsed;
            RLength.Visibility = Visibility.Collapsed;

            if (!firstSelection)
            {
                firstSelection = true;
                X1Position.Visibility = Visibility.Visible;
                Y1Position.Visibility = Visibility.Visible;
                Generate.Visibility = Visibility.Visible;
            }
            
            if (Square.IsChecked == true || Line.IsChecked == true)
            {
                X2Position.Visibility = Visibility.Visible;
                Y2Position.Visibility = Visibility.Visible;
            }
            else if (Triangle.IsChecked == true)
            {
                X2Position.Visibility = Visibility.Visible;
                Y2Position.Visibility = Visibility.Visible;
                X3Position.Visibility = Visibility.Visible;
                Y3Position.Visibility = Visibility.Visible;
            }
            else if (Circle.IsChecked == true)
            {
                RLength.Visibility = Visibility.Visible;
            }
        }
        private void Figure_Keyboard(object sender, RoutedEventArgs e)
        {
            if (Line.IsChecked == true)
            {
                Line line = new Line
                {
                    X1 = Convert.ToInt32(x1Value.Text),
                    Y1 = Convert.ToInt32(y1Value.Text),
                    X2 = Convert.ToInt32(x2Value.Text),
                    Y2 = Convert.ToInt32(y2Value.Text),
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                FigureCanva.Children.Add(line);
                figures.Add(line);
            }
            else if (Square.IsChecked == true)
            {

                var SquareWidth = Math.Abs(Convert.ToInt32(x2Value.Text) - Convert.ToInt32(x1Value.Text));
                var SquareHeight = Math.Abs(Convert.ToInt32(y2Value.Text) - Convert.ToInt32(y1Value.Text));
                var SquareTop = Math.Min(Convert.ToInt32(x2Value.Text), Convert.ToInt32(x1Value.Text));
                var SquareLeft = Math.Min(Convert.ToInt32(y2Value.Text), Convert.ToInt32(y1Value.Text));
                Rectangle square = new Rectangle
                {
                    Width = SquareWidth,
                    Height = SquareHeight,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                Canvas.SetLeft(square, SquareTop);
                Canvas.SetTop(square, SquareLeft);
                FigureCanva.Children.Add(square);
                figures.Add(square);

            }
            else if (Triangle.IsChecked == true)
            {
                Polygon triangle = new Polygon
                {
                    Points = new PointCollection
                    {
                        new Point(Convert.ToInt32(x1Value.Text),Convert.ToInt32(y1Value.Text)),
                        new Point(Convert.ToInt32(x2Value.Text),Convert.ToInt32(y2Value.Text)),
                        new Point(Convert.ToInt32(x3Value.Text),Convert.ToInt32(y3Value.Text)),
                    },
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                FigureCanva.Children.Add(triangle);
                figures.Add(triangle);
            }
            else if (Circle.IsChecked == true)
            {
                Ellipse circle = new Ellipse
                {
                    Width = Convert.ToInt32(rValue.Text) * 2,
                    Height = Convert.ToInt32(rValue.Text) * 2,
                    Stroke = Brushes.Black,
                    StrokeThickness = 2,
                };
                Canvas.SetLeft(circle, Convert.ToInt32(x1Value.Text) - Convert.ToInt32(rValue.Text));
                Canvas.SetTop(circle, Convert.ToInt32(y1Value.Text) - Convert.ToInt32(rValue.Text));
                FigureCanva.Children.Add(circle);
                figures.Add(circle);
            }
        }

    }
}