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
    public partial class HastaGiris : Form
    {
        public HastaGiris()
        {
            InitializeComponent();
        }
        SqlBaglantisi bgl = new SqlBaglantisi();
        SqlCommand sorgu;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            HastaUyeKayit kayit = new HastaUyeKayit();
            kayit.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            sorgu = new SqlCommand("Select * from Hastalar where @HastaTC and @HastaSifre", bgl.baglanti());
            sorgu.Parameters.AddWithValue("@HastaTC", maskedTextBox1.Text);
            sorgu.Parameters.AddWithValue("@HastaSifre", textBox1.Text);
            SqlDataReader dr = sorgu.ExecuteReader();
            if (dr.Read())
            {
                HastaDetay detay = new HastaDetay();
                detay.tc = maskedTextBox1.Text;
                detay.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hastalı TC veya Şifre");
            }
            bgl.baglanti().Close();
        }
    }
}
