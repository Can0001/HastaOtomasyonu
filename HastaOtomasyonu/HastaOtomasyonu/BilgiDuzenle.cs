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
    public partial class BilgiDuzenle : Form
    {
        public BilgiDuzenle()
        {
            InitializeComponent();
        }
        public string TCno;

        SqlBaglantisi bgl = new SqlBaglantisi();

        private void BilgiDuzenle_Load(object sender, EventArgs e)
        {
            MskTC.Text= TCno;
            SqlCommand sorgu = new SqlCommand("select * from Hastalar where HastaTC=@HastaTC", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@HastaTC", MskTC.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                TxtAd.Text = dr[1].ToString();
                TxtSoyad.Text = dr[2].ToString();
                MskTelefon.Text = dr[4].ToString();
                TxtSifre.Text = dr[5].ToString();
                CmbCinsiyet.Text = dr[6].ToString();
            }
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu=new SqlCommand("update Hastalar set HastaAd=@HastaAd,HastaSoyad=@HastaSoyad,HastaTelefon=@HastaTelefon,HastaSifre=@HastaSifre,HastaCinsiyet=@HastaCinsiyet where HastaTC=@HastaTC",bgl.baglanti());
            sorgu.Parameters.AddWithValue("@HastaAd", TxtAd.Text);
            sorgu.Parameters.AddWithValue("@HastaSoyad", TxtSoyad.Text);
            sorgu.Parameters.AddWithValue("@HastaTelefon", MskTelefon.Text);
            sorgu.Parameters.AddWithValue("@HastaSifre",TxtSifre);
            sorgu.Parameters.AddWithValue("@HastaCinsiyet", CmbCinsiyet.Text);
            sorgu.Parameters.AddWithValue("@HastaTC", MskTC.Text);
            sorgu.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Bilgileriniz Kaydedildi","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Warning);
        }
    }
}
