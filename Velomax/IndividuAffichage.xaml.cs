using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.Windows.Input;

namespace VELOMAX
{
    /// <summary>
    /// Logique d'interaction pour IndividuAffichage.xaml
    /// </summary>
    public partial class IndividuAffichage : Window
    {
        public IndividuAffichage()
        {
            InitializeComponent();
            loadGril();
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=userID;PASSWORD=password;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM individu NATURAL JOIN adresse", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            IndividuDatagrid.DataContext = dt;
        }

        private void Fermeture_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }
    }
}
