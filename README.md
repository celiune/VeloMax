# VeloMax
BDD et interopérabilité Avril 2022

## Set the correct connection string in 
```AffichageVeloMax.xaml.cs
```

```BicycletteAffichage.xaml.cs
```

```BoutiqueAffichage.xaml.cs
```

```IndividuAffichage.xaml.cs
```

```PieceAffichage.xaml.cs
```

```ruby
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

        string connectionString = @"SERVER=localhost;PORT=3306;DATABASE=velomax;UID=userID;PASSWORD=password;";
```

Be sure to change with your own user ```UID``` and ```PASSWORD```

