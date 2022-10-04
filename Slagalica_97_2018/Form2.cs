using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slagalica_97_2018
{
    public partial class Form2 : Form
    {
        Slagalica_97_2018.Form1 formOriginal;
        public Form2(int score, Form form1)
        {

            InitializeComponent();
            label3.Text = score.ToString()+" poena";
            formOriginal = (Form1)form1;
            
        }

        private void Form2_FormClosed(object sender, FormClosedEventArgs e)
        {
            formOriginal.Enabled = true;
            formOriginal.osveziMatricu();
            formOriginal.score = 0;
            formOriginal.timerStatus = false;
            formOriginal.vremeSekunde = 0;

            formOriginal.logika.init();
            formOriginal.osveziMatricu();



        }

        private void button1_Click(object sender, EventArgs e)
        {
            String Nickname;
            Nickname = textBox1.Text;
            


            if(textBox1.Text != "")
            {

                formOriginal.db.dodajRezultat(Nickname, formOriginal.score, formOriginal.logika.potezi, formOriginal.vremeSekunde);
                formOriginal.DSUpdated = true;
                this.Close();
            }
            else
            {
                MessageBox.Show("Unesite ime !!!","Greska", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.victory);
            player.Play();
        }
    }
}
