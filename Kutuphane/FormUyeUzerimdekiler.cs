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
    public partial class FormUyeUzerimdekiler : Form
    {
        KutuphaneEntities1 db = new KutuphaneEntities1();

        private int UyeID;
        public FormUyeUzerimdekiler(int UyeID)
        {
            InitializeComponent();
            this.UyeID = UyeID;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           
        }

        private void FormUyeUzerimdekiler_Load(object sender, EventArgs e)
        {
            ListeleKitap();
            ListeleDergi();
        }


        private void ListeleKitap()
        {
            var kitaplar = db.islem
         .Where(i => i.uyeID == UyeID && i.ISSN== null && i.emanetDurumu == "Alınan")
         .Select(i => new
         {
             i.ISBN,
             KitapAd = i.kitap.ad,
             Yazar = i.kitap.yazar,           
             BaskıTarihi= i.kitap.baskiYili,
             Kategori= i.kitap.kategori,
             YayınEvi=i.kitap.yayinEvi,
             SayfaSayısı= i.kitap.sayfaSayisi,
             Fotograf = i.kitap.fotograf,
             Açıklama=i.kitap.aciklama,
             AlışTarih=i.alisTarih,
             SonTarih=i.sonTarihi
         })
         .ToList();

            dataGridView1.DataSource = kitaplar;

            dataGridView1.Columns[0].HeaderText = "ISBN";
            dataGridView1.Columns[1].HeaderText = "Kitap Adı";
            dataGridView1.Columns[2].HeaderText = "Yazar";
            dataGridView1.Columns[3].HeaderText = "Baskı Yılı";
            dataGridView1.Columns[4].HeaderText = "Kategori";
            dataGridView1.Columns[5].HeaderText = "Yayın Evi";
            dataGridView1.Columns[6].Visible = false;
            dataGridView1.Columns[7].HeaderText = "Fotoğraf";
            dataGridView1.Columns[8].Visible = false;
            dataGridView1.Columns[9].HeaderText = "Alış Tarihi";
            dataGridView1.Columns[10].HeaderText = "Son Tarihi";
            DataGridViewColumn col = dataGridView1.Columns[0];
            col.Width = 40;


        }

        private void ListeleDergi()
        {
            var dergiler = db.islem
                .Where(i => i.uyeID == UyeID && i.ISBN == null && i.emanetDurumu == "Alınan")
                .Select(i => new
            {
                i.ISSN,
                Dergi= i.dergi.ad,
                YayınEvi = i.dergi.YayınEvi,
                Kategori = i.dergi.kategori,
                Fotograf= i.dergi.fotograf,
                Acıklama=i.dergi.aciklama,
                AlışTarihi=i.alisTarih,
                SonTarih= i.sonTarihi
            }).ToList();
            dataGridView2.DataSource = dergiler;

            dataGridView2.Columns[0].HeaderText = "ISSN";           
            dataGridView2.Columns[1].HeaderText = "Dergi Adı";
            dataGridView2.Columns[2].HeaderText = "Yayın Evi";
            dataGridView2.Columns[3].HeaderText = "Kategori";
            dataGridView2.Columns[4].HeaderText = "Fotoğraf";
            dataGridView2.Columns[5].HeaderText = "Açıklama";
            dataGridView2.Columns[6].HeaderText = "Alış Tarihi";
            dataGridView2.Columns[7].HeaderText = "Son Tarihi";
        }
    }
}
