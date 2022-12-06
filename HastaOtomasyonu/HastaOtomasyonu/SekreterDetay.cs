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
    public partial class SekreterDetay : Form
    {
        public SekreterDetay()
        {
            InitializeComponent();
        }
        public string TCnumara;
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void SekreterDetay_Load(object sender, EventArgs e)
        {
            LblTC.Text = TCnumara;
            //Ad Ve Soyad
            SqlCommand sorgu = new SqlCommand("select SekreterAdSoyad from Sekreter where SekreterTC=@TC", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@TC", LblTC.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            while (dr.Read())
            {
                LblAdSoyad.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

            //Brans Listeleme
            DataTable dt1 = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("select * from Branslar2", bgl.baglanti());
            da.Fill(dt1);
            dataGridView1.DataSource=dt1;

            //Doktorları Listeleme
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter("select (DoktorAd +' '+DoktorSoyad) as 'Doktorlar',DoktorBrans from Doktorlar", bgl.baglanti());
            da2.Fill(dt2);
            dataGridView2.DataSource = dt2;
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu=new SqlCommand("insert into Randevu values (RandevuTarih,RandevuSaat,RandevuBrans,RandevuDoktor) values (@Tarih,@Saat,@Brans,@Doktor)",bgl.baglanti());
            sorgu.Parameters.AddWithValue("@Tarih", MskTarih.Text);
            sorgu.Parameters.AddWithValue("@Saat",MskSaat.Text);
            sorgu.Parameters.AddWithValue("@Brans",CmbBrans.Text);
            sorgu.Parameters.AddWithValue("@Doktor",CmbDoktor.Text);
            sorgu.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Randevu Oluşturuldu","Bilgi",MessageBoxButtons.OK,MessageBoxIcon.Information);
        }
    }
}
