
using System.Windows;
using System.Windows.Controls;


namespace VELOMAX.User_Controls
{
    /// <summary>
    /// Logique d'interaction pour Commandes.xaml
    /// </summary>
    public partial class Commandes : UserControl
    {
        public Commandes()
        {
            InitializeComponent();
        }

        public string Title2
        {
            get { return (string)GetValue(Title2Property); }
            set { SetValue(Title2Property, value); }
        }

        public static readonly DependencyProperty Title2Property = DependencyProperty.Register("Title2",typeof(string), typeof(Commandes));

        public string Desc
        {
            get { return (string)GetValue(DescProperty); }
            set { SetValue(DescProperty, value); }
        }

        public static readonly DependencyProperty DescProperty = DependencyProperty.Register("Desc", typeof(string), typeof(Commandes));

        public FontAwesome.Sharp.IconChar Icon
        {
            get { return (FontAwesome.Sharp.IconChar)GetValue(IconProperty); }
            set { SetValue(IconProperty, value); }
        }

        public static readonly DependencyProperty IconProperty = DependencyProperty.Register("Icon", typeof(FontAwesome.Sharp.IconChar), typeof(Commandes));


    }
}
