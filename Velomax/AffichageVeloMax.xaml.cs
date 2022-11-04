using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using MySql.Data.MySqlClient;
using System.Data;



namespace VELOMAX
{
    /// <summary>
    /// Logique d'interaction pour AffichageVeloMax.xaml
    /// </summary>
    public partial class AffichageVeloMax : Window
    {
        public AffichageVeloMax()
        {
            InitializeComponent();
            customizeDesign();
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        private void customizeDesign() // pour cacher les sous-menu des stackpanels 
        {
            PieceVeloStackPanelSubMenu.Visibility = Visibility.Collapsed;
            ClientsStackPanelSubMenu.Visibility = Visibility.Collapsed;
        }

        private void hideSubMenu() // pour cacher les sous-menus qui ont été mis visibles avec une condition si les sous-menu sont visibles, cette méthode les cache
        {
            if (PieceVeloStackPanelSubMenu.Visibility == Visibility.Visible)
            {
                PieceVeloStackPanelSubMenu.Visibility = Visibility.Collapsed;
            }
            if (ClientsStackPanelSubMenu.Visibility == Visibility.Visible)
            {
                ClientsStackPanelSubMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void showSubMenu(StackPanel subMenu) // pour afficher les sous-menus, si le sous-menu est caché, cette méthode va l'afficher, cependant il va aussi cacher tous les sous-menus déjà ouverts
        {
            if (subMenu.Visibility == Visibility.Collapsed)
            {
                hideSubMenu();
                subMenu.Visibility = Visibility.Visible;
            }
            else
            {
                subMenu.Visibility = Visibility.Collapsed;
            }
        }

        private void PièceVeloBTN_Click(object sender, RoutedEventArgs e)
        {
            showSubMenu(PieceVeloStackPanelSubMenu);
        }

        #region PièceVeloSubMenu    
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TablePieces();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //... 
            hideSubMenu();
            TableBicyclette();
        }

        #endregion
        private void ClientsBTN_Click(object sender, RoutedEventArgs e)
        {
            showSubMenu(ClientsStackPanelSubMenu);
        }
        #region ClientsSubMenu  
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableIndividu();
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableBoutique();
        }

        #endregion

        private void CommandesBTN_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableCommande();
        }

        private void FournisseursBTN_Click(object sender, RoutedEventArgs e)
        {
            hideSubMenu();
            TableFournisseur();
        }

        private void AdressesBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableAdresse();
        }

        private void AssembleBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableAssemble();
        }
        private void FournitBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableFournit();
        }
        private void AchatPieceBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableAchatPieces();
        }

        private void AchatVeloBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableAchatVelo();
        }
        private void FidelioBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableFidelio();
        }
        private void AdhereBTN(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
            TableAdhere();
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void LogOutApp(object sender, RoutedEventArgs e)
        {
            Connexion connexion = new Connexion();
            connexion.Show();
            this.Close();
        }


        public void TableBicyclette()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from bicyclette;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TablePieces()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from piece_detachee;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }

        public void TableIndividu()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from individu;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableBoutique()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from boutique;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableFournisseur()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from fournisseur;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableCommande()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from commande;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableAdresse()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from adresse;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableAssemble()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from assemble;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableFournit()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from fournit;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableAchatPieces()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from achat_piece;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableAchatVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from achat_velo;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableFidelio()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from fidelio;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }
        public void TableAdhere()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select * from adhere;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            UserDATA.DataContext = dt;
        }


    }
}
