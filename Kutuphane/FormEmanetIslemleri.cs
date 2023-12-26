using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class FormEmanetIslemleri : Form
    {
        KutuphaneEntities1 db = new KutuphaneEntities1();

        private int adminID;
        public FormEmanetIslemleri(int adminID)
        {
            InitializeComponent();
            this.adminID = adminID;
        }


        private void FormEmanetIslemleri_Load(object sender, EventArgs e)
        {
            yetkiliIDTxt.Text = adminID.ToString();
            Listelekitap();
            ListeleKullanıcı();
            ListeleDergi();
            ListeleEmanet();
        }

        //LİSTELEME FONKSİYONLARI
        public void Listelekitap()
        {
            var Kitaplar = db.kitap.ToList();
            dataGridView2.DataSource = Kitaplar.ToList();

            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;

            //kalan kolonların isimlerini düzenledik.
            dataGridView2.Columns[0].HeaderText = "ISBN";
            // DataGridViewColumn c = dataGridView1.Columns[0];
            // c.Width = 30;
            dataGridView2.Columns[1].HeaderText = "Kitap Adı";
            DataGridViewColumn col = dataGridView2.Columns[1];
            col.Width = 50;
            dataGridView2.Columns[2].HeaderText = "Yazar";
            DataGridViewColumn colum = dataGridView2.Columns[2];
            colum.Width = 50;
            dataGridView2.Columns[3].HeaderText = "Basım Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView2.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView2.Columns[4];
            column.Width = 50;
            dataGridView2.Columns[5].HeaderText = "Yayın Evi";
            DataGridViewColumn co = dataGridView2.Columns[5];
            co.Width = 50;
            dataGridView2.Columns[6].HeaderText = "Sayfa Sayısı";
            DataGridViewColumn x = dataGridView2.Columns[6];
            x.Width = 65;
            dataGridView2.Columns[7].HeaderText = "Fotoğraf";
            dataGridView2.Columns[8].HeaderText = "Açıklama";

            dataGridView2.Columns[9].HeaderText = "Kitap aktifliği";
            DataGridViewColumn a = dataGridView2.Columns[9];
            a.Width = 45;
        }

        public void ListeleKullanıcı()
        {
            var uyeler = db.uye.ToList();
            dataGridView1.DataSource = uyeler.ToList();


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
        public void ListeleDergi()
        {
            var dergiler = db.dergi.ToList();
            dataGridView4.DataSource = dergiler.ToList();

            //kalan kolonların isimlerini düzenledik.
            dataGridView4.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn c = dataGridView4.Columns[0];
            c.Width = 50;
            dataGridView4.Columns[1].HeaderText = "Dergi Adı";
            DataGridViewColumn col = dataGridView4.Columns[1];
            col.Width = 50;
            dataGridView4.Columns[2].HeaderText = "Yayın Evi";
            DataGridViewColumn colum = dataGridView4.Columns[2];
            colum.Width = 50;
            dataGridView4.Columns[3].HeaderText = "Son Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView4.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn column = dataGridView4.Columns[4];
            column.Width = 50;
            dataGridView4.Columns[5].HeaderText = "Açıklama";
            DataGridViewColumn co = dataGridView4.Columns[5];
            co.Width = 50;
            dataGridView4.Columns[6].HeaderText = "Fotoğraf";
            DataGridViewColumn x = dataGridView4.Columns[6];
            x.Width = 65;
            dataGridView4.Columns[7].Visible = false;

        }
        public void ListeleEmanet()
        {
            var islemler = db.islem.ToList();
            dataGridView3.DataSource = islemler.ToList();

            dataGridView3.Columns[0].HeaderText = "İşlem ID";
            dataGridView3.Columns[1].HeaderText = "Üye ID";
            dataGridView3.Columns[2].HeaderText = "Son Getirme Tarihi";
            dataGridView3.Columns[3].HeaderText = "Admin ID";
            dataGridView3.Columns[4].HeaderText = "ISSN";
            dataGridView3.Columns[5].HeaderText = "ISBN";
            dataGridView3.Columns[6].HeaderText = "Emanet Durumu";
            dataGridView3.Columns[7].HeaderText = "Alış Tarihi";


            dataGridView3.Columns[8].Visible = false;
            dataGridView3.Columns[9].Visible = false;
            dataGridView3.Columns[10].Visible = false;
            dataGridView3.Columns[11].Visible = false;


        }
        //---------------------------------------------------------------


        //DATAYA TIKLAYINCA TEXTE ÇEKME
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.CurrentRow != null && e.RowIndex >= 0)
            {
                uyeIDTxt.Text = dataGridView1.CurrentRow.Cells[0].Value?.ToString();
                durumTxt.Text = dataGridView1.CurrentRow.Cells[7].Value?.ToString();
            }

        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView2.CurrentRow != null && e.RowIndex >= 0)
            {
                kitapIDTxt.Text = dataGridView2.CurrentRow.Cells[0].Value?.ToString();
            }

        }

        private void dataGridView4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView4 != null && e.RowIndex >= 0)
            {
                textBox2.Text = dataGridView4.CurrentRow.Cells[0].Value?.ToString();
            }
        }
        //------------------------------------------------------
        //ARAMA İŞLEMLERİ
        private void kitapAraTxt_TextChanged(object sender, EventArgs e)
        {
            string arama = kitapAraTxt.Text;
            var kitapsonuc = from s in db.kitap
                             where s.ad.Contains(arama) || s.yazar.Contains(arama) || s.kategori.Contains(arama)
                             select s;


            dataGridView2.DataSource = kitapsonuc.ToList();
        }

        private void uyeAraTxt_TextChanged(object sender, EventArgs e)
        {
            string aranan = uyeAraTxt.Text;
            var uyesonuc = from s in db.uye
                           where s.ad.Contains(aranan) || s.soyad.Contains(aranan) || s.eMail.Contains(aranan)
                           select s;


            dataGridView1.DataSource = uyesonuc.ToList();
        }

        private void dergiAraTxt_TextChanged(object sender, EventArgs e)
        {
            string ara = dergiAraTxt.Text;
            var dergisonuc = from s in db.dergi
                             where s.ad.Contains(ara) || s.YayınEvi.Contains(ara) || s.kategori.Contains(ara)
                             select s;


            dataGridView4.DataSource = dergisonuc.ToList();
        }
        //-------------------------------------------------------------------


        //TASARIM İŞLEMLERİ
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


        private void dergiAraTxt_Enter(object sender, EventArgs e)
        {
            if (dergiAraTxt.Text == "Dergi Adı, ISSN, Kategori Adı")
            {
                dergiAraTxt.Text = "";
                dergiAraTxt.ForeColor = Color.Black;
            }
        }
        private void dergiAraTxt_Leave(object sender, EventArgs e)
        {
            if (dergiAraTxt.Text == "")
            {
                dergiAraTxt.Text = "Dergi Adı, ISSN, Kategori Adı";
                dergiAraTxt.ForeColor = Color.FromArgb(173, 173, 173);
            }
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
        //-------------------------------------------------------------

        //TEMİZLEME İŞLEMİ
        private void verTemizleBtn_Click(object sender, EventArgs e)
        {
          
            uyeIDTxt.Text = "";
            durumTxt.Text = "";
            kitapIDTxt.Text = "";
            textBox2.Text = "";
            dateTimePicker1.Value = DateTime.Now;
            dateTimePicker2.Value = DateTime.Now;
            ListeleDergi();
            ListeleEmanet();
            Listelekitap();
            ListeleKullanıcı();
        }

        //------------------------------------------------------------------
        //KİTAP VERME İŞLEMLERİ FONKSİYONLARI
        private bool KitapEmanetMumkunMu(int kitapID)
        {
            // Verilen kitap ID'si ile zaten alınmış emanetleri kontrol et
            return !db.islem.Any(e => e.ISBN == kitapID && e.emanetDurumu == "Alınan");
        }

        private bool DergiEmanetMumkunMu(int dergiID)
        {
            // Verilen dergi ID'si ile zaten alınmış emanetleri kontrol et
            return !db.islem.Any(e => e.ISSN == dergiID && e.emanetDurumu == "Alınan");
        }
        private void kitapEmanetEt()
        {
            int kitapID = Convert.ToInt32(kitapIDTxt.Text);
            int uyeID = Convert.ToInt32(uyeIDTxt.Text);
            int yetkiliID = Convert.ToInt32(yetkiliIDTxt.Text);
            if (KitapEmanetMumkunMu(kitapID))
            {
                islem yenislem = new islem();
                yenislem.ISBN = kitapID;
                yenislem.uyeID = uyeID;
                yenislem.adminID = yetkiliID;
                yenislem.alisTarih = dateTimePicker1.Value;
                yenislem.sonTarihi = dateTimePicker2.Value;
                yenislem.emanetDurumu = "Alınan";

                // Kitap var mı kontrol et
                var kitapVarMi = db.kitap.Any(k => k.ISBN == kitapID);

                if (kitapVarMi)
                {
                    db.islem.Add(yenislem);
                    db.SaveChanges();

                    MessageBox.Show("Kitap emanet edildi.");

                    ListeleDergi();
                    ListeleEmanet();
                    Listelekitap();
                    ListeleKullanıcı();
                }
                else
                {
                    MessageBox.Show("Kitap bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bu kitap zaten başka bir kullanıcı tarafından alınmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            
        
        }
        private void dergiEmanetEt()
        {
            int dergiID = Convert.ToInt32(textBox2.Text);
            int uyeID = Convert.ToInt32(uyeIDTxt.Text);
            int yetkiliID = Convert.ToInt32(yetkiliIDTxt.Text);
            if (DergiEmanetMumkunMu(dergiID))
            {
                islem yenislem = new islem();
                yenislem.ISSN = dergiID;
                yenislem.uyeID = uyeID;
                yenislem.adminID = yetkiliID;
                yenislem.alisTarih = dateTimePicker1.Value;
                yenislem.sonTarihi = dateTimePicker2.Value;
                yenislem.emanetDurumu = "Alınan";

                // Dergi var mı kontrol et
                var dergiVarMi = db.dergi.Any(d => d.ISSN == dergiID);

                if (dergiVarMi)
                {
                    db.islem.Add(yenislem);
                    db.SaveChanges();

                    MessageBox.Show("Dergi emanet edildi.");

                    ListeleDergi();
                    ListeleEmanet();
                    Listelekitap();
                    ListeleKullanıcı();
                }
                else
                {
                    MessageBox.Show("Dergi bulunamadı.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Bu dergi zaten başka bir kullanıcı tarafından alınmış.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //------------------------------------------------------------------------

        //KİTAP VER
        private void verBtn_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(kitapIDTxt.Text) && !string.IsNullOrEmpty(uyeIDTxt.Text))
            {
                if (durumTxt.Text == "1")
                {
                    try
                    {
                        kitapEmanetEt();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Geçersiz sayısal format.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kişi cezalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Eksik veya geçersiz bilgi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnDergiEmanetEt_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox2.Text) && !string.IsNullOrEmpty(uyeIDTxt.Text))
            {
                if (durumTxt.Text == "1")
                {
                    try
                    {
                        dergiEmanetEt();
                    }
                    catch (FormatException)
                    {
                        MessageBox.Show("Geçersiz sayısal format.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Kişi cezalı.", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {
                MessageBox.Show("Eksik veya geçersiz bilgi.", "Hata", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void alBtn_Click(object sender, EventArgs e)
        {
            int secilenIslemID = Convert.ToInt16(dataGridView3.CurrentRow.Cells[0].Value);
            var islem = db.islem.Where(x => x.islemID == secilenIslemID).FirstOrDefault();
            db.islem.Remove(islem);
            db.SaveChanges();


            ListeleEmanet();          
        }


        // ÇEVİRİ İŞLEMLERİ
        private void tr_Click(object sender, EventArgs e)
        {
            label24.Text = "Yetkili ID :";
            label23.Text = "Uye ID :";
            label22.Text = "Kitap ISBN :";
            label2.Text = "Dergi ISSN :";
            label3.Text = "Alış Tarihi :";
            label1.Text = "Son Tarihi :";
            verTemizleBtn.Text = "Temizle";
            verBtn.Text = "Kitap Ver";
            alBtn.Text = "Teslim Al";
            btnDergiEmanetEt.Text = "Dergi Ver";

            //ÜYELER ÇEVİRİ

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



            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;

            // KİTAP ÇEVİRME İŞLEMİ

            //kalan kolonların isimlerini düzenledik.
            dataGridView2.Columns[0].HeaderText = "ISBN";
            // DataGridViewColumn c = dataGridView1.Columns[0];
            // c.Width = 30;
            dataGridView2.Columns[1].HeaderText = "Kitap Adı";
            DataGridViewColumn t = dataGridView2.Columns[1];
            t.Width = 50;
            dataGridView2.Columns[2].HeaderText = "Yazar";
            DataGridViewColumn f = dataGridView2.Columns[2];
            f.Width = 50;
            dataGridView2.Columns[3].HeaderText = "Basım Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView2.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn b = dataGridView2.Columns[4];
            b.Width = 50;
            dataGridView2.Columns[5].HeaderText = "Yayın Evi";
            DataGridViewColumn co = dataGridView2.Columns[5];
            co.Width = 50;
            dataGridView2.Columns[6].HeaderText = "Sayfa Sayısı";
            DataGridViewColumn x = dataGridView2.Columns[6];
            x.Width = 65;
            dataGridView2.Columns[7].HeaderText = "Fotoğraf";
            dataGridView2.Columns[8].HeaderText = "Açıklama";

            dataGridView2.Columns[9].HeaderText = "Kitap aktifliği";
            DataGridViewColumn a = dataGridView2.Columns[9];
            a.Width = 45;





            // EMANET ÇEVİRİ İŞLEMLERİ
            dataGridView3.Columns[0].HeaderText = "İşlem ID";
            dataGridView3.Columns[1].HeaderText = "Üye ID";
            dataGridView3.Columns[2].HeaderText = "Son Getirme Tarihi";
            dataGridView3.Columns[3].HeaderText = "Admin ID";
            dataGridView3.Columns[4].HeaderText = "ISSN";
            dataGridView3.Columns[5].HeaderText = "ISBN";
            dataGridView3.Columns[6].HeaderText = "Emanet Durumu";
            dataGridView3.Columns[7].HeaderText = "Alış Tarihi";


            dataGridView3.Columns[8].Visible = false;
            dataGridView3.Columns[9].Visible = false;
            dataGridView3.Columns[10].Visible = false;
            dataGridView3.Columns[11].Visible = false;


            // DERGİ ÇEVİRİ İŞLEMLERİ

            //kalan kolonların isimlerini düzenledik.
            dataGridView4.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn p = dataGridView4.Columns[0];
            p.Width = 50;
            dataGridView4.Columns[1].HeaderText = "Dergi Adı";
            DataGridViewColumn r = dataGridView4.Columns[1];
            r.Width = 50;
            dataGridView4.Columns[2].HeaderText = "Yayın Evi";
            DataGridViewColumn y = dataGridView4.Columns[2];
            y.Width = 50;
            dataGridView4.Columns[3].HeaderText = "Son Tarihi";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView4.Columns[4].HeaderText = "Kategori";
            DataGridViewColumn u = dataGridView4.Columns[4];
            u.Width = 50;
            dataGridView4.Columns[5].HeaderText = "Açıklama";
            DataGridViewColumn i = dataGridView4.Columns[5];
            i.Width = 50;
            dataGridView4.Columns[6].HeaderText = "Fotoğraf";
            DataGridViewColumn n = dataGridView4.Columns[6];
            n.Width = 65;
            dataGridView4.Columns[7].Visible = false;


        }

        private void eng_Click(object sender, EventArgs e)
        {
            label24.Text = "Admin ID :";
            label23.Text = "User ID :";
            label22.Text = "Book ISBN :";
            label2.Text = "Magazine ISSN :";
            label3.Text = "Purchase Date :";
            label1.Text = "Due Date :";
            verTemizleBtn.Text = "Clear";
            verBtn.Text = "Lend a Book";
            alBtn.Text = "Receive";
            btnDergiEmanetEt.Text = "Lend a Magazine";

            //ÜYELER ÇEVİRİ

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
            dataGridView1.Columns[1].HeaderText = "Name";
            DataGridViewColumn col = dataGridView1.Columns[1];
            col.Width = 50;
            dataGridView1.Columns[2].HeaderText = "Surname";
            DataGridViewColumn colum = dataGridView1.Columns[2];
            colum.Width = 60;
            dataGridView1.Columns[3].HeaderText = "E-Mail";
            DataGridViewColumn colu = dataGridView1.Columns[3];
            colu.Width = 60;
            dataGridView1.Columns[4].HeaderText = "Password";
            DataGridViewColumn column = dataGridView1.Columns[4];
            column.Width = 60;
            dataGridView1.Columns[5].HeaderText = "Phone";
            dataGridView1.Columns[6].HeaderText = "Address";
            dataGridView1.Columns[7].HeaderText = "Status";
            dataGridView1.Columns[9].HeaderText = "Fine";



            dataGridView2.Columns[10].Visible = false;
            dataGridView2.Columns[11].Visible = false;

            // KİTAP ÇEVİRME İŞLEMİ

            //kalan kolonların isimlerini düzenledik.
            dataGridView2.Columns[0].HeaderText = "ISBN";
            // DataGridViewColumn c = dataGridView1.Columns[0];
            // c.Width = 30;
            dataGridView2.Columns[1].HeaderText = "Book";
            DataGridViewColumn t = dataGridView2.Columns[1];
            t.Width = 50;
            dataGridView2.Columns[2].HeaderText = "Author";
            DataGridViewColumn f = dataGridView2.Columns[2];
            f.Width = 50;
            dataGridView2.Columns[3].HeaderText = "Publication Date";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView2.Columns[4].HeaderText = "Category";
            DataGridViewColumn b = dataGridView2.Columns[4];
            b.Width = 50;
            dataGridView2.Columns[5].HeaderText = "Publisher";
            DataGridViewColumn co = dataGridView2.Columns[5];
            co.Width = 50;
            dataGridView2.Columns[6].HeaderText = "Pages";
            DataGridViewColumn x = dataGridView2.Columns[6];
            x.Width = 65;
            dataGridView2.Columns[7].HeaderText = "Photo";
            dataGridView2.Columns[8].HeaderText = "Description";

            dataGridView2.Columns[9].HeaderText = "Activity";
            DataGridViewColumn a = dataGridView2.Columns[9];
            a.Width = 45;





            // EMANET ÇEVİRİ İŞLEMLERİ
            dataGridView3.Columns[0].HeaderText = "Transaction ID";
            dataGridView3.Columns[1].HeaderText = "Member ID";
            dataGridView3.Columns[2].HeaderText = "Due Return Date";
            dataGridView3.Columns[3].HeaderText = "Admin ID";
            dataGridView3.Columns[4].HeaderText = "ISSN";
            dataGridView3.Columns[5].HeaderText = "ISBN";
            dataGridView3.Columns[6].HeaderText = "Loan Status";
            dataGridView3.Columns[7].HeaderText = "Purchase Date";


            dataGridView3.Columns[8].Visible = false;
            dataGridView3.Columns[9].Visible = false;
            dataGridView3.Columns[10].Visible = false;
            dataGridView3.Columns[11].Visible = false;


            // DERGİ ÇEVİRİ İŞLEMLERİ

            //kalan kolonların isimlerini düzenledik.
            dataGridView4.Columns[0].HeaderText = "ISSN";
            DataGridViewColumn p = dataGridView4.Columns[0];
            p.Width = 50;
            dataGridView4.Columns[1].HeaderText = "Magazine";
            DataGridViewColumn r = dataGridView4.Columns[1];
            r.Width = 50;
            dataGridView4.Columns[2].HeaderText = "Publisher";
            DataGridViewColumn y = dataGridView4.Columns[2];
            y.Width = 50;
            dataGridView4.Columns[3].HeaderText = "Due Date";
            //DataGridViewColumn colu = dataGridView1.Columns[3];
            //colu.Width = 70;
            dataGridView4.Columns[4].HeaderText = "Category";
            DataGridViewColumn u = dataGridView4.Columns[4];
            u.Width = 50;
            dataGridView4.Columns[5].HeaderText = "Description";
            DataGridViewColumn i = dataGridView4.Columns[5];
            i.Width = 50;
            dataGridView4.Columns[6].HeaderText = "Photo";
            DataGridViewColumn n = dataGridView4.Columns[6];
            n.Width = 65;
            dataGridView4.Columns[7].Visible = false;
        }
        //-------------------------------------------------

    }
}

