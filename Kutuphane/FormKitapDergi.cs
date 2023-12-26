using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class FormKitapDergi : Form
    {
        KutuphaneEntities1 db = new KutuphaneEntities1 ();
        public FormKitapDergi()
        {
            InitializeComponent();
        }

        private void FormKitapDergi_Load(object sender, EventArgs e)
        {
            ListeleK();
            ListeleDergi();
        }
        private void ListeleK()
        {
            var aktifKitaplar = db.kitap.Where(k => k.kitapDurumu == 1).ToList();
            dataGridView1.DataSource = aktifKitaplar;


            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;

            //kalan kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ISBN";
            // DataGridViewColumn c = dataGridView1.Columns[0];
            // c.Width = 30;
            dataGridView1.Columns[1].HeaderText = "Kitap Adı";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Yazar";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 50;
            dataGridView1.Columns[3].HeaderText = "BasımTarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView1.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 50;
            dataGridView1.Columns[5].HeaderText = "Yayın Evi";
            DataGridViewColumn co = dataGridView1.Columns[5];
            co.Width = 50;
            dataGridView1.Columns[6].HeaderText = "Sayfa Sayısı";
            DataGridViewColumn x = dataGridView1.Columns[6];
            x.Width = 65;
            dataGridView1.Columns[7].HeaderText = "Fotoğraf";
            dataGridView1.Columns[8].HeaderText = "Açıklama";

            dataGridView1.Columns[9].Visible = false;
        }

        private void ListeleDergi()
        {
            var dergiler =db.dergi.ToList();
            dataGridView2.DataSource = dergiler;

            //kalan kolonların isimlerini düzenledik.
            dataGridView2.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn c = dataGridView2.Columns[0];
            c.Width = 50;
            dataGridView2.Columns[1].HeaderText = "Dergi Adı";
            DataGridViewColumn col = dataGridView2.Columns[1];
            col.Width = 50;
            dataGridView2.Columns[2].HeaderText = "Yayın Evi";
            DataGridViewColumn colum = dataGridView2.Columns[2];
            colum.Width = 50;
            dataGridView2.Columns[3].HeaderText = "Son Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView2.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView2.Columns[4];
            column.Width = 50;
            dataGridView2.Columns[5].HeaderText = "Açıklama";
            DataGridViewColumn co = dataGridView2.Columns[5];
            co.Width = 50;
            dataGridView2.Columns[6].HeaderText = "Fotoğraf";
            DataGridViewColumn x = dataGridView2.Columns[6];
            x.Width = 65;
            dataGridView2.Columns[7].Visible = false;
        }

 
        // ARAMA İŞLEMLERİ
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            string arama = textBox1.Text;
            var sonuc = from s in db.dergi
                        where s.ad.Contains(arama) || s.ISSN.ToString().Equals(arama) || s.kategori.Contains(arama)
                        select s;


            dataGridView2.DataSource = sonuc.ToList();
        }

        private void kitapAraTxt_TextChanged(object sender, EventArgs e)
        {
            string ara = kitapAraTxt.Text;
            var son = from s in db.kitap
                        where s.ad.Contains(ara) || s.yazar.Contains(ara) || s.kategori.Contains(ara)
                        select s;


            dataGridView1.DataSource = son.ToList();
        }
        //------------------------------------------------

        //TASARIMLAR
        private void textBox1_Enter(object sender, EventArgs e)
        {
            if (textBox1.Text == "Dergi Adı, ISSN, Kategori Adı")
            {
                textBox1.Text = "";
                textBox1.ForeColor = Color.Black;
            }
        }
        private void textBox1_Leave(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "Dergi Adı, ISSN, Kategori Adı";
                textBox1.ForeColor = Color.FromArgb(173, 173, 173);
            }
        }
        private void kitapAraTxt_Enter(object sender, EventArgs e)
        {
            if (kitapAraTxt.Text == "Kitap Adı, Yazar Adı, Kategori Adı")
            {
                kitapAraTxt.Text = "";
                kitapAraTxt.ForeColor = Color.Black;
            }
        }
        private void kitapAraTxt_Leave(object sender, EventArgs e)
        {
            if (kitapAraTxt.Text == "")
            {
                kitapAraTxt.Text = "Kitap Adı, Yazar Adı, Kategori Adı";
                kitapAraTxt.ForeColor = Color.FromArgb(173, 173, 173);
            }
        }
        //--------------------------------------------------------------------------------
    }
}
