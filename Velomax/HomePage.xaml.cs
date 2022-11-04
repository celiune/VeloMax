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
using System.Windows.Shapes;
using FontAwesome.Sharp;

namespace VELOMAX
{
    /// <summary>
    /// Logique d'interaction pour HomePage2.xaml
    /// </summary>
    public partial class HomePage : Window
    {
        public HomePage()
        {
            InitializeComponent();
            customizeDesign();
        }

        private void customizeDesign() // pour cacher les sous-menu des stackpanels 
        {
            PieceVeloStackPanelSubMenu.Visibility = Visibility.Collapsed;
            ClientsStackPanelSubMenu.Visibility = Visibility.Collapsed;
        }

        private void hideSubMenu() // pour cacher les sous-menus qui ont été mis visibles avec une condition si les sous-menu sont visibles, cette méthode les cache
        {
            if (PieceVeloStackPanelSubMenu.Visibility == Visibility.Visible )
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

        private void TableauDeBordBTN_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
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
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //... 
            hideSubMenu();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
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
        }

        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }
        #endregion

        private void StockBTN_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void CommandesBTN_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void StatistiquesBTN_Click(object sender, RoutedEventArgs e)
        {
            //...
            hideSubMenu();
        }

        private void FournisseursBTN_Click(object sender, RoutedEventArgs e)
        {
            hideSubMenu();
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


    }
}
