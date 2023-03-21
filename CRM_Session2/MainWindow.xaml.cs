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

namespace CRM_Session2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            db.tbe = new Entities();
            cbFIOEmployee.ItemsSource = db.tbe.Employees.ToList();
            mainframe.fMain = fMain;
            mainframe.fMain.Navigate(new pageAbonents());
            cbFIOEmployee.SelectedValuePath = "EmployeesID";
            cbFIOEmployee.DisplayMemberPath= "FullName";

        }
        void visabiltyObjects()
        {
            imSubscriber.Visibility = Visibility.Collapsed;
            imEquipmentManagement.Visibility = Visibility.Collapsed;
            imAssets.Visibility = Visibility.Collapsed;
            imBilling.Visibility = Visibility.Collapsed;
            imUserSupport.Visibility = Visibility.Collapsed;
            imCRM.Visibility = Visibility.Collapsed;
            lbSubscriber.Visibility = Visibility.Collapsed;
            lbEquipmentManagement.Visibility = Visibility.Collapsed;
            lbAssets.Visibility = Visibility.Collapsed;
            lbBilling.Visibility = Visibility.Collapsed;
            lbUserSupport.Visibility = Visibility.Collapsed;
            lbCRM.Visibility = Visibility.Collapsed;
        }
        private void cbFIOEmployee_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Employees employee = db.tbe.Employees.FirstOrDefault(x => x.EmployeeID == cbFIOEmployee.SelectedIndex + 1);
                if (employee != null)
                {
                    imUser.ImageSource = new BitmapImage(new Uri("" + employee.Image, UriKind.Relative));

                    List<Events> events = db.tbe.Events.Where(x => x.RoleID == employee.RoleID).ToList();
                    lvEvents.ItemsSource = events.OrderBy(x => x.EventDate);
                }
                visabiltyObjects();
                foreach (AvailableModules module in db.tbe.AvailableModules.Where(x => x.RoleID == employee.RoleID).ToList())
                {
                    switch (module.Modules.ModuleName)
                    {
                        case "Абоненты":
                            imSubscriber.Visibility = Visibility.Visible;
                            lbSubscriber.Visibility = Visibility.Visible;
                            break;
                        case "CRM":
                            imEquipmentManagement.Visibility = Visibility.Visible;
                            lbEquipmentManagement.Visibility = Visibility.Visible;
                            break;
                        case "Биллинг":
                            imAssets.Visibility = Visibility.Visible;
                            lbAssets.Visibility = Visibility.Visible;
                            break;
                        case "Поддержка пользователей":
                            imBilling.Visibility = Visibility.Visible;
                            lbBilling.Visibility = Visibility.Visible;
                            break;
                        case "Управление оборудованием":
                            imUserSupport.Visibility = Visibility.Visible;
                            lbUserSupport.Visibility = Visibility.Visible;
                            break;
                        case "Активы":
                            imCRM.Visibility = Visibility.Visible;
                            lbCRM.Visibility = Visibility.Visible;
                            break;
                    }
                }
            }
            catch (Exception EX)
            {
                MessageBox.Show(EX.Message);
            }
            
        }
        private void mainFrame_Navigated(object sender, NavigationEventArgs e)
        {

        }

        private void lbSubscriber_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainframe.fMain.Navigate(new pageAbonents());
            tbHeader.Text = "Абоненты ТНС";
        } 
        private void Grid_MouseLeave(object sender, MouseEventArgs e) // после того, как уберем 
        {
            cd.Width = 100;
            spOpen.Visibility = Visibility.Collapsed;
            spClose.Visibility = Visibility.Visible;
        }
        private void lbCRM_MouseDown(object sender, MouseButtonEventArgs e)
        {
            mainframe.fMain.Navigate(new pageCRM());
            tbHeader.Text = "CRM";
        }
        public static DependencyObject GetScrollViewer(DependencyObject o)
        {
            if (o is ScrollViewer)
            { return o; }

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(o); i++)
            {
                var child = VisualTreeHelper.GetChild(o, i);

                var result = GetScrollViewer(child);
                if (result == null)
                {
                    continue;
                }
                else
                {
                    return result;
                }
            }
            return null;
        }
        private void btnUp_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var scrollViwer = GetScrollViewer(lvEvents) as ScrollViewer;
            if (scrollViwer != null)
            {
                scrollViwer.ScrollToVerticalOffset(scrollViwer.VerticalOffset - 1);
            }
        }

        private void btnDown_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var scrollViwer = GetScrollViewer(lvEvents) as ScrollViewer;
            if (scrollViwer != null)
            {
                scrollViwer.ScrollToVerticalOffset(scrollViwer.VerticalOffset + 1);
            }
        }

        private void Grid_MouseEnter(object sender, MouseEventArgs e)
        {
            cd.Width = 300;
            spOpen.Visibility = Visibility.Visible;
            spClose.Visibility = Visibility.Collapsed;
        }
    }
}
