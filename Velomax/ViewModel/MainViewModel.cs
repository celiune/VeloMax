using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VELOMAX.Core;

namespace VELOMAX.MVVM.ViewModel
{
    class MainViewModel:ObservableObject
    {
        public RelayCommand TableauBordCommand
        {
            get;
            set;
        }        

        public RelayCommand PiecesCommand
        {
            get;
            set;
        }

        public RelayCommand VelosCommand
        {
            get;
            set;
        }

        public RelayCommand IndividusCommand
        {
            get;
            set;
        }

        public RelayCommand BoutiquesCommand
        {
            get;
            set;
        }

        public RelayCommand FournisseursCommand
        {
            get;
            set;
        }

        public RelayCommand CommandesCommand
        {
            get;
            set;
        }

        public RelayCommand StockCommand
        {
            get;
            set;
        }

        public RelayCommand StatistiqueCommand
        {
            get;
            set;
        }

        public TableauBordModel TableauBordVM
        {
            get;
            set;
        }

        public PiecesModel PiecesVM
        {
            get;
            set;
        }

        public VelosModel VelosVM
        {
            get;
            set;
        }

        public BoutiqueModel BoutiquesVM
        {
            get;
            set;
        }

        public IndividuModel IndividusVM
        {
            get;
            set;
        }

        public CommandeModel CommandesVM
        {
            get;
            set ;
        }
        public FournisseurModel FournisseursVM 
        { 
            get;
            set;
        }

        public StockModel StockVM
        {
            get;
            set;
        }

        public StatistiqueModel StatistiqueVM
        {
            get;
            set;
        }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set 
            {   _currentView = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            TableauBordVM = new TableauBordModel();
            PiecesVM = new PiecesModel();
            VelosVM = new VelosModel();
            IndividusVM = new IndividuModel();
            BoutiquesVM = new BoutiqueModel();
            FournisseursVM = new FournisseurModel();
            CommandesVM = new CommandeModel();
            StockVM = new StockModel();
            StatistiqueVM = new StatistiqueModel();
            CurrentView = TableauBordVM;

            TableauBordCommand = new RelayCommand(o => 
            {
                CurrentView = TableauBordVM;
            });

            PiecesCommand = new RelayCommand(o =>
            {
                CurrentView = PiecesVM;
            });


            VelosCommand = new RelayCommand(o =>
            {
                CurrentView = VelosVM;
            });

            BoutiquesCommand = new RelayCommand(o =>
            {
                CurrentView = BoutiquesVM;
            });

            IndividusCommand = new RelayCommand(o =>
            {
                CurrentView = IndividusVM;
            });

            FournisseursCommand = new RelayCommand(o =>
            {
                CurrentView = FournisseursVM;
            });

            CommandesCommand = new RelayCommand(o =>
            {
                CurrentView = CommandesVM;
            });

            StatistiqueCommand = new RelayCommand(o =>
            {
                CurrentView = StatistiqueVM;
            });

            StockCommand = new RelayCommand(o =>
            {
                CurrentView = StockVM;
            });

            
        }
    }
}
