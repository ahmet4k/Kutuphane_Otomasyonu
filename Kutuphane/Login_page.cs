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
    public partial class login_page : Form
    {
        KutuphaneEntities1 db = new KutuphaneEntities1();
        public login_page()
        {
            InitializeComponent();
        }

      
        private void girisBtn_Click(object sender, EventArgs e)
        {

            try
            {
             string girisa = kAdi.Text;
             string girisb = sifre.Text;

            //linq sorgusu
            var admin = db.admin.Where(x => x.eMail.Equals(girisa) && x.sifre.Equals(girisb)).FirstOrDefault();
            var uye = db.uye.Where(x => x.eMail.Equals(girisa) && x.sifre.Equals(girisb)).FirstOrDefault();
            

            if (admin == null && uye == null)
            {
                MessageBox.Show("Kullanıcı Adı veya Şifre Hatalı");
            }
            else
            {
                if (admin != null)
                {
                    MessageBox.Show("Admin Girişi Başarılı");
                    admin_page adminForm = new admin_page(admin);
                    adminForm.Show();
                    this.Hide();
                }
                else if (uye != null)
                {
                        if( uye.uyelikDurumu == 1) // Burada "Durum" özelliğini üyenin durumunu temsil ettiğini varsayalım.
                        {
                            MessageBox.Show("Üye Girişi Başarılı");
                            user_page uyeForm = new user_page(uye);
                            uyeForm.Show();
                            this.Hide();
                        }
                        else
                        {
                            MessageBox.Show("Üye hesabınız aktif değil. Lüygrn Cezanızı ödeyin");
                        }
                    }
            }
            }
            catch (Exception)
            {

                MessageBox.Show("Veritabanı hatası. Bilgileri kontrol edin.");
            }
        }

        private void login_page_Load(object sender, EventArgs e)
        {

        }



        //Tasarım ile alakalı
        private void kAdi_Enter(object sender, EventArgs e)
        {
            if (kAdi.Text == "E-Mail")
            {
                kAdi.Text = "";
                kAdi.ForeColor = Color.Black;
            }
        }
        private void kAdi_Leave(object sender, EventArgs e)
        {
            if (kAdi.Text == "")
            {
                kAdi.Text = "E-Mail";
                kAdi.ForeColor = Color.FromArgb(173, 173, 173);
            }
        }
        private void sifre_Enter(object sender, EventArgs e)
        {
            if (sifre.Text == "Password")
            {
                sifre.Text = "";
                sifre.ForeColor = Color.Black;
            }
        }
        private void sifre_Leave(object sender, EventArgs e)
        {
            if (sifre.Text == "")
            {
                sifre.Text = "Password";
                sifre.ForeColor = Color.FromArgb(173, 173, 173);
            }
        }
        //Kaptmak için
        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
