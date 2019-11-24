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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using myoddweb.directorywatcher;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Watcher watcher;
        public MainWindow()
        {
            InitializeComponent();

            watcher = new Watcher();
            watcher.Add(new Request("c:\\", true));


            watcher.OnAddedAsync += async (f, t) =>
            {
                Application.Current.Dispatcher.BeginInvoke(
                  DispatcherPriority.Background,
                  new Action(() => {
                      ListBoxItem item = new ListBoxItem();
                      item.Content = f.FileSystemInfo.FullName;
                      LB.Items.Add(item);
                  }));
                
            };

            watcher.Start();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            watcher.Stop();
        }
    }
}
