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
    public partial class NewGameForm : Form
    {
        public GameForm GameForm { get; set; }

        public NewGameForm(GameForm mGameForm)
        {
            InitializeComponent();
            GameForm = mGameForm;
        }

        private void Button1Click(object sender, EventArgs e)
        {
            List<Player> players = new List<Player>();

            players.Add(new Player(GameForm.Game, textBox1.Text, checkBox3.Checked));            
            GameForm.AddLog(string.Format("Player added: {0}", textBox1.Text));

            players.Add(new Player(GameForm.Game, textBox2.Text, checkBox4.Checked));
            GameForm.AddLog(string.Format("Player added: {0}", textBox2.Text));

            if (checkBox1.Checked)
            {
                players.Add(new Player(GameForm.Game, textBox3.Text, checkBox5.Checked));
                GameForm.AddLog(string.Format("Player added: {0}", textBox3.Text));
            }

            if (checkBox2.Checked)
            {
                players.Add(new Player(GameForm.Game, textBox4.Text, checkBox6.Checked));
                GameForm.AddLog(string.Format("Player added: {0}", textBox4.Text));
            }

            GameForm.Game.NewGame(players);
            GameForm.SyncWithGame();

            GameForm.button1.Enabled = false;
            GameForm.button2.Enabled = true;

            this.Close();
        }
    }
}
