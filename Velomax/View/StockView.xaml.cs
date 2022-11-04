using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour StockView.xaml
    /// </summary>
    public partial class StockView : UserControl
    {
        public StockView()
        {
            InitializeComponent();
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void StockParPiece()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_piece,stock_piece from piece_detachee;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        public void StockParFournisseur()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_piece,stock_piece,siret from piece_detachee natural join fournit order by id_piece;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        public void StockParVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_bicyclette, sum(stock_bicyclette) from bicyclette group by id_bicyclette;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        public void StockParCategorieVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select ligne_produit,sum(stock_bicyclette) from bicyclette group by ligne_produit;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        public void StockParMarqueVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select nom_bicyclette, sum(stock_bicyclette) from bicyclette group by nom_bicyclette;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        public void StockParDescriptionPiece()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select description, sum(stock_piece) from piece_detachee group by description;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StockDatagrid.DataContext = dt;
        }
        private void PieceBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParPiece();
        }

        private void FournisseurBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParFournisseur();
        }

        private void VeloBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParVelo();
        }

        private void LigneProduitBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParCategorieVelo();
        }

        private void NomVeloBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParMarqueVelo();
        }

        private void DesccriptionBTN_Click(object sender, RoutedEventArgs e)
        {
            StockParDescriptionPiece();
        }
    
    }

}
