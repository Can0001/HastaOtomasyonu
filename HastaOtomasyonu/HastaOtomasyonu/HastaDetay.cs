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
    public partial class HastaDetay : Form
    {
        public HastaDetay()
        {
            InitializeComponent();
        }
        public string tc;
        SqlBaglantisi bgl = new SqlBaglantisi();
        SqlCommand sorgu;
        private void HastaDetay_Load(object sender, EventArgs e)
        {
            //Ad Soyad Çekme
            LblTC.Text = tc;
            sorgu = new SqlCommand("select HastaAd,HastaSoyad from Hastalar where @HastaTC", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@HastaTC",LblTC.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0] +" "+ dr[1];
            }
            bgl.baglanti().Close();

            //Randevu Geçmişi
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevu where HastaTC=" + tc, bgl.baglanti());
            da.Fill(dt);
            dataGridView1.DataSource = dt;

            //Branşları Çekme
            SqlCommand sorgu2=new SqlCommand("select * from Branslar2",bgl.baglanti());
            SqlDataReader dr2 = sorgu2.ExecuteReader(); 
            while(dr2.Read())
            {
                CmbBrans.Items.Add(dr2[0]);
            }
            bgl.baglanti().Close();
        }

        private void CmbBrans_SelectedIndexChanged(object sender, EventArgs e)
        {
            CmbDoktor.Items.Clear();
            SqlCommand sorgu3 = new SqlCommand("select DoktorAd,DoktorSoyad from Doktorlar where @DoktorBrans ", bgl.baglanti());
            sorgu3.Parameters.AddWithValue("@DoktorBrans", CmbBrans.Text);
            SqlDataReader dr3=sorgu3.ExecuteReader();
            while (dr3.Read())
            {
                CmbDoktor.Items.Add(dr3[0]+" "+ dr3[1]);
            }
            bgl.baglanti().Close();
        }

        private void CmbDoktor_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Randevu where RandevuBrans='" + CmbBrans.Text + "'",bgl.baglanti());
            da.Fill(dt);
            dataGridView2.DataSource = dt;
        }

        private void LnkBilgiDüzenle_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            BilgiDuzenle bilgi = new BilgiDuzenle();
            bilgi.TCno = LblTC.Text;
            bilgi.Show();
        }
    }
}
