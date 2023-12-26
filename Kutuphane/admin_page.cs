using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kutuphane
{
    public partial class admin_page : Form
    {
         

        private admin admin;
        public int adminID => admin.adminID;
        public admin_page(admin admin)
        {
            InitializeComponent();
            this.admin = admin;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            adSoyad.Text = $"{admin.ad} {admin.soyad}";
            mailLabel.Text = admin.eMail;
        }
        private void btnMin_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void btnMax_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            btnNormal.Visible = true;
            btnMax.Visible = false;
        }

        private void btnExit_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNormal_Click_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Normal;
            btnNormal.Visible = false;
            btnMax.Visible = true;
        }

        private void timerSaat_Tick_1(object sender, EventArgs e)
        {
            labelSaat.Text = DateTime.Now.ToLongTimeString();
            labelTarih.Text = DateTime.Now.ToShortDateString();
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
        // KİTAP İŞLEMLERİ FORMU AÇILMASI ( EKRANIN ORTASINDA )
        // SEÇİLİ OLAN BUTONDA CUBUK İŞARETİ ÇIKMASI
        private void btnKitapIslem_Click(object sender, EventArgs e) { 
        
           AbrirFormPanel(new FormKitapIslemleri());
            
           
        }
    private void btnUyeIslem_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormUyeIslemleri());          
        }
        private void btnEmanetIslem_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormEmanetIslemleri(adminID));      
        }
        
        private void btnDergiIslem_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormDergiIslemleri());
        }
        private void btnYetkiliIslem_Click(object sender, EventArgs e)
        {
            AbrirFormPanel(new FormAdminIslemleri());
        }
        //-----------------------------------------------------------



        // Uygulamayı yukardan tutup istediğimiz yere sürükleme ( --> using System.Runtime.InteropServices;  )
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private extern static void ReleaseCapture();
        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private extern static void SendMessage(System.IntPtr hwnd, int wmsg, int wparam, int lparam);

        private void PanelTop_MouseMove(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void tr_Click(object sender, EventArgs e)
        {

            label1.Text = "BENİM KÜTÜPHANEM";
            btnKitapIslem.Text = "             Kitap İşlemleri";
            btnDergiIslem.Text = "             Dergi İşlemleri";
            btnUyeIslem.Text = "                  Üye İşlemleri";
            btnEmanetIslem.Text = "             Emanet İşlemleri";
            btnYetkiliIslem.Text = "             Admin İşlemleri";
            cikisYap.Text = "Çıkış Yap";
        }

        private void eng_Click(object sender, EventArgs e)
        {
            label1.Text = "MY LIBRARY";
            btnKitapIslem.Text = "             Book Staff";
            btnDergiIslem.Text = "             Magazine Staff";
            btnUyeIslem.Text = "                  User Staff";
            btnEmanetIslem.Text = "             Loan Transactions";
            btnYetkiliIslem.Text = "             Admin Staff";
            cikisYap.Text = "Exit";
        }
        private void label5_Click(object sender, EventArgs e)
        {
            this.Close();
            login_page loginForm = new login_page();
            loginForm.Show();
        }
        private void pictureBox5_Click(object sender, EventArgs e)
        {
            this.Close();
            login_page loginForm = new login_page();
            loginForm.Show();
        }
    }
}
