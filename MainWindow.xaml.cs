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

using MySql.Data.MySqlClient;

namespace planerAktivnosti
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MySqlConnection konekcija = null;
        bool? done = null;
        public MainWindow()
        {
            InitializeComponent();
            konekcija = new MySqlConnection("Server=localhost; Database=tmp; Username=root; Password=;");
            try
            {
                konekcija.Open();
            }
            catch
            {
                MessageBox.Show("Nije moguce uspostaviti konekciju sa bazom podataka!", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            ucitajAktivnosti();
        }

        private void lblIzlaz_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            konekcija.Close();
            Application.Current.Shutdown();
        }

        private void Label_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        public void ucitajAktivnosti()
        {
            wpSveObaveze.Children.Clear();

            string sql = "SELECT * FROM aktivnosti WHERE 1 ";
            

            if(done==true)
            {
                sql += " AND zavrseno=1";
            }
            else if (done==false)
            {
                sql += " AND zavrseno=0";
            }

            MySqlCommand komanda = new MySqlCommand(sql, konekcija);
            MySqlDataReader reader = komanda.ExecuteReader();

            while (reader.Read())
            {
                int id = (int)reader["id"];
                string naziv = reader["naziv"].ToString();
                string opis = reader["opis"].ToString();
                bool zavrsena = (bool)reader["zavrseno"];

                ucAktivnost aktivnost = new ucAktivnost(wpSveObaveze,id,naziv,opis,zavrsena,konekcija);
                wpSveObaveze.Children.Add(aktivnost);
            }
            reader.Close();
        }

        private void btnDone_Click(object sender, RoutedEventArgs e)
        {
            if (done == true)
                done = null;
            else
                done = true;
            ucitajAktivnosti();
        }

        private void btnNotDone_Click(object sender, RoutedEventArgs e)
        {
            if (done == false)
                done = null;
            else
                done = false;
            ucitajAktivnosti();
        }

        private void btnAddNew_Click(object sender, RoutedEventArgs e)
        {
            izmjenaAktivnosti novaAktivnost = new izmjenaAktivnosti();
            novaAktivnost.ShowDialog();

            if(novaAktivnost.DialogResult == true)
            {
                string novo = "INSERT INTO aktivnosti (id, naziv, opis, zavrseno) VALUES (NULL, '" + novaAktivnost.nazivAktivnosti + "', '" + novaAktivnost.opisAktivnosti + "', " + novaAktivnost.stanjeAktivnosti + ")";
                MySqlCommand komanda = new MySqlCommand(novo, konekcija);
                komanda.ExecuteNonQuery();

                MessageBox.Show("Uspjesno dodata nova aktivnost!","Info",MessageBoxButton.OK, MessageBoxImage.Asterisk);
            }
            ucitajAktivnosti();
        }
    }
}
