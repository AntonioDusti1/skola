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

namespace skola
{
    public partial class Form1 : Form
    {
        private List<Osoba> osobe = new List<Osoba>();
        public Form1()
        {
            InitializeComponent();
            comboBox1.Items.AddRange(new string[] { "Učenik", "Profesor", "Tehničko osoblje" });
            comboBox2.Items.AddRange(new string[] { "Učenik", "Profesor", "Tehničko osoblje" });
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string ime = textBox1.Text;
            string prezime = textBox2.Text;
            string uloga = comboBox1.SelectedItem?.ToString();

            if (string.IsNullOrWhiteSpace(ime) || string.IsNullOrWhiteSpace(prezime) || string.IsNullOrWhiteSpace(uloga))
            {
                MessageBox.Show("Molimo popunite sva polja.");
                return;
            }
            Osoba osoba = new Osoba { Ime = ime, Prezime = prezime, Uloga = uloga };
            osobe.Add(osoba);

            textBox1.Clear();
            textBox2.Clear();
            comboBox1.SelectedIndex = -1;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            PrikaziOsobe(osobe);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string filterUloga = comboBox2.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(filterUloga))
            {
                MessageBox.Show("Molimo odaberite ulogu za filtriranje.");
                return;
            }

            var filtriraneOsobe = osobe.Where(o => o.Uloga == filterUloga).ToList();
            PrikaziOsobe(filtriraneOsobe);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (var osoba in osobe)
            {
                switch (osoba.Uloga)
                {
                    case "Učenik":
                        osoba.Aktivnost = "Uči";
                        break;
                    case "Profesor":
                        osoba.Aktivnost = "Predaje";
                        break;
                    case "Tehničko osoblje":
                        osoba.Aktivnost = "Održava";
                        break;
                }
            }

            PrikaziOsobe(osobe);
        }
        private void PrikaziOsobe(List<Osoba> osobeZaPrikaz)
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = osobeZaPrikaz;
        }
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void btnSortByName_Click(object sender, EventArgs e)
        {
            var sortiraneOsobe = osobe.OrderBy(o => o.Ime).ToList();
            PrikaziOsobe(sortiraneOsobe);
        }

        private void btnEditPerson_Click(object sender, EventArgs e)
        {
            // Implementirajte logiku za uređivanje odabrane osobe
        }

        private void btnDeletePerson_Click(object sender, EventArgs e)
        {
            // Implementirajte logiku za brisanje odabrane osobe
        }
        private void btnSaveToCsv_Click(object sender, EventArgs e)
        {
            using (var sw = new StreamWriter("osobe.csv"))
            {
                foreach (var osoba in osobe)
                {
                    sw.WriteLine($"{osoba.Ime},{osoba.Prezime},{osoba.Uloga},{osoba.Aktivnost}");
                }
            }
        }

        private void btnLoadFromCsv_Click(object sender, EventArgs e)
        {
            using (var sr = new StreamReader("osobe.csv"))
            {
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    var parts = line.Split(',');
                    if (parts.Length == 4)
                    {
                        Osoba osoba = new Osoba { Ime = parts[0], Prezime = parts[1], Uloga = parts[2], Aktivnost = parts[3] };
                        osobe.Add(osoba);
                    }
                }
            }
            PrikaziOsobe(osobe);
        }
    }
}
