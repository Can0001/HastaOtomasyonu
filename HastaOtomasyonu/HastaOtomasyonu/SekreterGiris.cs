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
    public partial class SekreterGiris : Form
    {
        public SekreterGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sorgu = new SqlCommand("select * from Sekreter where SekreterTC=@TC and SekreterSifre=@Sifre", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@TC", MskTC.Text);
            sorgu.Parameters.AddWithValue("@Sifre", TxtSifre.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            if (dr.Read())
            {
                SekreterDetay yeni = new SekreterDetay();
                yeni.TCnumara=MskTC.Text;
                yeni.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Kullanıcı","Tekrar Dene",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
        }
    }
}
