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

namespace VELOMAX
{
    /// <summary>
    /// Logique d'interaction pour Connexion.xaml
    /// </summary>
    ///

    public partial class Connexion : Window
    {
        public Connexion()
        {
            InitializeComponent();
        }

        private void exitApp(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void LogInApp(object sender, RoutedEventArgs e)
        {
            if (txtUsername.Text == "root" & txtPassword.Password == "root")
            {
                HomePage HM = new HomePage();
                HM.Show();
                this.Close();
            }
            else if (txtUsername.Text == "bozo" & txtPassword.Password == "bozo")
            {
                AffichageVeloMax AV = new AffichageVeloMax();
                AV.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Identifiant ou mot de passe incorrect");
            }
        }
    }
}
