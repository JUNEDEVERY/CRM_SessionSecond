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
    /// Логика взаимодействия для pageCRM.xaml
    /// </summary>
    public partial class pageCRM : Page
    {
        public pageCRM()
        {
            InitializeComponent();
            value();

        }
        private void btnAddCRM_Click(object sender, RoutedEventArgs e)
        {
            if (cmbSubscriber.SelectedItem != null)
            {
                windowAddCRMSystem windowAddCRMSystem = new windowAddCRMSystem((int)cmbSubscriber.SelectedValue);
                windowAddCRMSystem.ShowDialog();
            }
            else
            {
                MessageBox.Show("Пользователь не выбран!");
            }
        }
        private void tbPhone_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0) || (e.Text == "(") || (e.Text == ")") || (e.Text == "+") || (e.Text == "-")))
            {
                e.Handled = true;
            }
        }
        private void tbPhone_TextChanged(object sender, TextChangedEventArgs e)
        {
            value();
        }

        private void tbSurname_TextChanged(object sender, TextChangedEventArgs e)
        {
            value();
        }
        void value()
        {
            List<Subscribers> subscribers = db.tbe.Subscribers.ToList();
            if (tbPhone.Text.Length > 0)
            {
                subscribers = subscribers.Where(x => x.Phone.ToLower().Contains(tbPhone.Text.ToLower())).ToList();
            }
            if (tbSurname.Text.Length > 0)
            {
                subscribers = subscribers.Where(x => x.Surname.ToLower().Contains(tbSurname.Text.ToLower())).ToList();
            }
            cmbSubscriber.ItemsSource = subscribers;
            cmbSubscriber.SelectedValuePath = "SubscriberID";
            cmbSubscriber.DisplayMemberPath = "FullName";
        }
    }
}
