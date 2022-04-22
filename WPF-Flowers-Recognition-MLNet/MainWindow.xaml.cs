using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace WPF_Flowers_Recognition_MLNet
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private byte[] _mainImageData;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BLoad_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Image |*.png; *.jpg; *.jpeg";
            if (ofd.ShowDialog() == true)
            {
                _mainImageData = File.ReadAllBytes(ofd.FileName);
                image.Source = (ImageSource)new ImageSourceConverter().ConvertFrom(_mainImageData);
            }
        }

        private void BReg_Click(object sender, RoutedEventArgs e)
        {
            //Load sample data
            var imageBytes = _mainImageData;
            FlowerMLModel.ModelInput sampleData = new FlowerMLModel.ModelInput()
            {
                ImageSource = imageBytes,
            };

            //Load model and predict output
            var result = FlowerMLModel.Predict(sampleData);
            TBlock.Text = String.Format("Это " + result.PredictedLabel + " с вероятностью " + result.Score.Max());
        }
    }
}
