﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WPF_MatchingGame_AjdiniSefedin
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer = new DispatcherTimer();
        int tempsEcoule = 0;
        int nbPairesTrouvees = 0;
        public MainWindow()
        {
            InitializeComponent();
            SetUpGame();
            timer.Interval = TimeSpan.FromSeconds(.1);
            timer.Tick += new EventHandler(Timer_Tick);
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            tempsEcoule++;
            txtTemps.Text = (tempsEcoule / 10F).ToString("0.0s");
            if (nbPairesTrouvees == 8)
            {
                timer.Stop();
                txtTemps.Text = txtTemps.Text + " - Rejouer ? ";
            }
        }

        private void SetUpGame()
        {
            tempsEcoule = 0;
            nbPairesTrouvees = 0;
            timer.Start();

            List<string> animalEmoji = new List<string>()
            {
            "🐈","🐈",
            "🐷","🐷",
            "🐐","🐐",
            "🦊","🦊",
            "🐴","🐴",
            "🦨","🦨",
            "🦉","🦉",
            "🐀","🐀",
            };

            foreach (TextBlock textBlock in grdMain.Children.OfType<TextBlock>())
            {

                Random nbAlea = new Random();
                string nextEmoji;
                int index;

                if (textBlock.Name != "txtTemps")
                {
                index = nbAlea.Next(animalEmoji.Count); // index est de type int
                                                            // nbalea est un objet de type Random()
                nextEmoji = animalEmoji[index]; // nextEmoji est de type string
                textBlock.Text = nextEmoji;
                textBlock.Visibility = Visibility.Visible;
                animalEmoji.RemoveAt(index); // on retire un animal de la liste pour ne pas l’attribuer à nouveau.

                }

            }
        }
        TextBlock derniereTBClique; // on va l’utiliser pour faire une référence à  la TextBlock sur laquelle on vient de cliquer
        bool trouvePaire = false;
        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

            TextBlock textBlockActif = sender as TextBlock;
            if (!trouvePaire)
            {
                textBlockActif.Visibility = Visibility.Hidden;
                derniereTBClique = textBlockActif;
                trouvePaire = true;
            }
            else if (textBlockActif.Text == derniereTBClique.Text)
            {
                nbPairesTrouvees++;
                textBlockActif.Visibility = Visibility.Hidden;
                trouvePaire = false;
            }
            else
            {
                derniereTBClique.Visibility = Visibility.Visible;
                trouvePaire = false;
            }

        }
        private void txtTemps_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (nbPairesTrouvees == 8)
            {
                SetUpGame();
            }
        }


    }
}
