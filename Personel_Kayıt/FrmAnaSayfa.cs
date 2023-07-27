using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //SQL komutlarını kullanmak için gerekli olan kütüphanedir.

namespace Personel_Kayıt
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-BOV3B8B\\SQLEXPRESS;Initial Catalog=PersonelVeriTabani;Integrated Security=True");
        //Burdaki (-) işareti kelimelerle bitişik olmalı                //Burdaki slash (\) sayısı iki tane olmalı. Tek slash kaçış komutu, iki slash bunun bir dosya yolu olduğunu belirtir.     

        public void temizle()
        {
            textBoxId.Text = "";
            textBoxAd.Text = "";
            textBoxSoyad.Text = "";
            textBoxMeslek.Text = "";
            maskedTextBoxMaas.Text = "";
            comboBoxSehir.Text = "";
            radioButton1.Checked = false; radioButton2.Checked = false;
            textBoxAd.Focus();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'personelVeriTabaniDataSet.Tbl_Personeller' table. You can move, or remove it, as needed.
            this.tbl_PersonellerTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personeller);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.tbl_PersonellerTableAdapter.Fill(this.personelVeriTabaniDataSet.Tbl_Personeller);
        }

        private void buttonKaydet_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into Tbl_Personeller (perAd,perSoyad,perSehir,perMaas,perMeslek,perDurum) values (@p1, @p2,@p3,@p4,@p5,@p6)", baglanti);
            // SqlCommand sınıfında komut isminde yeni bir nesne türettik. Tırnak içine yazılan kısım SQl sorgusudur ve @p1,@p2 vb. kısmı kullanıcıdan alınacak. Bu parametreler kısmı textboxlar için köprü görevi görür
            komut.Parameters.AddWithValue("@p1", textBoxAd.Text);
            komut.Parameters.AddWithValue("@p2", textBoxSoyad.Text);
            komut.Parameters.AddWithValue("@p3", comboBoxSehir.Text); //Bu kısım ise parametrelerin kullanıcıdan alınmasını sağlar.
            komut.Parameters.AddWithValue("@p4", maskedTextBoxMaas.Text);
            komut.Parameters.AddWithValue("@p5", textBoxMeslek.Text);
            komut.Parameters.AddWithValue("@p6", labelDurum.Text);
            komut.ExecuteNonQuery(); //Sorguyu çalıştırır.Tablo sonucunun eklendiği durumlarda (insert,update,delete) kullanılır.
            baglanti.Close();
            MessageBox.Show("Personel Eklendi");
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                labelDurum.Text = "True";
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                labelDurum.Text = "False";
            }
        }

        private void buttonTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex; //secilen değişkeni, bir hücreye tıklandığında o hücrenin bulumduğu satırın index değerini alır.

            textBoxId.Text = dataGridView1.Rows[secilen].Cells[0].Value.ToString();
            textBoxAd.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            textBoxSoyad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            comboBoxSehir.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            maskedTextBoxMaas.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            labelDurum.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            textBoxMeslek.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();
        }

        private void labelDurum_TextChanged(object sender, EventArgs e)
        {
            if (labelDurum.Text == "True")
            {
                radioButton1.Checked = true;
            }
            if (labelDurum.Text == "False")
            {
                radioButton2.Checked = true;
            }
        }

        private void buttonSil_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komutsil = new SqlCommand("delete from Tbl_Personeller where perId=@k1", baglanti);
            komutsil.Parameters.AddWithValue("@k1", textBoxId.Text);
            komutsil.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Personel Silindi");
        }

        private void buttonGuncelle_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand kayıtcüncelle = new SqlCommand("update Tbl_Personeller set perAd=@a1,perSoyad=@a2,perSehir=@a3,perMaas=@a4,perDurum=@a5,perMeslek=@a6 where perId=@a7 ", baglanti);
            kayıtcüncelle.Parameters.AddWithValue("@a1", textBoxAd.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a2", textBoxSoyad.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a3", comboBoxSehir.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a4", maskedTextBoxMaas.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a5", labelDurum.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a6", textBoxMeslek.Text);
            kayıtcüncelle.Parameters.AddWithValue("@a7", textBoxId.Text);
            kayıtcüncelle.ExecuteNonQuery();
            baglanti.Close();
            MessageBox.Show("Kayıt Güncellendi");
        }

        private void buttonIstatistik_Click(object sender, EventArgs e)
        {
            Frmistatistik fr = new Frmistatistik();
            fr.Show();
        }

        private void buttonGrafikler_Click(object sender, EventArgs e)
        {
            FrmGrafikler frg=new FrmGrafikler();
            frg.Show();
        }
    }
}
