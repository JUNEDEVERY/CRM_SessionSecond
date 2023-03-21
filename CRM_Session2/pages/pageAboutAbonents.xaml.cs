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
    /// Логика взаимодействия для pageAboutAbonents.xaml
    /// </summary>
    public partial class pageAboutAbonents : Page
    {
        Subscribers subscriber;
        public pageAboutAbonents(Subscribers subscriber)
        {
            InitializeComponent();
            this.subscriber = subscriber;
            tbSubscriberNomer.Text = tbSubscriberNomer.Text + subscriber.SubscriberNomer; 
            tbSurname.Text = tbSurname.Text + subscriber.Surname;
            tbName.Text = tbName.Text + subscriber.Name;
            tbPatronymic.Text = tbPatronymic.Text + subscriber.Patronymic;
            tbPlace0fResidence.Text = tbPlace0fResidence.Text + subscriber.Place0fResidence;
            tbResidentialAddress.Text = tbResidentialAddress.Text + subscriber.ResidentialAddress.Raions.RaionName + ", " + subscriber.ResidentialAddress.Citys.City + " " + subscriber.ResidentialAddress.Streets.Street + " " + subscriber.ResidentialAddress.House;
            tbSeria.Text = tbSeria.Text + subscriber.Seria;
            tbNomer.Text = tbNomer.Text + subscriber.Nomer;
            tbDateOfIssue.Text = tbDateOfIssue.Text + subscriber.DateOfIssue.ToString("d");
            tbIssuedBy.Text = tbIssuedBy.Text + subscriber.IssuedBy;
            tbContractNumber.Text = tbContractNumber.Text + subscriber.Contracts.ContractNumber; 
            tbDateOfCinclusion.Text = tbDateOfCinclusion.Text + subscriber.Contracts.DateOfCinclusion.ToString("d");
            tbTypeContract.Text = tbTypeContract.Text + subscriber.Contracts.TypeContracts.TypeContract;
            tbPersonalAccount.Text = tbPersonalAccount.Text + subscriber.Contracts.PersonalAccount;
            if (subscriber.Contracts.TermibationDate != null) 
            {
                tbTermibationDate.Text = tbTermibationDate.Text + Convert.ToDateTime(subscriber.Contracts.TermibationDate).ToString("d");
                tbReasonForTermination.Text = tbReasonForTermination.Text + subscriber.Contracts.ReasonForTerminations.ReasonForTermination;
            }
            else
            {
                tbTermibationDate.Text = "";
                tbTermibationDate.Visibility = Visibility.Collapsed;
                tbReasonForTermination.Text = "";
                tbReasonForTermination.Visibility = Visibility.Collapsed;
            }
            List<ConnectedServices> connectedServices = db.tbe.ConnectedServices.Where(x => x.SubscribersID == subscriber.SubscriberID).ToList(); 
            for (int i = 0; i < connectedServices.Count; i++)
            {
                if (i == connectedServices.Count - 1)
                {
                    if (connectedServices[i].ConnectionDate != null) 
                    {
                        listService.Text = listService.Text + connectedServices[i].Services.Services1 + " Дата подключения: " + Convert.ToDateTime(connectedServices[i].ConnectionDate).ToString("d");
                    }
                    else
                    {
                        listService.Text = listService.Text + connectedServices[i].Services.Services1;
                    }
                }
                else
                {
                    if (connectedServices[i].ConnectionDate != null) 
                    {
                        listService.Text = listService.Text + connectedServices[i].Services.Services1 + " Дата подключения: " + Convert.ToDateTime(connectedServices[i].ConnectionDate).ToString("d") + "\n";
                    }
                    else
                    {
                        listService.Text = listService.Text + connectedServices[i].Services.Services1 + "\n";
                    }
                }
            }
            List<EquipmentInstallations> equipmentInstallations = db.tbe.EquipmentInstallations.Where(x => x.SubscriberID == subscriber.SubscriberID).ToList();
            for (int i = 0; i < equipmentInstallations.Count; i++) 
            {
                if (i == connectedServices.Count - 1) 
                {
                    if (equipmentInstallations[i].Rental)
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Equipments.EquiomentName + " " + equipmentInstallations[i].Notes;
                    }
                    else
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Equipments.EquiomentName;
                    }
                }
                else
                {
                    if (equipmentInstallations[i].Rental) 
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Equipments.EquiomentName + " " + equipmentInstallations[i].Notes + "\n";
                    }
                    else
                    {
                        listEquipments.Text = listEquipments.Text + equipmentInstallations[i].Equipments.EquiomentName + "\n";
                    }
                }
            }
            DateTime dateTime = DateTime.Now.AddMonths(-12); 
            List<CRM> cRMs = db.tbe.CRM.Where(x => x.SubscriberID == subscriber.SubscriberID && x.DateCreation >= dateTime).ToList();
            for (int i = 0; i < cRMs.Count; i++) 
            {
                if (i == cRMs.Count - 1) 
                {
                    listCRM.Text = listCRM.Text + "Номер заявки " + cRMs[i].NumberCRM + "\n";
                    listCRM.Text = listCRM.Text + "Дата создания: " + cRMs[i].DateCreation.ToString("d") + "\n";
                    if (cRMs[i].ClosingDate != null)
                    {
                        listCRM.Text = listCRM.Text + "Дата закрытия: " + Convert.ToDateTime(cRMs[i].ClosingDate).ToString("d") + "\n";
                    }
                    listCRM.Text = listCRM.Text + "Услуга: " + cRMs[i].Services.Services1 + "\n";
                    listCRM.Text = listCRM.Text + "Вид услуги: " + cRMs[i].TypeOfServices.TypeOfService + "\n";
                    listCRM.Text = listCRM.Text + "Тип услуги: " + cRMs[i].ServiceTypes.ServiceType + "\n";
                    listCRM.Text = listCRM.Text + "Описание проблемы: " + cRMs[i].Description;
                }
                else
                {
                    listCRM.Text = listCRM.Text + "Номер заявки " + cRMs[i].NumberCRM + "\n";
                    listCRM.Text = listCRM.Text + "Дата создания: " + cRMs[i].DateCreation + "\n";
                    if (cRMs[i].ClosingDate != null)
                    {
                        listCRM.Text = listCRM.Text + "Дата закрытия: " + cRMs[i].ClosingDate + "\n";
                    }
                    listCRM.Text = listCRM.Text + "Услуга: " + cRMs[i].Services.Services1 + "\n";
                    listCRM.Text = listCRM.Text + "Вид услуги: " + cRMs[i].TypeOfServices.TypeOfService + "\n";
                    listCRM.Text = listCRM.Text + "Тип услуги: " + cRMs[i].ServiceTypes.ServiceType + "\n";
                    listCRM.Text = listCRM.Text + "Описание проблемы: " + cRMs[i].Description + "\n\n";
                }
            }
        }
        private void btnBack_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }
    }
}
