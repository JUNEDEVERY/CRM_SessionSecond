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
    /// Логика взаимодействия для pageAbonents.xaml
    /// </summary>
    public partial class pageAbonents : Page
    {
        bool b;

        public pageAbonents()
        {
            InitializeComponent();
            b = true;
            dgSubscribers.ItemsSource = db.tbe.Subscribers.ToList();
            List<Raions> raions = db.tbe.Raions.ToList();
            cmbFilterRaions.Items.Add("Все районы");
            foreach(Raions raions1 in raions)
            {
                cmbFilterRaions.Items.Add(raions1.RaionName);
            }
            cbActive.IsChecked = true;
            cmbFIlterStreets.IsEnabled = false;
            cmbFilterNumberHome.IsEnabled = false;


        }
        void Filter()
        {
            List<Subscribers> subscribers = new List<Subscribers>();

            if (!string.IsNullOrEmpty(tbSearchPersonalAccount.Text)) 
            {
                subscribers = subscribers.Where(x => x.Contracts.PersonalAccount.ToString().ToLower().Contains(tbSearchPersonalAccount.Text.Replace(" ", "").ToLower())).ToList();
            }
            if ((bool)cbActive.IsChecked && (bool)cbNoActive.IsChecked) 
            {
                subscribers = db.tbe.Subscribers.ToList();
            }
            else if ((bool)cbActive.IsChecked && (bool)!cbNoActive.IsChecked)
            {
                subscribers = db.tbe.Subscribers.Where(x => x.Contracts.TermibationDate == null).ToList();
            }
            else if ((bool)!cbActive.IsChecked && (bool)cbNoActive.IsChecked)
            {
                subscribers = db.tbe.Subscribers.Where(x => x.Contracts.TermibationDate != null).ToList();
            }
            else
            {
                subscribers = new List<Subscribers>();
            }
            if (tbSearchSurname.Text != null && tbSearchSurname.Text != "")
                
            {
                if (!string.IsNullOrEmpty(tbSearchSurname.Text))
                {
                    subscribers = subscribers.Where(x => x.Surname.ToLower().Contains(tbSearchSurname.Text)).ToList();

                }

            }
            if (cmbFilterRaions.SelectedIndex > 0 )
            {
                Raions raions = db.tbe.Raions.FirstOrDefault(x => x.RaionName == cmbFilterRaions.SelectedValue);
                subscribers = subscribers.Where(x => x.ResidentialAddress.RaionID == raions.RaionID).ToList();

            }
            if (cmbFIlterStreets.SelectedIndex > 0) 
            {
                Streets street = db.tbe.Streets.FirstOrDefault(x => x.Street == cmbFIlterStreets.SelectedValue);
                subscribers = subscribers.Where(x => x.ResidentialAddress.StreetID == street.StreetID).ToList();
            }
            if (cmbFilterNumberHome.SelectedIndex > 0) 
            {
                subscribers = subscribers.Where(x => Convert.ToString(x.ResidentialAddress.House) == (string)cmbFilterNumberHome.SelectedValue).ToList();
            }
           

            dgSubscribers.ItemsSource = subscribers;
            if(subscribers.Count == 0 && b)
            {
                MessageBox.Show("Отсутствуют требования, удовлетворяющие результатам поиска");
            }
        }

        private void dgSubscribers_MouseDoubleClick(object sender, MouseButtonEventArgs e) // При двойном нажатие открывается страница с подробным описанием абонента
        {
            Subscribers subscriber = new Subscribers();
            foreach (Subscribers subscribers in dgSubscribers.SelectedItems)
            {
                subscriber = subscribers;
            }
            if (subscriber == null)
            {
                return;
            }
            else
            {
                NavigationService.Navigate(new pageAboutAbonents(subscriber));
            }
        }

        private void tbSearchPersonalAccount_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            if (!(Char.IsDigit(e.Text, 0)))
            {
                e.Handled = true;
            }
        }
        private void cbActive_Click(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }

        private void tbSearchSurname_SelectionChanged(object sender, RoutedEventArgs e)
        {
            b = true;
            Filter();
        }
        private void cbFilterRaion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = true;
            Filter();
            if (cmbFilterRaions.SelectedIndex > 0)
            {
                b = false;
                cmbFIlterStreets.Items.Clear();
                cmbFIlterStreets.IsEnabled = true;
                List<ResidentialAddress> residentialAddresses = db.tbe.ResidentialAddress.Where(x => x.RaionID == cmbFilterRaions.SelectedIndex).ToList();
                List<string> streets = new List<string>();
                cmbFIlterStreets.Items.Add("Все улицы");
                foreach (ResidentialAddress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.StreetID != null)
                    {
                        streets.Add(res.Streets.Street);
                    }
                }
                streets = streets.Distinct().ToList();
                foreach (string street in streets)
                {
                    cmbFIlterStreets.Items.Add(street);
                }
                cmbFIlterStreets.SelectedIndex = 0;
            }
            else
            {
                b = true;
                cmbFIlterStreets.IsEnabled = false;
                cmbFIlterStreets.Items.Clear();
            }
        }
        private void cbFilterStreet_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
            if (cmbFIlterStreets.SelectedIndex > 0)
            {
                cmbFilterNumberHome.Items.Clear();
                cmbFilterNumberHome.IsEnabled = true;
                List<ResidentialAddress> residentialAddresses = db.tbe.ResidentialAddress.Where(x => x.RaionID == cmbFilterRaions.SelectedIndex && x.StreetID == cmbFIlterStreets.SelectedIndex).ToList();
                List<string> houses = new List<string>();
                cmbFilterNumberHome.Items.Add("Все дома");
                foreach (ResidentialAddress res in residentialAddresses) // Создание списка улиц согласно району
                {
                    if (res.House != null)
                    {
                        houses.Add(Convert.ToString(res.House));
                    }
                }
                houses = houses.Distinct().ToList();
                foreach (string house in houses)
                {
                    cmbFilterNumberHome.Items.Add(house);
                }
                cmbFilterNumberHome.SelectedIndex = 0;
            }
            else
            {
                cmbFilterNumberHome.IsEnabled = false;
                cmbFilterNumberHome.Items.Clear();
            }
        }

        private void cbFiltNomerHouse_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            b = false;
            Filter();
        }

    }

}
