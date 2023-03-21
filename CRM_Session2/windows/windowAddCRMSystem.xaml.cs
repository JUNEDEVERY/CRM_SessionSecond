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

namespace CRM_Session2
{
    /// <summary>
    /// Логика взаимодействия для windowAddCRMSystem.xaml
    /// </summary>
    public partial class windowAddCRMSystem : Window
    {
        public static int b;
        CRM CRM { get; set; }
        public windowAddCRMSystem(int subscriberID)
        {
            InitializeComponent();
            CRM = new CRM();
            Subscribers subscriber = db.tbe.Subscribers.FirstOrDefault(x => x.SubscriberID == subscriberID);
            CRM.SubscriberID = subscriberID; // Формирование клиента
            tbHeader.Text = tbHeader.Text + subscriber.FullName;
            CRM.NumberCRM = subscriber.Contracts.PersonalAccount + "/" + DateTime.Now.ToString("dd") + "/" + DateTime.Now.ToString("MM") + "/" + DateTime.Now.ToString("yyyy"); // Создание номера заявки
            tbNomer.Text = tbNomer.Text + CRM.NumberCRM;
            CRM.DateCreation = DateTime.Today; // Создание даты заказа
            dateOfCreation.Text = CRM.DateCreation.ToString("D");
            tbPhone.Text = subscriber.Phone;
            tbPersonalAccount.Text = subscriber.Contracts.PersonalAccount.ToString();
            cmbService.ItemsSource = db.tbe.Services.ToList(); // Заполнение списка услуг
            cmbService.SelectedValuePath = "ServicesID";
            cmbService.DisplayMemberPath = "Services1";
            cmbTypeOfService.ItemsSource = db.tbe.TypeOfServices.ToList(); // Заполнение списка вида услуг
            cmbTypeOfService.SelectedValuePath = "TypeOfServiceID";
            cmbTypeOfService.DisplayMemberPath = "TypeOfService";
            cmbStatus.ItemsSource = db.tbe.ServiceStatus.ToList(); // Заполнение списка статусов
            cmbStatus.SelectedValuePath = "ServiceStatusID";
            cmbStatus.DisplayMemberPath = "ServiceStatus1";
            cmbStatus.SelectedIndex = 0;
            cmbProblemType.ItemsSource = db.tbe.ProblemTypes.ToList(); // Заполнение списка типа проблем
            cmbProblemType.SelectedValuePath = "ProblemTypeID";
            cmbProblemType.DisplayMemberPath = "ProblemType";
            EquipmentInstallations equipmentInstallations = db.tbe.EquipmentInstallations.FirstOrDefault(x => x.SubscriberID == subscriberID); // Формирование типа оборудования клиента
            tbTypeEquipment.Text = tbTypeEquipment.Text + equipmentInstallations.Equipments.TypeEquioment.TypeEquipment;
            cmbServiceType.IsEnabled = false;

        }
        private void cmbTypeOfService_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            cmbServiceType.IsEnabled = true;
            List<KindsAndTypesService> kindsAndTypesServices = db.tbe.KindsAndTypesService.Where(x => x.TypeOfServiceID == (int)cmbTypeOfService.SelectedValue).ToList(); // Формирование списка типа услуг на основание вида
            List<ServiceTypes> serviceTypes = new List<ServiceTypes>();
            foreach (KindsAndTypesService kind in kindsAndTypesServices)
            {
                serviceTypes.Add(kind.ServiceTypes);
            }
            cmbServiceType.ItemsSource = serviceTypes;
            cmbServiceType.SelectedValuePath = "ServiceTypeID";
            cmbServiceType.DisplayMemberPath = "ServiceType";
        }

        private void btnCRM_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cmbService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"услуга\" не заполнено!");
                    return;
                }
                if (cmbTypeOfService.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"вид услуги\" не заполнено!");
                    return;
                }
                if (cmbServiceType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип услуги\" не заполнено!");
                    return;
                }
                if (cmbProblemType.SelectedIndex < 0)
                {
                    MessageBox.Show("Поле \"тип проблемы\" не заполнено!");
                    return;
                }
                CRM.ServicesID = (int)cmbService.SelectedValue;
                CRM.TypeOfServiceID = (int)cmbTypeOfService.SelectedValue;
                CRM.ServiceTypeID = (int)cmbServiceType.SelectedValue;
                CRM.ServiceStatusID = (int)cmbStatus.SelectedValue;
                CRM.ProblemTypeID = (int)cmbProblemType.SelectedValue;
                if (tbDescription.Text.Length > 0)
                {
                    CRM.Description = tbDescription.Text;
                }
                if (dpClosingDate.SelectedDate != null)
                {
                    CRM.ClosingDate = dpClosingDate.SelectedDate;
                }
               db.tbe.CRM.Add(CRM);
                db.tbe.SaveChanges();
                MessageBox.Show("Заявка успешно создана");
                this.Close();
            }
            catch
            {
                MessageBox.Show("При создание заявки клиента возникла ошибка!");
            }
        }


        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void btnCheck_Click(object sender, RoutedEventArgs e)
        {
            b = 3;
            windowCheckingEQ checkEquipment = new windowCheckingEQ();
            checkEquipment.ShowDialog();
            if (b == 1)
            {
                cmbStatus.SelectedIndex = 2;
                dpClosingDate.Text = DateTime.Today.ToString("D");
            }
            else if (b == 2)
            {
                cmbStatus.SelectedIndex = 1;
                dpClosingDate.Text = "";
            }
        }
    }
}
