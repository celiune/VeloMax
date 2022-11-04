﻿using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour BoutiquesView.xaml
    /// </summary>
    public partial class BoutiquesView : UserControl
    {
        public BoutiquesView()
        {
            InitializeComponent();
            loadGril();
            loadIdAdresse();
        }
        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM boutique NATURAL JOIN adresse", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            BoutiqueDatagrid.DataContext = dt;
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
            txtIdBoutique.Clear();
            txtNomContact.Clear();
            txtRemise.Clear();
            txtTelephone.Clear();
            txtCourriel.Clear();
            txtIdAdresseBTQ.Clear();
            txtRegionBTQ.Clear();
            txtVilleBTQ.Clear();
            txtCodePostalBTQ.Clear();
            txtRueBTQ.Clear();
        }

        public bool isValid()
        {
            if(txtIdBoutique.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID boutique", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNomContact.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nom du contact boutique", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtTelephone.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le numéro de téléphone de la boutique", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCourriel.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'adresse mail de la boutique", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (txtRueBTQ.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de rue", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtCodePostalBTQ.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un code postal", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtVilleBTQ.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de ville", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtRegionBTQ.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un nom de région", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            
            if (txtIdAdresseBTQ.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID adresse de la boutique", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (txtRemise.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir si il y a une remise", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void AnnulerBTQ_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clearData();
        }

        private void AjoutBTQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();
                    MySqlCommand cmd = new MySqlCommand("INSERT INTO adresse VALUES (@id_adresse, @rue, @ville, @code_postal, @region)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueBTQ.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleBTQ.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalBTQ.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionBTQ.Text);
                    cmd.ExecuteNonQuery();
                    cmd = new MySqlCommand("INSERT INTO boutique VALUES (@id_boutique, @nom_contact_boutique, @remise, @tel_boutique, @courriel_boutique, @id_adresse)", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                    cmd.Parameters.AddWithValue("@nom_contact_boutique", txtNomContact.Text);
                    cmd.Parameters.AddWithValue("@remise", txtRemise.Text);
                    cmd.Parameters.AddWithValue("@tel_boutique", txtTelephone.Text);
                    cmd.Parameters.AddWithValue("@courriel_boutique", txtCourriel.Text);
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.ExecuteNonQuery();
                    connexion.Close();
                    loadGril();
                    loadIdAdresse();
                    MessageBox.Show("Boutique ajoutée avec succès", "Sauvegardé", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifierBTQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();
                if (txtNomContact.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE boutique SET nom_contact_boutique = @nom_contact_boutique WHERE id_boutique = @id_boutique", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                    cmd.Parameters.AddWithValue("@nom_contact_boutique", txtNomContact.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtRemise.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE boutique SET remise = @remise WHERE id_boutique = @id_boutique", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                    cmd.Parameters.AddWithValue("@remise", txtRemise.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtTelephone.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE boutique SET tel_boutique = @tel_boutique WHERE id_boutique = @id_boutique", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                    cmd.Parameters.AddWithValue("@tel_boutique", txtTelephone.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtCourriel.Text.ToString() != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE boutique SET courriel_boutique = @courriel_boutique WHERE id_boutique = @id_boutique", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                    cmd.Parameters.AddWithValue("@courriel_boutique", txtCourriel.Text);
                    cmd.ExecuteNonQuery();
                }
                if (txtRueBTQ.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET rue = @rue WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.Parameters.AddWithValue("@rue", txtRueBTQ.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtVilleBTQ.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET ville = @ville WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.Parameters.AddWithValue("@ville", txtVilleBTQ.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtCodePostalBTQ.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET code_postal = @code_postal WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.Parameters.AddWithValue("@code_postal", txtCodePostalBTQ.Text);
                    cmd.ExecuteNonQuery();
                }

                if (txtRegionBTQ.Text != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE adresse SET region = @region WHERE id_adresse = @id_adresse", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text);
                    cmd.Parameters.AddWithValue("@region", txtRegionBTQ.Text);
                    cmd.ExecuteNonQuery();
                }
                connexion.Close();
                loadGril();
                loadIdAdresse();
                clearData();
                MessageBox.Show("Boutique modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerBTQ_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM boutique WHERE id_boutique = @id_boutique ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text.ToString());
                cmd.ExecuteNonQuery();
                cmd = new MySqlCommand("DELETE FROM adresse WHERE id_adresse = @id_adresse ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresseBTQ.Text.ToString());
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                loadIdAdresse();
                MessageBox.Show("Boutique supprimée avec succès", "Supprimée", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
