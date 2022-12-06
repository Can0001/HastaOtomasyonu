using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace HastaOtomasyonu
{
    public partial class HastaUyeKayit : Form
    {
        public HastaUyeKayit()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        SqlCommand sorgu;
        private void BtnKayitOl_Click(object sender, EventArgs e)
        {
            sorgu = new SqlCommand("insert into Hastalar values (@HastaAd,@HastaSoyad,@HastaTC,@HastaTelefon,@HastaSifre,@HastaCinsiyet)", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@HastaAd", TxtAd.Text);
            sorgu.Parameters.AddWithValue("@HastaSoyad", TxtSoyad.Text);
            sorgu.Parameters.AddWithValue("@HastaTC",MskTC.Text);
            sorgu.Parameters.AddWithValue("HastaTelefon",MskTelefon.Text);
            sorgu.Parameters.AddWithValue("HastaSifre", TxtSifre.Text);
            sorgu.Parameters.AddWithValue("HastaCinsiyet", CmbCinsiyet.Text);
            sorgu.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Kaydınız Gerçekleşmiştir Şifreniz: "+TxtSifre.Text,"Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
