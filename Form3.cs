using classes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Prodavnica
{

   
    public partial class Form3 : Form
    {

        
        public Form3()
        {
            InitializeComponent();
            this.Text = "Prodaja/Naplata";
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            var data = DatabaseConnection.select("SELECT * from Kategorija", new Dictionary<string, string>());
            var dt = new DataTable();
            dt.Load(data);
            var pozicija = 9;
            foreach (DataRow red in dt.Rows)
            {
                var novo_dugme = new System.Windows.Forms.Button();
                this.panel1.Controls.Add(novo_dugme);
                novo_dugme.Location = new System.Drawing.Point(pozicija, 3);
                novo_dugme.Name = "button" + red.ItemArray[0];
                novo_dugme.Size = new System.Drawing.Size(75, 50);
                pozicija += 75;
                novo_dugme.TabIndex = 0;
                novo_dugme.Text = red.ItemArray[1].ToString();
                novo_dugme.UseVisualStyleBackColor = true;


        
            }

            foreach(var btn in panel1.Controls)
            {
                ((Button)btn).Click += Form3_Click;
                
            }

            
            
            textBox2.Text = "1";
        }

        public void artikal_dugme_klik(object sender, EventArgs e){
            Button b = (Button)sender;
            textBox1.Text = b.Text;

        }
             
        

        private void Form3_Click(object sender, EventArgs e)
        {
            panel2.Controls.Clear();
            Button b = (Button)sender;
            char[] charsToTrim = { 'b', 'u', 't', 't', 'o', 'n'};
            string res = b.Name.Trim(charsToTrim);
            var ime = Int32.Parse(res);


            Dictionary<string, int> parametar = new Dictionary<string, int>();
            parametar.Add("@ime", ime);


            var data = DatabaseConnection.select2("SELECT * from Artikal WHERE idKat=@ime",parametar); 
            var dt = new DataTable();
            dt.Load(data);
            var pozicija = 9;
            foreach (DataRow red in dt.Rows)
            {
                var novo_dugme = new System.Windows.Forms.Button();
                this.panel2.Controls.Add(novo_dugme);
                novo_dugme.Location = new System.Drawing.Point(pozicija, 3);
                novo_dugme.Name = "button" + red.ItemArray[1];
                novo_dugme.Size = new System.Drawing.Size(75, 50);
                pozicija += 75;
                novo_dugme.TabIndex = 0;
                novo_dugme.Text = red.ItemArray[1].ToString() + " "+ red.ItemArray[3] + "rsd";
                novo_dugme.UseVisualStyleBackColor = true;

                foreach (var btn in panel2.Controls){
                ((Button)btn).Click += artikal_dugme_klik;

            }
            }
            

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void panel4_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }


        //Buttons
        private void button10_Click(object sender, EventArgs e)
        {
            button10.Name = "0";
            textBox2.Text = button10.Name;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Name = "1";
            textBox2.Text = button1.Name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            button3.Name = "2";
            textBox2.Text = button3.Name;
        }

        private void button13_Click(object sender, EventArgs e)
        {
            button13.Name = "3";
            textBox2.Text = button13.Name;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            button2.Name = "4";
            textBox2.Text = button2.Name;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            button5.Name = "5";
            textBox2.Text = button5.Name;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            button8.Name = "6";
            textBox2.Text = button8.Name;
        }

        private void button12_Click(object sender, EventArgs e)
        {
            button12.Name = "7";
            textBox2.Text = button12.Name;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            button7.Name = "8";
            textBox2.Text = button7.Name;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            button9.Name = "9";
            textBox2.Text = button9.Name;
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            var ime = textBox1.Text;
            var kol = textBox2.Text;
            int k = Int32.Parse(kol);
            char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string res = ime.Trim(charsToTrim);
            int i = Int32.Parse(res);

            var temp = 0;
            temp += i * k;
            label1.Text = temp.ToString();

            listBox1.Items.Add(ime+ " x "+ kol);
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void button15_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            listBox2.Items.AddRange(listBox1.Items);
            listBox1.Items.Clear();
        }

        private void button17_Click(object sender, EventArgs e)
        {

            if (listBox2.Items != null)
            {
                var lista = listBox2.Items.Cast<string>().ToList();

                int iznos = 0;
                foreach(var clan in lista)
                {
                    var kol = clan.Remove(clan.Length - 2, 2);
                    var ime = clan.Substring(clan.Length - 1);
                    char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz".ToCharArray();
                    string res = ime.Trim(charsToTrim);
                    string res_kol = kol.Trim(charsToTrim);
                    int i = Int32.Parse(res);
                    int k = Int32.Parse(res_kol);
                    iznos += k*i;
                }
                Dictionary<string, string> parametar = new Dictionary<string, string>();
                var now = DateTime.Now;
                var datum = now.ToString("yyyy-MM-dd HH:mm");
                parametar.Add("@Sadrzaj", listBox2.GetItemText(listBox2.Items));
                parametar.Add("@Datum",datum);
                parametar.Add("@Iznos", iznos.ToString());
                DatabaseConnection.insert("INSERT INTO Racun (Sadrzaj, Datum, Iznos) VALUES (@Sadrzaj, @Datum, @Iznos)", parametar);
                
                string put = $@"C:\Users\User\Desktop\ProgMobKom\Prodavnica\Racuni\{DateTime.Now.Ticks}.txt";

                if (!File.Exists(put)){
                    using (StreamWriter ispis = File.CreateText(put))
                    {
                        ispis.WriteLine("Prodavnica\n");
                        foreach(var clan in lista)
                        {
                            ispis.WriteLine(clan);
                        }
                        ispis.WriteLine("Ukupan iznos: " + iznos);
                        ispis.WriteLine(datum);
                        
                    }
                }


                listBox2.Items.Clear();
            }

            else { MessageBox.Show("Na racunu nema artikala!"); }
        }

    }


    
}
