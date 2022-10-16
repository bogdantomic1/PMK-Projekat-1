using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using classes;
namespace Prodavnica
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.Text = "Administracija i Statstika";
            
        }

        private void Form2_Load(object sender, EventArgs e)
        {

            //prikaz kategorija i njhovih id u listboxu
            listBox1.Items.Clear();
            var data = DatabaseConnection.select("SELECT * from Kategorija", new Dictionary<string, string>());
            var dt = new DataTable();
            dt.Load(data);
            List<string> pun_prikaz = new List<string>();
            foreach (DataRow dataRow in dt.Rows)
            {
                pun_prikaz.Add(dataRow["idKategorija"].ToString() +" "+ dataRow["NazivKategorija"].ToString());
            }
            if (dt.Rows.Count > 0)
            {
                listBox1.DataSource = pun_prikaz;
                listBox2.ValueMember = "NazivKategorija";
                listBox1.DisplayMember = "NazivKategorija";
            }


            //combobox ucitavanje kategorija
            comboBox1.Items.Clear();
            var data1 = DatabaseConnection.select("SELECT * from Kategorija", new Dictionary<string, string>());
            var dt1 = new DataTable();
            dt1.Load(data1);

            List<string> pun_prikaz_cb = new List<string>();
            foreach (DataRow dataRow in dt1.Rows)
            {
                pun_prikaz_cb.Add(dataRow["idKategorija"].ToString() + " " + dataRow["NazivKategorija"].ToString());
            }
            if (dt1.Rows.Count > 0)
            {
                comboBox1.DataSource = pun_prikaz_cb;
                comboBox1.DisplayMember = "NazivKategorija";
            }



            
            //prikaz artikala u listboxu
            listBox2.Items.Clear();
            var data2 = DatabaseConnection.select("SELECT * from Artikal", new Dictionary<string, string>());
            var dt2 = new DataTable();
            dt2.Load(data2);

            List<string> pun_prikaz_artikla = new List<string>();
            foreach (DataRow dataRow in dt2.Rows)
            {
                pun_prikaz_artikla.Add(dataRow["idArtikal"].ToString() + "      " + dataRow["NazivArtikal"].ToString() + "   Cena:" + dataRow["Cena"].ToString() + "rsd     " + dataRow["JedinicaMere"].ToString());
            }

            if (dt2.Rows.Count > 0)
            {
                listBox2.DataSource = pun_prikaz_artikla;
                listBox2.DisplayMember = "NazivArtikal";
            }


        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            var id = listBox1.GetItemText(listBox1.SelectedValue);
            char[] charsToTrim = "0123456789 '\'''\"'".ToCharArray();
            string a = id.Trim(charsToTrim);
            textBox1.Text = a;                                                 //prikaz selektovanog item-a (samo Naziva kategorije) koji  u text boxu

            button2.Name = listBox1.GetItemText(listBox1.SelectedItem);         //prilikom menjanja selektovanog item-a menja se i ime samog dugmeta
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //dodaj
            Dictionary<string, string> parametar = new Dictionary<string, string>();
            parametar.Add("@NazivKategorija", textBox1.Text);                                       //iz textboxa se izvlaci naziv kategorije
            DatabaseConnection.insert("INSERT INTO Kategorija (NazivKategorija) VALUES (@NazivKategorija)", parametar);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //izmeni
            var id = button2.Name;                                              //selektovani item se smesta u promenjljivu
            var ime = id.Remove(2, button2.Name.Length - 2);                    //ostavljaju se samo prva dva karaktera(id)
            char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string res = ime.Trim(charsToTrim);                                 //u slucaju da je id jednocifren "skidaju" se slova i razmaci
            Dictionary<string, string> parametar = new Dictionary<string, string>();
            parametar.Add("@NazivKategorija", textBox1.Text);
            parametar.Add("@id", res);
            DatabaseConnection.update("UPDATE Kategorija SET NazivKategorija = @NazivKategorija WHERE  idKategorija = @id", parametar);

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //obrisi
            var id = listBox1.GetItemText(listBox1.SelectedValue);
            char[] charsToTrim = "0123456789 '\'''\"'".ToCharArray();
            string res = id.Trim(charsToTrim);
            Dictionary<string, string> parametar = new Dictionary<string, string>(); 
            parametar.Add("@NazivKategorija", res);
            DatabaseConnection.delete("DELETE FROM Kategorija WHERE NazivKategorija=@NazivKategorija", parametar);
        }


        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //promena izbora comboboxa menja i ime samog comboboxa
            var id = comboBox1.GetItemText(comboBox1.SelectedItem);
            char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ ".ToCharArray();
            string res = id.Trim(charsToTrim);
            comboBox1.Name = res;


        }

        private void button5_Click(object sender, EventArgs e)
        {
            //dodaj artikal
            Dictionary<string, string> parametar = new Dictionary<string, string>();
            parametar.Add("@idKat", comboBox1.Name);
            parametar.Add("@NazivArtikal", textBox3.Text);
            parametar.Add("Cena", textBox2.Text);
            parametar.Add("@JedinicaMere", textBox4.Text);
            DatabaseConnection.insert("INSERT INTO Artikal (NazivArtikal, idKat, Cena, JedinicaMere) VALUES (@NazivArtikal,@idKat,@Cena,@JedinicaMere)", parametar);
            textBox2.Clear();
            textBox3.Clear();           //nakon unosa se ciste polja
            textBox4.Clear();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            // selektovanjem se menjaju imena dugmica
            button6.Name = listBox2.GetItemText(listBox2.SelectedItem);
            button4.Name = listBox2.GetItemText(listBox2.SelectedItem);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //dugme obrisi arikal
            var t = button6.Name;
            var ime = t.Remove(2, button6.Name.Length - 2);
            char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string res = ime.Trim(charsToTrim);
            Dictionary<string, string> parametar = new Dictionary<string, string>();
            parametar.Add("@idArtikal", res);
            DatabaseConnection.delete("DELETE FROM Artikal WHERE idArtikal=@idArtikal", parametar);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            //dugme azuriraj artikal
            var id = button4.Name;
            var ime = id.Remove(2, button4.Name.Length - 2);
            char[] charsToTrim = "ABCDEFGHIJKLMNOPQRSTUVWXYZ abcdefghijklmnopqrstuvwxyz".ToCharArray();
            string res = ime.Trim(charsToTrim);
            Dictionary<string, string> parametar = new Dictionary<string, string>();
            parametar.Add("@NazivArtikal", textBox3.Text);
            parametar.Add("@Cena", textBox2.Text);
            parametar.Add("@JedinicaMere", textBox4.Text);
            parametar.Add("@id", res);
            parametar.Add("@idKat", comboBox1.Name);

            DatabaseConnection.update("UPDATE Artikal SET NazivArtikal = @NazivArtikal,idKat=@idKat, Cena=@Cena, JedinicaMere=@JedinicaMere WHERE  idArtikal = @id", parametar);

        }
    }
}


