﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Batu
{
    public partial class z2_m1 : Form
    {
        public z2_m1()
        {
            InitializeComponent();
        }
        string k_ad = Form3.k_ad;
        int saniye = 45;
        int dakika = 0;
        int puan = 45;
        SqlConnection sqlserver = new SqlConnection("Data Source=HPCOMPUTER\\SQLEXPRESS;Initial Catalog=projedatabase;Integrated Security=True");
        SqlDataAdapter sqldataadapter;
        SqlCommand sqlcommand;
        SqlDataReader sqlreader;
        private void Duvara_dokundu(object sender, EventArgs e)
        {
            MessageBox.Show("Duvara değdin ve başlangıç noktasına dönüyorsun.Bi'daha ki sefere daha dikkatli olmalısın.");
            Point baslangicnoktasi = label1.Location;
            Cursor.Position = PointToScreen(baslangicnoktasi);
        }
        private void Bitis(object sender, EventArgs e)
        {
            sure.Stop();
            MessageBox.Show("Tebrikler haritayı bitirdiniz...(Kazanılan puan: " + puan + ")");
            sqlserver.Open();
            sqlcommand = new SqlCommand("UPDATE tbl_kullanici SET kullanici_puan=kullanici_puan+'" + puan + "' WHERE kullanici_ad = '" + k_ad + "'", sqlserver);
            sqlcommand.ExecuteNonQuery();
            sqlserver.Close();
            this.Close();
        }
        private void esc_tusu(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Escape)
            {
                sure.Stop();
                Close();
            }
        }

        private void Z2_m1_Load(object sender, EventArgs e)
        {
            MessageBox.Show("Bu bölümün süresi :" + dakika + "dakika : " + saniye + "saniye'dir");
            Point baslangicnoktasi = label1.Location;
            Cursor.Position = PointToScreen(baslangicnoktasi);
            sure.Enabled = true;
            sure.Interval = 1000;
            sure.Start();
        }

        private void Sure_Tick_1(object sender, EventArgs e)
        {
            if (dakika != 0)
            {
                saniye = saniye - 1;
                puan = puan -1;

                if (saniye <= 0)
                {
                    saniye = 59;
                    dakika = dakika - 1;
                }
            }
            else if (dakika == 0)
            {
                if (saniye != 0)
                {
                    saniye = saniye - 1;
                    puan = puan - 1;
                }
                else
                {
                    sure.Stop();
                    MessageBox.Show("Belirtilen süre içinde oyunu bitiremedin..Üzülme bi'daha ki sefere daha iyi hazırlan :)");
                    this.Close();
                }
            }
            lbl_sure.Text = "Dakika: " + dakika + "Saniye: " + saniye;
        }
    }
}
