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

namespace planerAktivnosti
{
    /// <summary>
    /// Interaction logic for izmjenaAktivnosti.xaml
    /// </summary>
    public partial class izmjenaAktivnosti : Window
    {
        public string nazivAktivnosti;
        public string opisAktivnosti;
        public int stanjeAktivnosti;
        public izmjenaAktivnosti()
        {
            InitializeComponent();
            
        }

        public izmjenaAktivnosti(string naziv, string opis, bool status)
        {
            InitializeComponent();

            txtNaziv.Text = naziv;
            txtDetalji.Text = opis;

            if (status == true)
                rdbUradjeno.IsChecked = true;
            else
                rdbNovo.IsChecked = true;
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            if (txtNaziv.Text != "" && txtDetalji.Text != "")
            {
                nazivAktivnosti = txtNaziv.Text;
                opisAktivnosti = txtDetalji.Text;
                if (rdbNovo.IsChecked == true)
                {
                    stanjeAktivnosti = 0;
                }
                else if (rdbUradjeno.IsChecked == true)
                {
                    stanjeAktivnosti = 1;
                }
                this.DialogResult = true;
            }
            else
            {
                MessageBox.Show("Neophodno je popuniti sva polja za unos!");
            }
        }

        private void btnOdustani_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
