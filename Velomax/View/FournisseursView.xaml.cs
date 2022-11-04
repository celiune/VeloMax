using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.IO; 

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour FournisseursView.xaml
    /// </summary>
    public partial class FournisseursView : UserControl
    {
        public FournisseursView()
        {
            InitializeComponent();
            loadGril();
            loadIdAdresse();

        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM fournisseur NATURAL JOIN adresse", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            FournisseurDatagrid.DataContext = dt;
        }

        public void loadIdAdresse()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT max(id_adresse) FROM adresse", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            IdAdresseDatagrid.DataContext = dt;
        }

        public void clearData()
        {
            txtSiret.Clear();
            txtNomFournisseur.Clear();
            txtContactFournisseur.Clear();
            txtRueFournisseur.Clear();
            txtCodePostalFournisseur.Clear();
            txtVilleFournisseur.Clear();
            txtRegionFournisseur.Clear();
            txtIdAdresseFournisseur.Clear();
        }

        private void AnnulerPiece_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {
            if (txtSiret.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le numéro de siret du fournisseur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNomFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nom du fournisseur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtContactFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le contact du fournisseur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtRueFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de rue", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCodePostalFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un code postal", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtVilleFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de ville", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtRegionFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de région", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtIdAdresseFournisseur.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID adresse du fournisseur", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void AjoutPiece_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO adresse VALUES (@id_adresse, @rue, @ville, @code_postal, @region)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueFournisseur.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleFournisseur.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalFournisseur.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionFournisseur.Text);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand("INSERT INTO fournisseur VALUES (@siret, @nom_entreprise, @contact_fournisseur, @note, @id_adresse)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@siret", txtSiret.Text);
                    cmd.Parameters.AddWithValue("@nom_entreprise", txtNomFournisseur.Text);
                    cmd.Parameters.AddWithValue("@contact_fournisseur", txtContactFournisseur.Text);
                    cmd.Parameters.AddWithValue("@note", Note.Text);
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.ExecuteNonQuery();
                    connexion.Close();
                    loadGril();
                    loadIdAdresse();
                    MessageBox.Show("Fournisseur ajoutée avec succès", "Sauvegardé", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifierPiece_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();
                if (txtNomFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE Fournisseur SET nom_entreprise = @nom_entreprise WHERE siret = @siret", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@siret", txtSiret.Text);
                    cmd.Parameters.AddWithValue("@nom_entreprise", txtNomFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtContactFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE Fournisseur SET contact_fournisseur = @contact_fournisseur WHERE siret = @siret", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@siret", txtSiret.Text);
                    cmd.Parameters.AddWithValue("@contact_fournisseur", txtContactFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }
                if (Note.Text != "1 Très bon")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE Fournisseur SET note = @note WHERE siret = @siret", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@siret", txtSiret.Text);
                    cmd.Parameters.AddWithValue("@note", Note.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtRueFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET rue = @rue WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtVilleFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET ville = @ville WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtCodePostalFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET code_postal = @code_postal WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtRegionFournisseur.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET region = @region WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionFournisseur.Text);
                    cmd.ExecuteNonQuery();
                }
                connexion.Close();
                loadGril();
                loadIdAdresse();
                clearData();
                MessageBox.Show("Fournisseur modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerPiece_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM fournisseur WHERE siret = @siret ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@siret", txtSiret.Text);
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("DELETE FROM adresse WHERE id_adresse = @id_adresse ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseFournisseur.Text);
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                loadIdAdresse();
                MessageBox.Show("Fournisseur supprimé avec succès", "Supprimé", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ExportXML_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select DISTINCT siret, nom_entreprise, contact_fournisseur, note, id_adresse from fournisseur natural join piece_detachee where stock_piece < 20;", connexion);
            connexion.Open();
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(dt);
            dataSet.WriteXml(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/XMLFournisseur.xml");
            dataSet.ReadXml(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/XMLFournisseur.xml");
            connexion.Close();
            MessageBox.Show("Export réussi");
        }

        private void ExportJSON_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select JSON_ARRAYAGG(JSON_OBJECT('siret', siret, 'nom_entreprise', nom_entreprise, 'contact_fournisseur', contact_fournisseur, 'note', note, 'id_adresse', id_adresse)) as fournisseur_json from fournisseur natural join piece_detachee where stock_piece < 20;", connexion);
            connexion.Open();
            MySqlDataReader reader  = cmd.ExecuteReader();
            string json = "";
            if (reader.Read())
            {
                json = reader.GetValue(0).ToString();

            }
            File.WriteAllText(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/JSONFournisseur.json",json);
            reader.Close();
            connexion.Close();
            MessageBox.Show("Export réussi");

        }
    }
}
