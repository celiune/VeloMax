using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;
using System;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour CommandesView.xaml
    /// </summary>
    public partial class CommandesView : UserControl
    {
        public CommandesView()
        {
            InitializeComponent();
            loadGril();
            loadIdCommande();

        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM commande NATURAL JOIN adresse", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            CommandeDatagrid.DataContext = dt;
        }

        public void loadIdCommande()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT max(id_commande) FROM commande", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            IdCommandeDatagrid.DataContext = dt;
        }



        private void AfficheInd(object sender, System.Windows.RoutedEventArgs e)
        {
            IndividuAffichage individuAffichage = new IndividuAffichage();
            individuAffichage.Show();
        }

        private void AfficheBTQ(object sender, System.Windows.RoutedEventArgs e)
        {
            BoutiqueAffichage boutiqueAffichage = new BoutiqueAffichage();
            boutiqueAffichage.Show();
        }

        public void clearData()
        {
            txtIdCommande.Clear();
            txtIdAdresse.Clear();
            txtIdBoutique.Clear();
            txtIdIndividu.Clear();
            txtIdPiece.Clear();
            txtIdBicyclette.Clear();
            qteIdBicyclette.Clear();
            qteIdPiece.Clear();
        }

        private void AnnulerCommande_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {

            if (txtIdCommande.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir un ID commande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dateCommande.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la date de commande", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtIdAdresse.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir une ID adresse", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void AjoutCommande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();
                    if (txtIdBoutique.Text == string.Empty)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO commande (date_commande, date_livraison, id_individu, id_adresse) VALUES (@date_commande, @date_livraison, @id_individu, @id_adresse)", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@date_commande", dateCommande.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@date_livraison", dateLivraison.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@id_individu", txtIdIndividu.Text);
                        cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresse.Text);
                        cmd.ExecuteNonQuery();
                    }
                    else if (txtIdIndividu.Text == string.Empty)
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO commande (date_commande, date_livraison, id_boutique, id_adresse) VALUES (@date_commande, @date_livraison, @id_boutique, @id_adresse)", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@date_commande", dateCommande.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@date_livraison", dateLivraison.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                        cmd.Parameters.AddWithValue("@id_boutique", txtIdBoutique.Text);
                        cmd.Parameters.AddWithValue("@id_adresse", txtIdAdresse.Text);
                        cmd.ExecuteNonQuery();
                    }
                    if (txtIdBicyclette.Text != "")
                    {
                        MySqlCommand cmd = new MySqlCommand("INSERT INTO achat_velo VALUES ( @id_bicyclette, @id_commande, @quantite_velo)", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_bicyclette", Int32.Parse(txtIdBicyclette.Text));
                        cmd.Parameters.AddWithValue("@id_commande", Int32.Parse(txtIdCommande.Text));
                        cmd.Parameters.AddWithValue("@quantite_velo", Int32.Parse(qteIdBicyclette.Text));
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand("UPDATE bicyclette SET stock_bicyclette = stock_bicyclette - @quantite_velo WHERE id_bicyclette = @id_bicyclette", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@quantite_velo", Int32.Parse(qteIdBicyclette.Text));
                        cmd.Parameters.AddWithValue("@id_bicyclette", Int32.Parse(txtIdBicyclette.Text));
                        cmd.ExecuteNonQuery();
                    }
                    if (txtIdPiece.Text != "")
                    {
                        MySqlCommand cmd = new MySqlCommand("SELECT siret FROM fournit WHERE id_piece = @id_piece", connexion);
                        cmd.Parameters.AddWithValue("@id_piece", txtIdPiece.Text);
                        MySqlDataReader reader = cmd.ExecuteReader();
                        string NumSiret = "";
                        if (reader.Read())
                        {
                            NumSiret = reader.GetValue(0).ToString();
                        }
                        reader.Close();
                        cmd = new MySqlCommand("INSERT INTO achat_piece VALUES (@id_piece, @siret, @id_commande, @quantite_piece)", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@id_piece", txtIdPiece.Text);
                        cmd.Parameters.AddWithValue("@siret", NumSiret);
                        cmd.Parameters.AddWithValue("@id_commande", txtIdCommande.Text);
                        cmd.Parameters.AddWithValue("@quantite_piece", qteIdPiece.Text);
                        cmd.ExecuteNonQuery();
                        cmd = new MySqlCommand("UPDATE piece_detachee SET stock_piece = stock_piece - @quantite_piece WHERE id_piece = @id_piece", connexion);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@quantite_piece", Int32.Parse(qteIdPiece.Text));
                        cmd.Parameters.AddWithValue("@id_piece", txtIdPiece.Text);
                        cmd.ExecuteNonQuery();
                    }
                    connexion.Close();
                    loadGril();
                    MessageBox.Show("Commande ajoutée avec succès", "Sauvegardée", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SupprimerCommande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();

                MySqlCommand cmd = new MySqlCommand("DELETE FROM commande WHERE id_commande = @id_commande ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_commande", txtIdCommande.Text);
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                loadIdCommande();
                MessageBox.Show("Commande supprimée avec succès", "Supprimée", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ModifierCommande_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                MySqlConnection connexion = new MySqlConnection(connectionString);
                connexion.Open();
                if (dateCommande.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {

                    MySqlCommand cmd = new MySqlCommand("UPDATE commande SET date_commande = @date_commande WHERE id_commande = @id_commande", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_commande", txtIdPiece.Text);
                    cmd.Parameters.AddWithValue("@date_commande", dateCommande.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();

                }

                if (dateLivraison.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {
                    MySqlCommand cmd = new MySqlCommand("UPDATE commande SET date_livraison = @date_livraison WHERE id_commande = @id_commande", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_commande", txtIdCommande.Text);
                    cmd.Parameters.AddWithValue("@date_livraison", dateLivraison.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                }
                connexion.Close();
                loadGril();
                loadIdCommande();
                clearData();
                MessageBox.Show("Boutique modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);

            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AfficheP(object sender, RoutedEventArgs e)
        {
            PieceAffichage pieceAffichage = new PieceAffichage();
            pieceAffichage.Show();

        }

        private void AfficheV(object sender, RoutedEventArgs e)
        {
            BicycletteAffichage bicycletteAffichage = new BicycletteAffichage();
            bicycletteAffichage.Show();
        }
    }
}
