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
    public partial class FormUyeIslemleri : Form
    {
        public FormUyeIslemleri()
        {
            InitializeComponent();
        }
        KutuphaneEntities1 db = new KutuphaneEntities1();

        private void FormUyeIslemleri_Load(object sender, EventArgs e)
        {
            Listele();

           
            //id ve kayıtlar kolonunu gizledik.
           // dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[10].Visible = false;
          //  dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            // dataGridView1.Columns[9].Visible = false;

            //kalan kolonların isimlerini düzenledik.
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
            dataGridView1.Columns[7].HeaderText = "Üye Durumu";
            dataGridView1.Columns[9].HeaderText = "Para Cezası";



        }
        //TEXTLERİ TEMİZLEME
        public void textTemizle()
        {
            adTxt.Text = "";
            soyadTxt.Text = "";
            telefonTxt.Text = "";
            textBox1.Text = "";
            emailTxt.Text = "";
            adresTxt.Text = "";
            uyelikTxt.Text = "";
            cezaTxt.Text = "";
        }
        //-----------------------------

        //KULLANICI LİSTELEME FONKSİYONU
        public void Listele()
        {
            var uyeler = db.uye.ToList();
            dataGridView1.DataSource = uyeler.ToList();
        }
        //------------------------------------------------------


        // TASARIM
      
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
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                adTxt.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                soyadTxt.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                emailTxt.Text = dataGridView1.CurrentRow.Cells[3].Value?.ToString();
                textBox1.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString();
                telefonTxt.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString();
                adresTxt.Text = dataGridView1.CurrentRow.Cells[6].Value?.ToString();
                uyelikTxt.Text = dataGridView1.CurrentRow.Cells[7].Value?.ToString();
                cezaTxt.Text = dataGridView1.CurrentRow.Cells[9].Value?.ToString();
            }
        }
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            try
            {
                uye uyeler = new uye();

                uyeler.ad = adTxt.Text;
                uyeler.soyad = soyadTxt.Text;
                uyeler.eMail = emailTxt.Text;
                uyeler.sifre = textBox1.Text;
                uyeler.telefon = telefonTxt.Text;
                uyeler.adres = adresTxt.Text;
                uyeler.uyelikDurumu = (int)Convert.ToDouble(uyelikTxt.Text);
                uyeler.ParaCezasi = (int)Convert.ToDouble(cezaTxt.Text);

                db.uye.Add(uyeler);
                db.SaveChanges();
                if (btnKaydet.Text == "Kaydet")
                {
                    MessageBox.Show("Eklendi...");
                }
                else
                {
                    MessageBox.Show("Added...");
                }

                Listele();
                textTemizle();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
                var kullanici = db.uye.Where(x => x.uyeID == id).FirstOrDefault();

                if (kullanici != null)
                {

                    if (kullanici.islem.Any(i => i.emanetDurumu != "Alınan"))
                    {
                        db.uye.Remove(kullanici);
                        db.SaveChanges();

                        if (btnSil.Text == "Sil")
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
                    else
                    {
                       
                        MessageBox.Show("Üye silinemedi çünkü emanet verilmiş bir kitap var.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("Üye bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception )
            {
                MessageBox.Show("Hata oluştu");
            }
        }

        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                int id = Convert.ToInt16(dataGridView1.CurrentRow.Cells[0].Value);
                var kullanici = db.uye.Where(x => x.uyeID == id).FirstOrDefault();
                kullanici.ad = adTxt.Text;
                kullanici.soyad = soyadTxt.Text;
                kullanici.eMail = emailTxt.Text;
                kullanici.sifre = textBox1.Text;
                kullanici.telefon = telefonTxt.Text;
                kullanici.adres = adresTxt.Text;
                kullanici.uyelikDurumu = (int)Convert.ToDouble(uyelikTxt.Text);
                kullanici.ParaCezasi = (int)Convert.ToDouble(cezaTxt.Text);

                
                db.SaveChanges();
                if (btnGuncelle.Text == "Güncelle")
                {
                    MessageBox.Show("Güncellendi...");
                }
                else
                {
                    MessageBox.Show("Updated...");
                }
                Listele();
                textTemizle();

            }catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        //ÜYE ARAMA
        private void uyeAraTxt_TextChanged(object sender, EventArgs e)
        {
            textTemizle();
            string aranan=uyeAraTxt.Text;
            var sonuc = from s in db.uye
                        where s.ad.Contains(aranan) || s.soyad.Contains(aranan) || s.eMail.Contains(aranan)
                        select s;

           
            dataGridView1.DataSource = sonuc.ToList();
        }
        //---------------------------------------------------------

        private void tr_Click(object sender, EventArgs e)
        {
            //id ve kayıtlar kolonunu gizledik.
            // dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            //  dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            // dataGridView1.Columns[9].Visible = false;

            //kalan kolonların isimlerini düzenledik.
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
            dataGridView1.Columns[7].HeaderText = "Üye Durumu";
            dataGridView1.Columns[9].HeaderText = "Para Cezası";
            label1.Text = "Ad:";
            label2.Text = "Soyad:";
            label3.Text = "E-mail:";
            label4.Text = "Şifre:";
            label5.Text = "Telefon:";
            label6.Text = "Adres:";
            label8.Text = "Üyelik Durumu:";
            label7.Text = "Para Cezası:";
            btnKaydet.Text = "Kaydet";
            btnSil.Text = "Sil";
            btnGuncelle.Text = "Güncelle";
        }

        private void eng_Click(object sender, EventArgs e)
        {
            //id ve kayıtlar kolonunu gizledik.
            // dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[10].Visible = false;
            //  dataGridView1.Columns[7].Visible = false;
            dataGridView1.Columns[8].Visible = false;
            // dataGridView1.Columns[9].Visible = false;

            // kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ID";
            DataGridViewColumn c = dataGridView1.Columns[0];
            c.Width = 30;
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
            column.Width = 80;
            dataGridView1.Columns[5].HeaderText = "Phone";
            DataGridViewColumn x = dataGridView1.Columns[5];
             x.Width = 60;
            dataGridView1.Columns[6].HeaderText = "Address";
             DataGridViewColumn a = dataGridView1.Columns[6];
              a.Width = 70;

            dataGridView1.Columns[7].HeaderText = "Membership";
            dataGridView1.Columns[9].HeaderText = "Fine";
             DataGridViewColumn b= dataGridView1.Columns[9];
             b.Width = 40;

            label1.Text = "Name:";
            label2.Text = "Surname:";
            label3.Text = "E-mail:";
            label4.Text = "Password:";
            label5.Text = "Phone:";
            label6.Text = "Adrress:";
            label8.Text = "Membership Status:";
            label7.Text = "Fine:";
            btnKaydet.Text = "Add";
            btnSil.Text = "Delete";
            btnGuncelle.Text = "Update";
        }


        



    }
}
