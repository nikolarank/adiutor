﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NHibernate;
using SP.Data_Access;
using Studentski_projekti.Entiteti;

namespace SP
{
    public partial class FormDodajKnjigu : Form
    {
        public IList<Autor> listaAutora;

        public FormDodajKnjigu()
        {
            listaAutora = new List<Autor>();
            InitializeComponent();
            dataGridView1.MultiSelect = false;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Literatura l = new Literatura {
                Naslov = textBox1.Text,
                GodinaIzdavanja = int.Parse(textBox2.Text),
                Autori = listaAutora
            };

            Knjiga k = new Knjiga
            {
                ISBN = textBox4.Text,
                Izdavac = textBox3.Text,
                Literatura = l
            };

            ISession s = DataLayer.GetSession();
            Crud<Literatura>.Create(s, l);
            Crud<Knjiga>.Create(s, k);
            s.Close();

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";

            dataGridView1.DataSource = null;
            dataGridView1.Update();
            dataGridView1.Refresh();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Autor a = new Autor()
            {
                Ime = textBox6.Text
            };
            listaAutora.Add(a);
            dataGridView1.DataSource = null;
            dataGridView1.Update();
            dataGridView1.Refresh();
            dataGridView1.DataSource = listaAutora;
            dataGridView1.Columns[0].Visible = false;
            dataGridView1.Columns[2].Visible = false;
            textBox6.Text = "";
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.CurrentRow != null)
            {
                int pos = dataGridView1.CurrentRow.Index;
                listaAutora.RemoveAt(pos);
                dataGridView1.DataSource = null;
                dataGridView1.Update();
                dataGridView1.Refresh();
                dataGridView1.DataSource = listaAutora;
                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[2].Visible = false;
            }
        }
    }
}
