using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour PiecesView.xaml
    /// </summary>
    public partial class PiecesView : UserControl
    {
        public PiecesView()
        {
            InitializeComponent();
            loadGril();
            
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        public void loadGril()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("SELECT * FROM piece_detachee", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            PieceDatagrid.DataContext = dt;
        }

        public void clearData()
        {
            txtIdpiece.Clear();
            txtStockPiece.Clear();
            txtDescription.Clear();

        }

        private void AnnulerPiece_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {
            if (txtIdpiece.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir l'ID pièce", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la description de la pièce", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (dateIntroPiece.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir la date d'introduction de la pièce", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtStockPiece.Text == string.Empty)
            {
                MessageBox.Show("Veuillez saisir le nombre de pièces", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private void AjoutPiece_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    MySqlConnection connexion = new MySqlConnection(connectionString);
                    connexion.Open();

                    MySqlCommand cmd = new MySqlCommand("INSERT INTO piece_detachee VALUES (@id_piece, @description, @date_intro_piece, @date_discontinuation_piece, @stock_piece)",connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_piece", txtIdpiece.Text.ToString());
                    cmd.Parameters.AddWithValue("@description", txtDescription.Text.ToString());
                    cmd.Parameters.AddWithValue("@stock_piece", txtStockPiece.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_intro_piece", dateIntroPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.Parameters.AddWithValue("@date_discontinuation_piece", dateDiscontPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));             
                    cmd.ExecuteNonQuery();
                    connexion.Close();
                    loadGril();
                    MessageBox.Show("Pièce ajoutée avec succès", "Sauvegardé", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();

                }
            }
            catch(MySqlException ex)
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

                MySqlCommand cmd = new MySqlCommand("DELETE FROM piece_detachee WHERE id_piece = @id_piece ", connexion);
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@id_piece", txtIdpiece.Text.ToString());                    
                cmd.ExecuteNonQuery();
                connexion.Close();
                loadGril();
                MessageBox.Show("Pièce supprimée avec succès", "Supprimée", MessageBoxButton.OK, MessageBoxImage.Information);
                clearData();
                
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

                if (dateIntroPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {
                    
                    MySqlCommand cmd = new MySqlCommand("UPDATE piece_detachee SET date_intro_piece = @date_intro_piece WHERE id_piece = @id_piece", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_piece", txtIdpiece.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_intro_piece", dateIntroPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    //connexion.Close();
                    
                }

                if (dateDiscontPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss") != "")
                {
                    //MySqlConnection connexion = new MySqlConnection(connectionString);
                    //connexion.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE piece_detachee SET date_discontinuation_piece = @date_discontinuation_piece WHERE id_piece = @id_piece", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_piece", txtIdpiece.Text.ToString());
                    cmd.Parameters.AddWithValue("@date_discontinuation_piece", dateDiscontPiece.SelectedDate.Value.ToString("yyyy-MM-dd HH:mm:ss"));
                    cmd.ExecuteNonQuery();
                    //connexion.Close();
                }

                if (txtStockPiece.Text.ToString() != "")
                {
                    //MySqlConnection connexion = new MySqlConnection(connectionString);
                    //connexion.Open();
                    MySqlCommand cmd = new MySqlCommand("UPDATE piece_detachee SET stock_piece = @stock_piece WHERE id_piece = @id_piece", connexion);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id_piece", txtIdpiece.Text.ToString());
                    cmd.Parameters.AddWithValue("@stock_piece", txtStockPiece.Text.ToString());
                    cmd.ExecuteNonQuery();
                    //connexion.Close();
                }
                connexion.Close();
                loadGril();
                clearData();
                MessageBox.Show("Pièce modifiée avec succès", "Modifiée", MessageBoxButton.OK, MessageBoxImage.Information);


            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
