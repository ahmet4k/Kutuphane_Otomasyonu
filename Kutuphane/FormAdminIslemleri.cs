using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kutuphane
{
    public partial class FormAdminIslemleri : Form
    {
        public FormAdminIslemleri()
        {
            InitializeComponent();
        }
        KutuphaneEntities1 db = new KutuphaneEntities1();

        private void FormAdminIslemleri_Load(object sender, EventArgs e)
        {
            Listele();

            //kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ID";
            DataGridViewColumn c = dataGridView1.Columns[0];
            c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Ad";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Soyad";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 60;
            dataGridView1.Columns[3].HeaderText = "E-Mail";
            DataGridViewColumn colu = dataGridView1.Columns[3];
            colu.Width = 60;
            dataGridView1.Columns[4].HeaderText = "Şifre";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 60;
            dataGridView1.Columns[5].HeaderText = "Telefon No";
            dataGridView1.Columns[6].HeaderText = "Adres";

            dataGridView1.Columns[7].Visible=false;          
        }

        //TEXTLERİ TEMİZLEME
        public void textTemizle()
        {
            adTxt.Text = "";
            soyadTxt.Text = "";
            telefonTxt.Text = "";
            sifreTxt.Text = "";
            emailTxt.Text = "";
            adresTxt.Text = "";
          
        }
        //-----------------------------

        //ADMİN LİSTELEME FONKSİYONU
        public void Listele()
        {
            var Adminler = db.admin.ToList();
            dataGridView1.DataSource = Adminler.ToList();
        }
        //------------------------------------------------------



        // TASARIM
        void gridStyle()
        {
            int i;
            for (i = 0; i <= dataGridView1.RowCount - 1; i++)
            {
                if (i % 2 == 0)
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.White;
                }
                else
                {
                    dataGridView1.Rows[i].DefaultCellStyle.BackColor = Color.FromKnownColor(KnownColor.Control);
                }
            }
        }
        private void dataGridView1_DataSourceChanged(object sender, EventArgs e)
        {
            gridStyle();
        }
        private void uyeAraTxt_Enter(object sender, EventArgs e)
        {
            if (uyeAraTxt.Text == "Ad, Soyad, E-Mail")
            {
                uyeAraTxt.Text = "";
                uyeAraTxt.ForeColor = Color.Black;
            }
        }
        private void uyeAraTxt_Leave(object sender, EventArgs e)
        {
            if (uyeAraTxt.Text == "")
            {
                uyeAraTxt.Text = "Ad, Soyad, E-Mail";
                uyeAraTxt.ForeColor = Color.FromArgb(171, 171, 171);
            }
        }
        //----------------------------------------------------------------------


        private void btnKaydet_Click(object sender, EventArgs e)
        {
            admin uyeler = new admin();          
            uyeler.ad = adTxt.Text;
            uyeler.soyad = soyadTxt.Text;
            uyeler.eMail = emailTxt.Text;
            uyeler.sifre = sifreTxt.Text;
            uyeler.telefon = telefonTxt.Text;
            uyeler.adres = adresTxt.Text;
            

            db.admin.Add(uyeler);
            db.SaveChanges();

            if (btnKaydet.Text=="Kaydet")
            {
                MessageBox.Show("Eklendi...");
            }else
            {
                MessageBox.Show("Added...");
            }
            
            Listele();
            textTemizle();
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (true)
                {
                int id = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
                var kullanici = db.admin.Where(x => x.adminID == id).FirstOrDefault();
                kullanici.ad = adTxt.Text;
                kullanici.soyad = soyadTxt.Text;
                kullanici.eMail = emailTxt.Text;
                kullanici.sifre = sifreTxt.Text;
                kullanici.telefon = telefonTxt.Text;
                kullanici.adres = adresTxt.Text;
                 if (btnGuncelle.Text == "Güncelle")
                  {
                        MessageBox.Show("Güncellendi...");
                  }
                  else
                  {
                        MessageBox.Show("Updated...");
                  }
                    

                db.SaveChanges();
                Listele();
                textTemizle();
                }          
            }
            catch (Exception)
            {

                MessageBox.Show("Veritabanına Hatası. Bilgileri kontrol edin.");
            }
            

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            int id = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
            var kullanici = db.admin.Where(x => x.adminID == id).FirstOrDefault();
            db.admin.Remove(kullanici);
            db.SaveChanges();
            if (btnSil.Text=="Sil")
            {
                MessageBox.Show("Silindi...");
            }
            else
            {
                MessageBox.Show("Deleted...");
            }
            Listele();
            textTemizle();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                adTxt.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                soyadTxt.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                emailTxt.Text = dataGridView1.CurrentRow.Cells[3].Value?.ToString();
                sifreTxt.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString();
                telefonTxt.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString();
                adresTxt.Text = dataGridView1.CurrentRow.Cells[6].Value?.ToString();
            }
        }


        private void uyeAraTxt_TextChanged(object sender, EventArgs e)
        {
            string aranan = uyeAraTxt.Text;
            var adminad =from s in db.admin where s.ad.Contains(aranan)  select s;
            var adminsoyad =from s in db.admin where s.soyad.Contains(aranan) select s;
            var adminemail =from s in db.admin where s.eMail.Contains(aranan) select s;

            dataGridView1.DataSource= adminad.ToList();
            dataGridView1.DataSource=adminsoyad.ToList();
            dataGridView1.DataSource=adminemail.ToList();   
            
        }

        private void tr_Click(object sender, EventArgs e)
        {
            
            dataGridView1.Columns[0].HeaderText = "ID";
            DataGridViewColumn c = dataGridView1.Columns[0];
            c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Ad";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Soyad";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 60;
            dataGridView1.Columns[3].HeaderText = "E-Mail";
            DataGridViewColumn colu = dataGridView1.Columns[3];
            colu.Width = 60;
            dataGridView1.Columns[4].HeaderText = "Şifre";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 60;
            dataGridView1.Columns[5].HeaderText = "Telefon No";
            dataGridView1.Columns[6].HeaderText = "Adres";

            dataGridView1.Columns[7].Visible = false;

            label1.Text = "Ad:";
            label2.Text = "Soyad:";
            label3.Text = "E-mail:";
            label4.Text = "Şifre:";
            label5.Text = "Telefon:";
            label6.Text = "Adres:";
            btnKaydet.Text = "Kaydet";
            btnSil.Text = "Sil";
            btnGuncelle.Text = "Güncelle";
           

        }

        private void eng_Click(object sender, EventArgs e)
        {
            //kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ID";
            //DataGridViewColumn c = dataGridView1.Columns[0];
            //c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Name";
           // DataGridViewColumn col = dataGridView1.Columns[1];
           // col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Surname";
           DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 70;
            dataGridView1.Columns[3].HeaderText = "E-Mail";
           // DataGridViewColumn colu = dataGridView1.Columns[3];
           // colu.Width = 60;
            dataGridView1.Columns[4].HeaderText = "Password";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 100;
            dataGridView1.Columns[5].HeaderText = "Phone";
           // DataGridViewColumn x = dataGridView1.Columns[5];
           // x.Width = 60;
            dataGridView1.Columns[6].HeaderText = "Address";
           // DataGridViewColumn a = dataGridView1.Columns[6];
          //  a.Width = 50;

            dataGridView1.Columns[7].Visible = false;

            label1.Text = "Name:";
            label2.Text = "Surname:";
            label3.Text = "E-mail:";
            label4.Text = "Password:";
            label5.Text = "Phone:";
            label6.Text = "Adress:";
            btnKaydet.Text = "Add";
            btnSil.Text = "Delete";
            btnGuncelle.Text = "Update";
            
        }
    }
}
