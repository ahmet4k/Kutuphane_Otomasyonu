using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kutuphane
{
    public partial class FormKitapIslemleri : Form
    {
        
        public FormKitapIslemleri()
        {
            InitializeComponent();
            
        }
        KutuphaneEntities1 db = new KutuphaneEntities1();

        MemoryStream ms = new MemoryStream();

        private void FormKitapIslemleri_Load(object sender, EventArgs e)
        {
            Listele();

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

            dataGridView1.Columns[9].HeaderText = "Kitap aktifliği";
            DataGridViewColumn a = dataGridView1.Columns[9];
            a.Width = 45;
        }

        //KİTAP LİSTELEME FONKSİYONU
        public void Listele()
        {
            var Kitaplar = db.kitap.ToList();
            dataGridView1.DataSource = Kitaplar.ToList();
        }
        //------------------------------------------------------
        //TEXTLERİ TEMİZLEME
        public void textTemizle()
        {
            
            adTxt.Text = "";
            yazarTxt.Text = "";
            kategoriTxt.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            numericUpDown1.Value = 0;
            yayinEviTxt.Text = "";
            sayfaSayisiTxt.Text = "";
            fotografPic.Image = null;
            aciklamaTxt.Text = "";
        }
        //----------------------------------------------------


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
        //--------------------------------------------------------
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
        //KAYDETME İŞLEMİ
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

                    kitap kitaplar = new kitap();
                    kitaplar.ad = adTxt.Text;
                    kitaplar.yazar = yazarTxt.Text;
                    kitaplar.kategori = kategoriTxt.Text;
                    kitaplar.baskiYili = dateTimePicker1.Value;
                    kitaplar.yayinEvi = yayinEviTxt.Text;
                    kitaplar.sayfaSayisi = sayfaSayisiTxt.Text;
                    kitaplar.kitapDurumu = (byte)numericUpDown1.Value;
                    kitaplar.fotograf = ms.ToArray();
                    kitaplar.aciklama = aciklamaTxt.Text;
               

                    db.kitap.Add(kitaplar);
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
        //---------------------------------------------------------------------
        //GÜNCELLEME İŞLEMİ
        private void btnGuncelle_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int guncelle = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var kitaplar = db.kitap.FirstOrDefault(x => x.ISBN == guncelle);

                    // Eğer kitaplar null değilse devam et
                    if (kitaplar != null)
                    {
                        kitaplar.ad = adTxt.Text;
                        kitaplar.yazar = yazarTxt.Text;
                        kitaplar.kategori = kategoriTxt.Text;
                        kitaplar.baskiYili = dateTimePicker1.Value;
                        kitaplar.yayinEvi = yayinEviTxt.Text;
                        kitaplar.sayfaSayisi = sayfaSayisiTxt.Text;
                        kitaplar.kitapDurumu = (byte)numericUpDown1.Value;
                        kitaplar.aciklama = aciklamaTxt.Text;

                        // Eğer fotografPic.Image null değilse ConvertImageToByteArray fonksiyonunu çağır
                        if (fotografPic.Image != null)
                        {
                            byte[] fotografData = ConvertImageToByteArray(fotografPic.Image);
                            kitaplar.fotograf = fotografData;
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
                        MessageBox.Show("Seçili kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //-------------------------------------------------------

        // ISBN VERİSİNE GÖRE SİLME İŞLEMİ
        private void btnSil_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow != null)
                {
                    int secilenISBN = (int)dataGridView1.CurrentRow.Cells[0].Value;
                    var sil = db.kitap.FirstOrDefault(x => x.ISBN == secilenISBN);

                    if (sil != null)
                    {
                        if (sil.islem.Any(i => i.emanetDurumu != "Alınan"))
                        {
                            db.kitap.Remove(sil);
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
                            MessageBox.Show("Kitap bir üyenin üzerinde olduğndan silinemez", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Seçili kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Lütfen bir kitap seçin.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata oluştu: " + ex.Message, "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
 
        //--------------------------------------------------

        //DATAYA TIKLAYINCA VERİLERİ TEXTLERE GETİRME
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                adTxt.Text = dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                yazarTxt.Text = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
                kategoriTxt.Text = dataGridView1.CurrentRow.Cells[4].Value?.ToString();
                dateTimePicker1.Value = Convert.ToDateTime(dataGridView1.CurrentRow.Cells[3].Value);
                yayinEviTxt.Text = dataGridView1.CurrentRow.Cells[5].Value?.ToString();
                sayfaSayisiTxt.Text = dataGridView1.CurrentRow.Cells[6].Value?.ToString();
                byte[] imageData = dataGridView1.CurrentRow.Cells[7].Value as byte[];
                if (imageData != null && imageData.Length > 0)
                {
                    using (MemoryStream ms = new MemoryStream(imageData))
                    {
                        fotografPic.Image = Image.FromStream(ms);
                    }
                }
                aciklamaTxt.Text = dataGridView1.CurrentRow.Cells[8].Value?.ToString();
                numericUpDown1.Value = Convert.ToInt32(dataGridView1.CurrentRow.Cells[9].Value);
            }
        }
        //---------------------------------------------------------

        
        private void label2_Click(object sender, EventArgs e)
        {
            textTemizle();
            Listele();
        }
        //TASARIM...............
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


        //--------------------------------------------------------------------

        // ARAMA İŞLEMİ
        private void kitapAraTxt_TextChanged(object sender, EventArgs e)
        {
            textTemizle();
            string arama = kitapAraTxt.Text;
            var sonuc = from s in db.kitap
                        where s.ad.Contains(arama) || s.yazar.Contains(arama) || s.kategori.Contains(arama)
                        select s;

            
            dataGridView1.DataSource = sonuc.ToList();
        }

        //-------------------------------------------------------------


        //İNGİLİZCE TÜRKÇE ÇEVİRİ
        private void eng_Click(object sender, EventArgs e)
        {
            dataGridView1.Columns[10].Visible = false;
            dataGridView1.Columns[11].Visible = false;

            //kalan kolonların isimlerini düzenledik.
            dataGridView1.Columns[0].HeaderText = "ISBN";
            // DataGridViewColumn c = dataGridView1.Columns[0];
            // c.Width = 30;
            dataGridView1.Columns[1].HeaderText = "Book";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Writer";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 50;
            dataGridView1.Columns[3].HeaderText = "Publication Year";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView1.Columns[4].HeaderText = "Category";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 70;
            dataGridView1.Columns[5].HeaderText = "Publisher";
            DataGridViewColumn co = dataGridView1.Columns[5];
            co.Width = 70;
            dataGridView1.Columns[6].HeaderText = "Pages";
            DataGridViewColumn x = dataGridView1.Columns[6];
            x.Width = 65;
            dataGridView1.Columns[7].HeaderText = "Photo";
            dataGridView1.Columns[8].HeaderText = "Description";

            dataGridView1.Columns[9].HeaderText = "Book Activity";
            DataGridViewColumn a = dataGridView1.Columns[9];
            a.Width = 45;


            label2.Text = "Book Name:";
            label3.Text = "Writer:";
            label9.Text = "Category";
            label4.Text = "Publication Year:";
            label5.Text = "Publisher:";
            label6.Text = "Pages:";
            label1.Text = "Book Activity:";
            label7.Text = "Photo:";
            label8.Text = "Description:";

            btnKaydet.Text = "Add";
            btnGuncelle.Text = "Update";
            btnResim.Text = "Select Image";
            btnSil.Text = "Remove";
        }
        private void tr_Click(object sender, EventArgs e)
        {

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

            dataGridView1.Columns[9].HeaderText = "Kitap aktifliği";
            DataGridViewColumn a = dataGridView1.Columns[9];
            a.Width = 45;

            label2.Text = "Kitap Adı:";
            label3.Text = "Yazar:";
            label9.Text = "Kategori:";
            label4.Text = "Baskı Yılı:";
            label5.Text = "Yayın Evi:";
            label6.Text = "Sayfa Sayısı:";
            label1.Text = "Kitap Aktifliği:";
            label7.Text = "Fotoğraf:";
            label8.Text = "Açıklama:";

            btnKaydet.Text = "Kaydet";
            btnGuncelle.Text = "Güncelle";
            btnResim.Text = "Resim Seç";
            btnSil.Text = "Sil";

        }
        //----------------------------------------------------------
    }
}