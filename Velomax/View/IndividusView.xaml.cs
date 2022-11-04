using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System.IO; 



namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour IndividusView.xaml
    /// </summary>
    public partial class IndividusView : UserControl
    {
        public IndividusView()
        {
            InitializeComponent();
            loadGril();
            loadIdAdresse();

        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

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
            txtIdindividu.Clear();
            txtNomindividu.Clear();
            txtPrenomindividu.Clear();
            txtTelephoneindividu.Clear();
            txtRueindividu.Clear();
            txtCourrielindividu.Clear();
            txtCodePostalindividu.Clear();
            txtVilleindividu.Clear();
            txtRegionIndividu.Clear();
            txtIdAdresseIndividu.Clear();
        }


        public bool isValid()
        {
            if (txtIdindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNomindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nom de l'individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtPrenomindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le prénom de l'individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTelephoneindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le numéro de téléphone de l'individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCourrielindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'adresse mail de l'individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtRueindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de rue", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCodePostalindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un code postal", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtVilleindividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de ville", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtRegionIndividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de région", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtIdAdresseIndividu.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID adresse de l'individu", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void AnnulerIndividu_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        private void AjoutIndividu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO adresse VALUES (@id_adresse, @rue, @ville, @code_postal, @region)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueindividu.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleindividu.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalindividu.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionIndividu.Text);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand("INSERT INTO individu VALUES (@id_individu, @nom_individu, @prenom_individu, @tel_individu, @courriel_individu, @id_adresse)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                    cmd.Parameters.AddWithValue("@nom_individu", txtNomindividu.Text);
                    cmd.Parameters.AddWithValue("@prenom_individu", txtPrenomindividu.Text);
                    cmd.Parameters.AddWithValue("@tel_individu", txtTelephoneindividu.Text);
                    cmd.Parameters.AddWithValue("@courriel_individu", txtCourrielindividu.Text);
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.ExecuteNonQuery();
                    if (Fidelite.Text != "Aucun")
                    {
                        cmd = new MySqlCommand("INSERT INTO adhere VALUES (@id_individu, @num_program, @date_paiement_adhesion", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                        cmd.Parameters.AddWithValue("@num_program", Fidelite.Text);
                        cmd.Parameters.AddWithValue("@date_paiement_adhesion", datePaiementAdhesion.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    }
                    connexion.Close();
                    loadGril();
                    loadIdAdresse();
                    MessageBox.Show("Individu ajoutée avec succès", "Sauvegardé", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifierIndividu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();
                if (txtNomindividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE individu SET nom_individu = @nom_individu WHERE id_individu = @id_individu", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                    cmd.Parameters.AddWithValue("@nom_individu", txtNomindividu.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtPrenomindividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE individu SET prenom_individu = @prenom_individu WHERE id_individu = @id_individu", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                    cmd.Parameters.AddWithValue("@prenom_individu", txtPrenomindividu.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtTelephoneindividu.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE individu SET tel_individu = @tel_individu WHERE id_individu = @id_individu", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                    cmd.Parameters.AddWithValue("@tel_individu", txtTelephoneindividu.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtCourrielindividu.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE individu SET courriel_individu = @courriel_individu WHERE id_individu = @id_individu", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text);
                    cmd.Parameters.AddWithValue("@courriel_individu", txtCourrielindividu.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtRueindividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET rue = @rue WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueindividu.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtVilleindividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET ville = @ville WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleindividu.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtCodePostalindividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET code_postal = @code_postal WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalindividu.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtRegionIndividu.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET region = @region WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionIndividu.Text);
                    cmd.ExecuteNonQuery();
                }

                //if (Fidelite.Text != "Aucun")
                //{
                //    MySqlCommand cmd = new MySqlCommand("UPDATE adhere SET region = @region WHERE id_adresse = @id_adresse", connexion);
                //    cmd.CommandType = CommandType.Text;
                //    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text);
                //    cmd.Parameters.AddWithValue("@region", txtRegionIndividu.Text);
                //    cmd.ExecuteNonQuery();
                //}
                connexion.Close();
                loadGril();
                loadIdAdresse();
                clearData();
                MessageBox.Show("Individu modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerIndividu_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM individu WHERE id_individu = @id_individu ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_individu", txtIdindividu.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("DELETE FROM adresse WHERE id_adresse = @id_adresse ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseIndividu.Text.ToString());
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                loadIdAdresse();
                MessageBox.Show("Individu supprimée avec succès", "Supprimée", MessageBoxButton.OK, MessageBoxImage.Information);
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
            MySqlCommand cmd = new MySqlCommand("select DISTINCT id_individu, nom_individu, prenom_individu, tel_individu, courriel_individu, id_adresse, num_program from individu natural join adhere;", connexion);
            connexion.Open();
            DataSet dataSet = new DataSet();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            dataSet.Tables.Add(dt);
            dataSet.WriteXml(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/XMLIndividu.xml");
            dataSet.ReadXml(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/XMLIndividu.xml");
            connexion.Close();
            MessageBox.Show("Export réussi");
        }

        private void ExportJSON_Click(object sender, RoutedEventArgs e)
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select JSON_ARRAYAGG(JSON_OBJECT('id_individu', id_individu, 'nom_individu', nom_individu, 'prenom_individu', prenom_individu, 'tel_individu', tel_individu, 'courriel_individu', courriel_individu, 'id_adresse', id_adresse, 'num_program', num_program)) as individu_json from individu natural join adhere ;", connexion);
            connexion.Open();
            MySqlDataReader reader = cmd.ExecuteReader();
            string json = "";
            if (reader.Read())
            {
                json = reader.GetValue(0).ToString();

            }
            File.WriteAllText(@"C:/Users/celin/source/repos/VELOMAX/VELOMAX/JSONIndividu.json", json);
            reader.Close();
            connexion.Close();
            MessageBox.Show("Export réussi");
        }
    }
}
