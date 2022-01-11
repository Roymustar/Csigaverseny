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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Csigaverseny
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public class MainWindow : Window, IComponentConnector
        {
            private List<MainWindow.Csiga> versenyzok;
            private DispatcherTimer timer = new DispatcherTimer();
            private int helyezes = 1;
            private SolidColorBrush[] ermek = new SolidColorBrush[3]
            {
      Brushes.Gold,
      Brushes.Silver,
      Brushes.SandyBrown
            };
            private double celVonalX;
            internal MainWindow foAblak;
            private bool _contentLoaded;

            public MainWindow()
            {
                this.InitializeComponent();
                this.ujversenyGomb.Click += new RoutedEventHandler(this.visszaGomb_Click);
                this.VersenyzoBeallitas();
                this.timer.Interval = TimeSpan.FromSeconds(0.04);
                this.timer.Tick += new EventHandler(this.timer_Tick);
                Random random = new Random();
                this.visszaGomb.IsEnabled = false;
            }

            private void VersenyzoBeallitas()
            {
                this.versenyzok = new List<MainWindow.Csiga>();
                this.versenyzok.Add(new MainWindow.Csiga("csiga1"));
                this.versenyzok.Add(new MainWindow.Csiga("csiga2"));
                this.versenyzok.Add(new MainWindow.Csiga("csiga3"));
            }

            private void timer_Tick(object sender, EventArgs e)
            {
                this.celVonalX = this.celVonal.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X;
                Random random = new Random();
                if (this.csiga1.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + 200.0 < this.celVonalX)
                {
                    double left = this.csiga1.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + (double)random.Next(1, 8);
                    if (left + 200.0 > this.celVonalX)
                        left = this.celVonalX - 200.0;
                    this.csiga1.Margin = new Thickness(left, 50.0, 0.0, 0.0);
                }
                else if (this.helyezes1.Content == (object)"")
                {
                    this.helyezes1.Content = (object)this.helyezes;
                    this.palya1.Fill = (Brush)this.ermek[this.helyezes - 1];
                    ++this.versenyzok[0].Helyezes[this.helyezes];
                    ++this.helyezes;
                }
                if (this.csiga2.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + 200.0 < this.celVonalX)
                {
                    double left = this.csiga2.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + (double)random.Next(1, 8);
                    if (left + 200.0 > this.celVonalX)
                        left = this.celVonalX - 200.0;
                    this.csiga2.Margin = new Thickness(left, 250.0, 0.0, 0.0);
                }
                else if (this.helyezes2.Content == (object)"")
                {
                    this.helyezes2.Content = (object)this.helyezes;
                    this.palya2.Fill = (Brush)this.ermek[this.helyezes - 1];
                    ++this.versenyzok[1].Helyezes[this.helyezes];
                    ++this.helyezes;
                }
                if (this.csiga3.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + 200.0 < this.celVonalX)
                {
                    double left = this.csiga3.TransformToAncestor((Visual)this.foAblak).Transform(new Point(0.0, 0.0)).X + (double)random.Next(1, 8);
                    if (left + 200.0 > this.celVonalX)
                        left = this.celVonalX - 200.0;
                    this.csiga3.Margin = new Thickness(left, 420.0, 0.0, 0.0);
                }
                else if (this.helyezes3.Content == (object)"")
                {
                    this.helyezes3.Content = (object)this.helyezes;
                    this.palya3.Fill = (Brush)this.ermek[this.helyezes - 1];
                    ++this.versenyzok[2].Helyezes[this.helyezes];
                    ++this.helyezes;
                }
                if (this.helyezes != 4)
                    return;
                this.timer.Stop();
                this.helyezes = 1;
                this.visszaGomb.IsEnabled = true;
                this.ujversenyGomb.IsEnabled = true;
                this.versenyAllasa.Content = (object)this.Allas();
            }

            private string Allas()
            {
                string str = "Hely\tNév\t\t1.\t2.\t3.\tPont\n";
                List<MainWindow.Csiga> list = this.versenyzok.OrderByDescending<MainWindow.Csiga, int>((Func<MainWindow.Csiga, int>)(x => x.Pont)).ThenByDescending<MainWindow.Csiga, int>((Func<MainWindow.Csiga, int>)(x => x.Helyezes[1])).ThenByDescending<MainWindow.Csiga, int>((Func<MainWindow.Csiga, int>)(x => x.Helyezes[2])).ToList<MainWindow.Csiga>();
                for (int index = 0; index < this.versenyzok.Count; ++index)
                    str += string.Format("{0}.\t{1}\t\t{2}\t{3}\t{4}\t{5} p\n", (object)(index + 1), (object)list[index].Nev, (object)list[index].Helyezes[1], (object)list[index].Helyezes[2], (object)list[index].Helyezes[3], (object)list[index].Pont);
                return str;
            }

            private void startButton_Click(object sender, RoutedEventArgs e)
            {
                this.timer.Start();
                this.startButton.IsEnabled = false;
                this.ujversenyGomb.IsEnabled = false;
            }

            private void visszaGomb_Click(object sender, RoutedEventArgs e)
            {
                this.csiga1.Margin = new Thickness(20.0, 50.0, 0.0, 0.0);
                this.csiga2.Margin = new Thickness(20.0, 250.0, 0.0, 0.0);
                this.csiga3.Margin = new Thickness(20.0, 420.0, 0.0, 0.0);
                this.helyezes1.Content = (object)"";
                this.helyezes2.Content = (object)"";
                this.helyezes3.Content = (object)"";
                this.startButton.IsEnabled = true;
                this.visszaGomb.IsEnabled = false;
                this.sav1.Fill = (Brush)null;
                this.sav2.Fill = (Brush)null;
                this.sav3.Fill = (Brush)null;
            }

            private void ujversenyGomb_Click(object sender, RoutedEventArgs e)
            {
                int num = (int)MessageBox.Show(this.Allas(), "A Verseny végeredménye");
                foreach (MainWindow.Csiga csiga in this.versenyzok)
                {
                    for (int index = 0; index < ((IEnumerable<int>)csiga.Helyezes).Count<int>(); ++index)
                        csiga.Helyezes[index] = 0;
                }
                this.versenyAllasa.Content = (object)"";
            }

            
            public void InitializeComponent()
            {
                if (this._contentLoaded)
                    return;
                this._contentLoaded = true;
                Application.LoadComponent((object)this, new Uri("/Csigaverseny;component/mainwindow.xaml", UriKind.Relative));
            }

           
            void IComponentConnector.Connect(int connectionId, object target)
            {
                switch (connectionId)
                {
                    case 1:
                        this.foAblak = (MainWindow)target;
                        break;
                    case 2:
                        this.palya1 = (Rectangle)target;
                        break;
                    case 3:
                        this.palya2 = (Rectangle)target;
                        break;
                    case 4:
                        this.palya3 = (Rectangle)target;
                        break;
                    case 5:
                        this.rajtVonal = (Rectangle)target;
                        break;
                    case 6:
                        this.celVonal = (Rectangle)target;
                        break;
                    case 7:
                        this.csiga1 = (Image)target;
                        break;
                    case 8:
                        this.csiga2 = (Image)target;
                        break;
                    case 9:
                        this.csiga3 = (Image)target;
                        break;
                    case 10:
                        this.startButton = (Button)target;
                        this.startButton.Click += new RoutedEventHandler(this.startButton_Click);
                        break;
                    case 11:
                        this.visszaGomb = (Button)target;
                        this.visszaGomb.Click += new RoutedEventHandler(this.visszaGomb_Click);
                        break;
                    case 12:
                        this.ujversenyGomb = (Button)target;
                        this.ujversenyGomb.Click += new RoutedEventHandler(this.ujversenyGomb_Click);
                        break;
                    case 13:
                        this.helyezes1 = (Label)target;
                        break;
                    case 14:
                        this.helyezes2 = (Label)target;
                        break;
                    case 15:
                        this.helyezes3 = (Label)target;
                        break;
                    case 16:
                        this.versenyAllasa = (Label)target;
                        break;
                    default:
                        this._contentLoaded = true;
                        break;
                }
            }

            private class Csiga
            {
                private string nev;
                private int pont;
                private int[] helyezes;
                private int[] pontok = new int[4] { 0, 3, 2, 1 };

                public Csiga(string csigaNev)
                {
                    this.nev = csigaNev;
                    this.helyezes = new int[4];
                }

                public int Pont => this.helyezes[1] * this.pontok[1] + this.helyezes[2] * this.pontok[2] + this.helyezes[3] * this.pontok[3];

                public string Nev
                {
                    get => this.nev;
                    private set => this.nev = value;
                }

                public int[] Helyezes
                {
                    get => this.helyezes;
                    set => this.helyezes = value;
                }
            }
        }
    }

    
    



