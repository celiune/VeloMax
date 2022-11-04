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
using System.Data;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour VelosView.xaml
    /// </summary>
    public partial class VelosView : UserControl
    {
        public VelosView()
        {
            InitializeComponent();
            loadGril();
            
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM bicyclette", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            VeloDatagrid.DataContext = dt;
        }

        public void clearData()
        {
            txtIdbicyclette.Clear();
            txtNomBicyclette.Clear();
            txtGrandeur.Clear();
            txtPrixBicyclette.Clear();
            txtLigneProduit.Clear();
            txtStockVelo.Clear();
        }
        private void AnnulerVelo_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {
            if (txtIdbicyclette.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID bicylette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNomBicyclette.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nom de la bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtGrandeur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le type de grandeur de bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPrixBicyclette.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le prix de la bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtLigneProduit.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la ligne de produit de la bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dateIntroVelo.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la date d'introduction de la bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtStockVelo.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le stock de bicyclette", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }
        private void AjoutVelo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO bicyclette VALUES (@id_bicyclette, @nom_bicyclette, @grandeur, @prix_bicyclette, @ligne_produit, @date_intro_bicyclette, @date_discontinuation_bicyclette, @stock_bicyclette)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@nom_bicyclette", txtNomBicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@grandeur", txtGrandeur.Text.ToString());
                    cmd.Parameters.AddWithValue("@prix_bicyclette", txtPrixBicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@ligne_produit", txtLigneProduit.Text.ToString());
                    cmd.Parameters.AddWithValue("@stock_bicyclette", txtStockVelo.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_intro_bicyclette", dateIntroVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@date_discontinuation_bicyclette", dateDiscontVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    connexion.Close();
                    loadGril();
                    MessageBox.Show("Bicyclette ajoutée avec succès", "Sauvegardée", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifierVelo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();
                if (txtNomBicyclette.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET nom_bicyclette = @nom_bicyclette WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@nom_bicyclette", txtNomBicyclette.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                if (txtGrandeur.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET grandeur = @grandeur WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@grandeur", txtGrandeur.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                if (txtPrixBicyclette.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET prix_bicyclette = @prix_bicyclette WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@prix_bicyclette", txtPrixBicyclette.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                if (txtLigneProduit.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET ligne_produit = @ligne_produit WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@ligne_produit", txtLigneProduit.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                if (dateIntroVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {

                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET date_intro_bicyclette = @date_intro_bicyclette WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_intro_bicyclette", dateIntroVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();

                }

                if (dateDiscontVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET date_discontinuation_bicyclette = @date_discontinuation_bicyclette WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_discontinuation_bicyclette", dateDiscontVelo.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }

                if (txtStockVelo.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE bicyclette SET stock_bicyclette = @stock_bicyclette WHERE id_bicyclette = @id_bicyclette", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                    cmd.Parameters.AddWithValue("@stock_bicyclette", txtStockVelo.Text.ToString());
                    cmd.ExecuteNonQuery();
                }
                connexion.Close();
                loadGril();
                clearData();
                MessageBox.Show("Bicyclette modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerVelo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM bicyclette WHERE id_bicyclette = @id_bicyclette ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_bicyclette", txtIdbicyclette.Text.ToString());
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                MessageBox.Show("Bicyclette supprimée avec succès", "Supprimée", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
