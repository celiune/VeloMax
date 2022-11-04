using System.Windows.Controls;
using MySql.Data.MySqlClient;
using System.Data;
using System.Windows;

namespace VELOMAX.MVVM.View
{
    /// <summary>
    /// Logique d'interaction pour StatistiqueView.xaml
    /// </summary>
    public partial class StatistiqueView : UserControl
    {
        public StatistiqueView()
        {
            InitializeComponent();
        }

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=root;PASSWORD=MySQLmdp.123;";

        //1)
        public void QuantitesVendues_PiecesVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_bicyclette as modèle,quantite_velo as quantité, prix_bicyclette as prix_unite, sum(quantite_velo*prix_bicyclette) as gainTotalparVeloMax from achat_velo natural join bicyclette group by id_bicyclette union select id_piece, sum(quantite_piece), prix_piece, sum(quantite_piece*prix_piece) from achat_piece natural join fournit group by id_piece,siret;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //2) et 3) Liste des membres des programmes fidelio & date d'expiration d'abonnements
        public void ListeMembresFidelioProgramme()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_individu,nom_individu,prenom_individu,date_paiement_adhesion,date_add(date_paiement_adhesion, interval 1 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '1 Fidélio' union " +
                "select id_individu, nom_individu, prenom_individu, date_paiement_adhesion, date_add(date_paiement_adhesion, interval 2 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '2 Fidélio Or' union " +
                "select id_individu, nom_individu, prenom_individu, date_paiement_adhesion, date_add(date_paiement_adhesion, interval 2 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '3 Fidélio Platine' union " +
                "select id_individu, nom_individu, prenom_individu, date_paiement_adhesion, date_add(date_paiement_adhesion, interval 3 YEAR) as date_expiration_adhesion from adhere natural join individu where num_program = '4 Fidélio Max';", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //4)
        //les best clients(individus et boutiques) en fonction des quantités vendues en nombre de pièces vendues
        public void BestClientIndividu_Quantite()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_individu, nom_individu, prenom_individu, sum(quantite_piece), count(id_piece) from achat_piece natural join commande natural join individu where id_individu is not null group by id_individu order by sum(quantite_piece) desc;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        public void BestClientBoutique_Quantite()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_boutique,sum(quantite_piece),count(id_piece) from achat_piece natural join commande where id_boutique is not null group by id_boutique order by sum(quantite_piece) desc;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //les best clients(individus et boutiques) en cumul en euros pour les pièces
        public void BestClients_CumulEurosPiece()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_individu as id_client,id_commande,sum(prix_piece*quantite_piece) as prixTotalPiecesEn€,count(id_piece) as nombre_pieces from commande natural join achat_piece natural join fournit where id_individu is not null group by id_commande union " +
                "select id_boutique, id_commande, sum(prix_piece * quantite_piece), count(id_piece) from commande natural join achat_piece natural join fournit where id_boutique is not null group by id_commande;  ", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //les best clients(individus et boutiques) en cumul en euros pour les vélos
        public void BestClients_CumulEurosVelo()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_individu as id_client,id_commande,sum(prix_bicyclette*quantite_velo) as prixTotalVeloEn€, count(id_bicyclette) as nombre_velos from commande natural join achat_velo natural join bicyclette where id_individu is not null group by id_commande union " +
                "select id_boutique, id_commande, sum(prix_bicyclette * quantite_velo), count(id_bicyclette) from commande natural join achat_velo natural join bicyclette where id_boutique is not null group by id_commande;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //5)
        //moyenne du nombre de pièces par commande
        public void MoyenneNombrePiecesCommande()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_commande,avg(quantite_piece) from commande natural join achat_piece natural join fournit group by id_commande;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }
        //moyenne du nombre de vélos par commande
        public void MoyenneNombreVelosCommande()
        {
            MySqlConnection connexion = new MySqlConnection(connectionString);
            MySqlCommand cmd = new MySqlCommand("select id_commande,avg(quantite_velo) from commande natural join achat_velo natural join bicyclette group by id_commande;", connexion);
            connexion.Open();
            DataTable dt = new DataTable();
            dt.Load(cmd.ExecuteReader());
            connexion.Close();
            StatistiqueDatagrid.DataContext = dt;
        }

        private void QteVenduParPieceVelo_Click(object sender, RoutedEventArgs e)
        {
            QuantitesVendues_PiecesVelo();
        }

        private void ListeMembre_Click(object sender, RoutedEventArgs e)
        {
            ListeMembresFidelioProgramme();
        }

        private void MeilleurClientIndQte_Click(object sender, RoutedEventArgs e)
        {
            BestClientIndividu_Quantite();
        }

        private void MeilleurClientBtqQte_Click(object sender, RoutedEventArgs e)
        {
            BestClientBoutique_Quantite();
        }

        private void MeilleurClientIndEuros_Click(object sender, RoutedEventArgs e)
        {
            BestClients_CumulEurosPiece();
        }

        private void MeilleurClientBtqEuros_Click(object sender, RoutedEventArgs e)
        {
            BestClients_CumulEurosVelo();
        }

        private void AnalysePiece_Click(object sender, RoutedEventArgs e)
        {
            MoyenneNombrePiecesCommande();
        }

        private void AnalyseVelo_Click(object sender, RoutedEventArgs e)
        {
            MoyenneNombreVelosCommande();
        }
    }
}
