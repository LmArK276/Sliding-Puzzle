using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

namespace Slagalica_97_2018
{
    public partial class Form1 : Form
    {
        public Logika logika = new Logika();
        public bool timerStatus;
        public int vremeSekunde;
        DateTime dt = new DateTime();
        public int score;
        public DBFunkcije db;

        public bool DSUpdated;


        public Form1()
        {
            
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'scoreboardDataSet.Scoreboard_tbl' table. You can move, or remove it, as needed.
            //this.scoreboard_tblTableAdapter.Fill(this.scoreboardDataSet.Scoreboard_tbl);

            db = new DBFunkcije();
            

            timerStatus = false;
            vremeSekunde = 0;
            score = 0;
            DSUpdated = false;

            logika.init();
            osveziMatricu();
            osveziScoreboard();


            foreach (Control c in tableLayoutPanel1.Controls)
            {
                c.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.tableLayoutPanel1_MouseDoubleClick);
            }



            if (logika.proveraPobede())
            {
                int timeFactor = 1 ;
                int moveFactor = 10;

                score = (int)((timeFactor + moveFactor) * 1000);

                MessageBox.Show("Cestitamo, imate puno srece", "Bravo !!!", MessageBoxButtons.OK);


                Form2 victory = new Slagalica_97_2018.Form2(score, this);
                timerStatus = false;
                this.Enabled = false;
                victory.Show();


                
            }

        }

        public void osveziMatricu()
        {

            label3.Text = logika.potezi.ToString();


            for (int i = 0; i < tableLayoutPanel1.RowCount; i++)
            {
                for(int j = 0; j < tableLayoutPanel1.ColumnCount; j++)
                {

                    //Debug.Write(" F "+logika.tabla[i, j]);
                   
                    //tableLayoutPanel1.GetControlFromPosition(i, j).BackgroundImage = null;
                    tableLayoutPanel1.GetControlFromPosition(j, i).BackgroundImage = logika.dajSlikuZaPolje(logika.tabla[i,j]);
                 
                    //tableLayoutPanel1.GetControlFromPosition(i, j).BackgroundImageLayout = ImageLayout.Stretch;
                }
                //Debug.WriteLine("\n");
            }


        }

        private void button1_Click(object sender, EventArgs e)
        {

            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.beep_1);
            player.Play();

            score = 0;
            timerStatus = false;
            vremeSekunde = 0;

            logika.init();
            osveziMatricu();
            osveziScoreboard();
            
            
        }



        private void tableLayoutPanel1_MouseClick(object sender, MouseEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //Debug.WriteLine("TIMER STATUS"+timerStatus);
            if(timerStatus)
            {
                vremeSekunde++;
            }

            label6.Text = dt.AddSeconds(vremeSekunde).ToString("HH.mm.ss");


            if(DSUpdated)
            {
                //Debug.WriteLine("DSUPDATE "+DSUpdated);

                osveziScoreboard();

                DSUpdated = false;

            }
        }


        public void osveziScoreboard()
        {
            dataGridView1.Rows.Clear();

            List<ScoreTip> top10 = db.dajTop10();


            foreach(ScoreTip score in top10)
            {
                dataGridView1.Rows.Add(
                    score.nicnkame,
                    score.score,
                    score.potezi,
                    score.vreme
                    );
            }

        }

        private void tableLayoutPanel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            timerStatus = true;

            //Debug.WriteLine("click " + ((Panel)sender).Name);

            int i = tableLayoutPanel1.GetRow((Control)sender);
            int j = tableLayoutPanel1.GetColumn((Control)sender);

            bool odigranSuccess = logika.odigrajPotez(i, j);

            //Debug.WriteLine("I: " + i + " J: " + j);
            //Debug.WriteLine("ODIGRANSUCCES "+odigranSuccess);

            if (odigranSuccess)
            {

                //logika.printTabla();
                osveziMatricu();
                //label1.Text = "Nista";
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.whoosh);
                player.Play();


                if (logika.proveraPobede())
                {
                    double timeFactor = 1 / Math.Sqrt(vremeSekunde);
                    double moveFactor = 10 / logika.potezi;

                    score = (int)((timeFactor + moveFactor) * 1000);

                    Form2 victory = new Slagalica_97_2018.Form2(score, this);
                    timerStatus = false;
                    this.Enabled = false;
                    victory.Show();



                }

            }
        }
    }

}
