using System.Windows;
using Kosumi.Presentation;
using MahApps.Metro.Controls;

namespace Kosumi
{
    /// <summary>
    /// Interaction logic for TestModelWindow.xaml
    /// </summary>
    public partial class TestWindow : MetroWindow
    {
        public TestWindow(MainViewModel main,bool additing)
        {
            InitializeComponent(); 
            DataContext = new TestViewModel(main, Close,additing);         
        }
    }
}
