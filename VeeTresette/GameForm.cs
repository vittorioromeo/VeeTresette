using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace VeeTresette
{
    public partial class GameForm : Form
    {
        public Game Game { get; set; }

        public GameForm(Game mGame)
        {
            Game = mGame;
            InitializeComponent();
        }

        public void AddLog(string mString)
        {
            textBox1.Text += Environment.NewLine + mString;
        }
        public void GameNewGame()
        {
            NewGameForm newGameForm = new NewGameForm(this);
            newGameForm.Show();
        }
        public void GameNext()
        {
            Game.Next(listBox2.SelectedIndex);

            SyncWithGame();
        }
        public void SyncWithGame()
        {
            label1.Text = string.Format("Turn: {0}", Game.Turn);
            label2.Text = string.Format("Current player: {0}", Game.CurrentPlayer.Name);

            groupBox2.Text = string.Format("{0}'s Hand", Game.CurrentPlayer.Name);

            listBox1.Items.Clear();
            listBox2.Items.Clear();
            listBox3.Items.Clear();

            foreach (Player player in Game.Players)
            {
                player.CalculateScore();
                listBox1.Items.Add(string.Format("{0} ({1})", player.Name, player.Score));
            }

            // if (Game.CurrentPlayer.AI == false)
            //  {
            foreach (Card card in Game.CurrentPlayer.Hand)
            {
                listBox2.Items.Add(card.ToString());
            }


            listBox2.SelectedIndex = 0;
            //   }

            foreach (Card card in Game.Table)
            {
                listBox3.Items.Add(card.ToString());
            }

            AddLog("--- --- ---");

            textBox1.SelectionStart = textBox1.Text.Length;
            textBox1.ScrollToCaret();

            listBox1.SelectedIndex = Game.Players.IndexOf(Game.CurrentPlayer);
        }

        private void Button1Click(object sender, EventArgs e)
        {
            GameNewGame();
        }
        private void Button2Click(object sender, EventArgs e)
        {
            GameNext();
        }
    }
}
