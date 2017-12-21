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
    /// Interaction logic for QuestionWindow.xaml
    /// </summary>
    public partial class QuestionWindow : MetroWindow
    {
        public QuestionWindow(MainViewModel main,bool additing)
        {
            
            InitializeComponent();
            DataContext = new QuestionViewModel(main,this.Close, additing);

        }
    }
}
