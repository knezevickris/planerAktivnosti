using MySql.Data.MySqlClient;
using MySql.Data.Types;
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

namespace planerAktivnosti
{
    /// <summary>
    /// Interaction logic for ucAktivnost.xaml
    /// </summary>
    public partial class ucAktivnost : UserControl
    {
        public int idAktivnosti;
        public MySqlConnection kon = null;
        bool trenutniStatus;
        WrapPanel panel;
        public ucAktivnost()
        {
            InitializeComponent();
        }

        public string nazivAktivnosti { get; set; }
        public string opisAktivnosti { get; set; }

        public ucAktivnost(WrapPanel wp, int id, string naziv, string opis, bool zavrsena, MySqlConnection konekcija)
        {
            InitializeComponent();


            nazivAktivnosti = naziv;
            opisAktivnosti = opis;

            panel = wp;
            idAktivnosti = id;
            kon = konekcija;
            lblNaslov.Content = nazivAktivnosti;
            lblPodnaslov.Content = opisAktivnosti;
            trenutniStatus = zavrsena;


            if (zavrsena == true)
                imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\true.png") as ImageSource;
            else
                imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\false.png") as ImageSource;

        }

        private void btnUkloni_Click(object sender, RoutedEventArgs e)
        {
            promjenaStatusa();
        }

        private void promjenaStatusa()
        {
            trenutniStatus = !trenutniStatus;
            string update = "UPDATE aktivnosti SET zavrseno= " + trenutniStatus + " WHERE aktivnosti.id = " + idAktivnosti;
            MySqlCommand komanda = new MySqlCommand(update, kon);
            komanda.ExecuteNonQuery();
            
            if(trenutniStatus == true)
                imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\true.png") as ImageSource;
            else
                imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\false.png") as ImageSource;

        }
        private void UserControl_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            izmjenaAktivnosti izmjena = new izmjenaAktivnosti(nazivAktivnosti, opisAktivnosti, trenutniStatus);
            izmjena.ShowDialog();

            if (izmjena.DialogResult == true)
            {
                lblNaslov.Content = izmjena.nazivAktivnosti;
                lblPodnaslov.Content = izmjena.opisAktivnosti;

                if (izmjena.stanjeAktivnosti == 0)
                {
                    imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\false.png") as ImageSource;
                }

                else if (izmjena.stanjeAktivnosti == 1)
                {
                    imbPozadnina.ImageSource = new ImageSourceConverter().ConvertFromString(@"C:\Users\x\Downloads\planerAktivnosti\Resources\true.png") as ImageSource;
                }

                string update = "UPDATE aktivnosti SET naziv='" + lblNaslov.Content + "', opis='" + lblPodnaslov.Content + "', zavrseno=" + izmjena.stanjeAktivnosti + " WHERE id=" + idAktivnosti;
                MySqlCommand komanda = new MySqlCommand(update, kon);
                komanda.ExecuteNonQuery();
            }
        }

        private void ctxObrisi_Click(object sender, RoutedEventArgs e)
        {  
            string obrisi = "DELETE FROM aktivnosti WHERE id="+idAktivnosti;
            MySqlCommand komanda = new MySqlCommand(obrisi, kon);
            komanda.ExecuteNonQuery();

            panel.Children.Remove(this);   
        }
    }
}
