using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using WPFModernVerticalMenu.Models;
using WPFModernVerticalMenu.Services;

namespace WPFModernVerticalMenu.Pages
{
    public partial class ServiceRequestStatus : Page
    {
        private TreeManager treeManager;
        private const double NodeDiameter = 40;
        private const double LevelHeight = 80;
        private const double HorizontalSpacing = 60;

        public ServiceRequestStatus()
        {
            InitializeComponent();
            treeManager = ReportIssues.GetTreeManager();
            DrawTree();
        }

        private void DrawTree()
        {
            TreeCanvas.Children.Clear();
            if (treeManager.Root != null)
            {
                // Calculate initial position for root node
                double canvasWidth = TreeCanvas.ActualWidth > 0 ? TreeCanvas.ActualWidth : 800;
                double startX = canvasWidth / 2;
                double startY = 40;

                DrawNode(treeManager.Root, startX, startY, canvasWidth);
            }
        }

        private void DrawNode(TreeNode node, double x, double y, double availableWidth)
        {
            // Draw the circle
            Ellipse circle = new Ellipse
            {
                Style = (Style)TreeCanvas.Resources["NodeCircle"]
            };

            // Position the circle
            Canvas.SetLeft(circle, x - NodeDiameter / 2);
            Canvas.SetTop(circle, y - NodeDiameter / 2);
            TreeCanvas.Children.Add(circle);

            // Draw the text
            TextBlock text = new TextBlock
            {
                Text = node.Name,                
                Style = (Style)TreeCanvas.Resources["NodeText"]
                
            };

            // Center the text in the circle
            text.Measure(new Size(Double.PositiveInfinity, Double.PositiveInfinity));
            Canvas.SetLeft(text, x - text.DesiredSize.Width / 2);
            Canvas.SetTop(text, y - text.DesiredSize.Height / 2);
            TreeCanvas.Children.Add(text);

            if (node.Children.Count > 0)
            {
                double childrenWidth = availableWidth / node.Children.Count;
                double startX = x - (availableWidth / 2) + (childrenWidth / 2);

                for (int i = 0; i < node.Children.Count; i++)
                {
                    double childX = startX + (i * childrenWidth);
                    double childY = y + LevelHeight;

                    // Draw line to child
                    Line line = new Line
                    {
                        X1 = x,
                        Y1 = y + NodeDiameter / 2,
                        X2 = childX,
                        Y2 = childY - NodeDiameter / 2,
                        Stroke = Brushes.DarkRed,
                        StrokeThickness = 2
                    };
                    TreeCanvas.Children.Add(line);

                    // Draw child node
                    DrawNode(node.Children[i], childX, childY, childrenWidth);
                }
            }
        }

        private void RefreshStatus_Click(object sender, RoutedEventArgs e)
        {
            DrawTree(); // Redraw the tree
            MessageBox.Show("Status Refreshed", "Refresh", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // Handle window resize to redraw the tree
        protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
        {
            base.OnRenderSizeChanged(sizeInfo);
            DrawTree();
        }

        private void ViewHeaps_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Settings());
        }
    }
}