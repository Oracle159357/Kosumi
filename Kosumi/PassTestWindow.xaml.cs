using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;
using Kosumi.Presentation;
using MahApps.Metro.Controls;

namespace Kosumi
{
    /// <summary>
    /// Interaction logic for PassTestWindow1.xaml
    /// </summary>
    public partial class PassTestWindow : MetroWindow
    {
        public PassTestWindow(MainViewModel main)
        {
            InitializeComponent();
            DataContext = new PassTestViewModel(main,Close,this.Dispatcher);
        }
    }
}
