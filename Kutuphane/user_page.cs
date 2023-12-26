using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class user_page : Form
    {
        private uye Uye;
        public int uyeID => Uye.uyeID;
        public user_page(uye uye)
        {
            InitializeComponent();
            this.Uye = uye;
        }
        
        private void user_page_Load(object sender, EventArgs e)
        {
            uyeAd.Text = $"{Uye.ad} {Uye.soyad}";
            uyeMail.Text = Uye.eMail;
        }

        // YENİ AÇILAN FORMUN PANELCONTENT İÇİNDE YER ALMASI İÇİN AYARLAMALAR
        private void AbrirFormPanel(object Formhijo)
        {
            if (this.PanelContent.Controls.Count > 0)
            {
                this.PanelContent.Controls.RemoveAt(0);
            }
            Form fh = Formhijo as Form;
            fh.TopLevel = false;
            fh.Dock = DockStyle.Fill;
            this.PanelContent.Controls.Add(fh);
            this.PanelContent.Tag = fh;
            fh.Show();

        }
        //------------------------------------------------------------

        private void button1_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormKitapDergi());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormUyeUzerimdekiler(uyeID));
        }

        // Uygulamayı yukardan tutup istediğimiz yere sürükleme ( --> using System.Runtime.InteropServices;  )
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);
        private void PanelTop_MouseMove_1(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
        //------------------------------------------------------------

        // ÇIKIŞ KÜÇÜLTME BÜYÜLTME TEPSİ MODUNA ALMA
        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnMax_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnNormal.Visible = true;
            btnMax.Visible = false;
        }

        private void btnMin_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnNormal_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnNormal.Visible = false;
            btnMax.Visible = true;
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            login_page loginForm = new login_page();
            loginForm.Show();
        }

        private void cikisYap_Click(object sender, EventArgs e)
        {
            login_page loginForm = new login_page();
            loginForm.Show();
            this.Close();
        }
        // -----------------------------------------------------------

        // GÜN-SAAT ( TIMER EKLEDİK ENABLED = TRUE)
        private void timerSaat_Tick_1(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToLongTimeString();
            labelTarih.Text = DateTime.Now.ToShortDateString();
        }



        // -----------------------------------------------------------
        // İNGLİZCE TÜRKÇE AYARLAMASI
        private void tr_Click(object sender, EventArgs e)
        {
            button1.Text = "             Kitaplar ve Dergiler";
            button2.Text = "               Üzerimdekiler";
            cikisYap.Text = "Çıkış Yap";      
            label1.Text = "BENİM KÜTÜPHANEM";
        }

        private void eng_Click(object sender, EventArgs e)
        {
            button1.Text = "             Books and Magazines";
            button2.Text = "               Return procedures";
            cikisYap.Text = "Exit";
            label1.Text = "MY LIBRARY";
        }

        
        // -----------------------------------------------------------

    }
}
