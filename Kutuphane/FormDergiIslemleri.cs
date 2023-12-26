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

namespace Kutuphane
{
    public partial class FormDergiIslemleri : Form
    {
        KutuphaneEntities1 db = new KutuphaneEntities1();
        public FormDergiIslemleri()
        {
            InitializeComponent();
        }

        private void FormDergiIslemleri_Load(object sender, EventArgs e)
        {
            Listele();
            //kalan kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ISSN";
             DataGridViewColumn c = dataGridView1.Columns[0];
             c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Dergi Adı";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Yayın Evi";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 50;
            dataGridView1.Columns[3].HeaderText = "Son Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView1.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 50;
            dataGridView1.Columns[5].HeaderText = "Açıklama";
            DataGridViewColumn co = dataGridView1.Columns[5];
            co.Width = 50;
            dataGridView1.Columns[6].HeaderText = "Fotoğraf";
            DataGridViewColumn x = dataGridView1.Columns[6];
            x.Width = 65;
            dataGridView1.Columns[7].Visible= false;
            
        }

        //KİTAP LİSTELEME FONKSİYONU
        public void Listele()
        {
            var Dergiler = db.dergi.ToList();
            dataGridView1.DataSource = Dergiler.ToList();
        }
        //------------------------------------------------------
        //TEXTLERİ TEMİZLEME
        public void textTemizle()
        {

            adTxt.Text = "";
            yayıneviTxt.Text = "";
            kategoriTxt.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            fotografPic.Image = null;
            aciklamaTxt.Text = "";
        }
        //----------------------------------------------------

        //RESMİ PİCTURE BOXA ATMA VE BOYUTUNU AYARLAMA
        private void btnResim_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog();
            opf.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";

            if (opf.ShowDialog() == DialogResult.OK)
            {
                fotografPic.Image = Image.FromFile(opf.FileName);
                fotografPic.Image.Tag = "";
                Bitmap bmp = new Bitmap(fotografPic.Image, 85, 105);
                fotografPic.Image = bmp;
            }
        }
        //-----------------------------------------------
        //DERGİ EKLEME
        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (fotografPic.Image == null)
            {
                MessageBox.Show("Lütfen bir resim seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    fotografPic.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);

                    dergi dergiler = new dergi();
                    dergiler.ad = adTxt.Text;
                    dergiler.YayınEvi = yayıneviTxt.Text;
                    dergiler.kategori = kategoriTxt.Text;
                    dergiler.sonTarih = dateTimePicker1.Value;                
                    dergiler.fotograf = ms.ToArray();
                    dergiler.aciklama = aciklamaTxt.Text;
                    db.dergi.Add(dergiler);
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //----------------------------------------------------------------------

        // ISBN VERİSİNE GÖRE SİLME İŞLEMİ
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int secilenISSN = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var sil = db.dergi.FirstOrDefault(x => x.ISSN == secilenISSN);

                    if (sil != null)
                    {
                        if (sil.islem.Any(i => i.emanetDurumu != "Alınan"))
                        {
                            db.dergi.Remove(sil);
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
                            MessageBox.Show("Dergi bir üyenin üzerinde olduğndan silinemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                       
                    }
                    else
                    {
                        MessageBox.Show("Seçili dergi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir dergi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //--------------------------------------------------

        //GÜNCELLEME İŞLEMİ
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int guncelle = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var dergiler = db.dergi.FirstOrDefault(x => x.ISSN == guncelle);

                    if (dergiler != null)
                    {
                        dergiler.ad = adTxt.Text;
                        dergiler.YayınEvi = yayıneviTxt.Text;
                        dergiler.kategori = kategoriTxt.Text;
                        dergiler.sonTarih = dateTimePicker1.Value;
                        dergiler.aciklama = aciklamaTxt.Text;

                        // Eğer fotografPic.Image null değilse ConvertImageToByteArray fonksiyonunu çağır
                        if (fotografPic.Image != null)
                        {
                            byte[] fotografData = ConvertImageToByteArray(fotografPic.Image);
                            dergiler.fotograf = fotografData;
                        }

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
                    }
                    else
                    {
                        MessageBox.Show("Seçili dergi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir dergi seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //-------------------------------------------------------



        // PictureBox'tan Byte Dizisine dönüştürme metodu
        private byte[] ConvertImageToByteArray(Image image)
        {
            if (image != null)
            {
                using (MemoryStream ms = new MemoryStream())
                {

                    using (Bitmap bmp = new Bitmap(image))
                    {
                        bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    return ms.ToArray();
                }
            }
            return null;
        }

        private void label2_Click(object sender, EventArgs e)
        {
            textTemizle();
        }
        //TASARIM...............
        private void kitapAraTxt_Enter(object sender, EventArgs e)
        {
            if (kitapAraTxt.Text == "Dergi Adı, ISSN, Kategori Adı")
            {
                kitapAraTxt.Text = "";
                kitapAraTxt.ForeColor = Color.Black;
            }
        }
        private void kitapAraTxt_Leave(object sender, EventArgs e)
        {
            if (kitapAraTxt.Text == "")
            {
                kitapAraTxt.Text = "Dergi Adı, ISSN, Kategori Adı";
                kitapAraTxt.ForeColor = Color.FromArgb(173, 173, 173);
            }
        }
        //--------------------------------------------------------------------

        // ARAMA İŞLEMİ
        private void kitapAraTxt_TextChanged(object sender, EventArgs e)
        {
            textTemizle();
            string arama = kitapAraTxt.Text;
            var sonuc = from s in db.dergi
                        where s.ad.Contains(arama) || s.YayınEvi.Contains(arama) || s.kategori.Contains(arama)
                        select s;


            dataGridView1.DataSource = sonuc.ToList();
        }

        //-------------------------------------------------------------
        //DATAYA TIKLAYINCA VERİLERİ TEXTLERE GETİRME
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                adTxt.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                yayıneviTxt.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
                kategoriTxt.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString();
                aciklamaTxt.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString();
                byte[] imageData = dataGridView1.CurrentRow.Cells[6].Value as byte[];
                if (imageData != null && imageData.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        fotografPic.Image = Image.FromStream(ms);
                    }
                }
            }
        }

        private void tr_Click(object sender, EventArgs e)
        {
            //kalan kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn c = dataGridView1.Columns[0];
            c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Dergi Adı";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Yayın Evi";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 50;
            dataGridView1.Columns[3].HeaderText = "Son Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView1.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 50;
            dataGridView1.Columns[5].HeaderText = "Açıklama";
            DataGridViewColumn co = dataGridView1.Columns[5];
            co.Width = 50;
            dataGridView1.Columns[6].HeaderText = "Fotoğraf";
            DataGridViewColumn x = dataGridView1.Columns[6];
            x.Width = 65;
            dataGridView1.Columns[7].Visible = false;

            label2.Text = "Dergi ad:";
            label3.Text = "Yayın evi:";
            label9.Text = "Kategori";
            label5.Text = "Son Tarih:";
            label7.Text = "Fotoğraf:";
            label8.Text = "Açıklama:";
            btnKaydet.Text = "Kaydet";
            btnGuncelle.Text = "Güncelle";
            btnResim.Text = "Resim Seç";
            btnSil.Text = "Sil";


        }

        private void eng_Click(object sender, EventArgs e)
        {
            //kalan kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn c = dataGridView1.Columns[0];
            c.Width = 50;
            dataGridView1.Columns[1].HeaderText = "Magazine";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Publisher";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 50;
            dataGridView1.Columns[3].HeaderText = "Due Date";
            DataGridViewColumn colu = dataGridView1.Columns[3];
            colu.Width = 50;
            dataGridView1.Columns[4].HeaderText = "Category";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 50;
            dataGridView1.Columns[5].HeaderText = "Description";
            DataGridViewColumn co = dataGridView1.Columns[5];
            co.Width = 50;
            dataGridView1.Columns[6].HeaderText = "Photo";
            DataGridViewColumn x = dataGridView1.Columns[6];
            x.Width = 65;
            dataGridView1.Columns[7].Visible = false;

            label2.Text = "Magazine:";
            label3.Text = "Publisher:";
            label9.Text = "Category:";
            label5.Text = "Due Date:";
            label7.Text = "Photo:";
            label8.Text = "Description:";
            btnKaydet.Text = "Add";
            btnGuncelle.Text = "Update";
            btnResim.Text = "Select Image";
            btnSil.Text = "Remove";
        }
        //---------------------------------------------------------

    }
}
