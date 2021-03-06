﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Lab3_cm
{
    public partial class Form1 : Form
    {
        readonly double[] x = { -2, 0, 2, 3 };
        readonly double[] f = { 3, 4, 1, 2 };

        Polynomial LPoly;
        Polynomial PPoly;
        Polynomial QPoly;
        double standardDeviation;

        const int mQPolyIndex = 4;

        const string xRowString = "X";
        const string fRowString = "F";

        const string lPolyString = "L(x)";
        const string pPolyString = "P(x)";
        const string qPolyString = "Q(x)";
        const string standatdDeviationString = "Standard deviation = ";

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            InitializeDataGridView();

            InitializePolynomials();
        }

        private void InitializeDataGridView()
        {
            for (int i = 0; i < x.Length; i++)
            {
                var column = new DataGridViewColumn();
                column.HeaderCell.Value = i.ToString();

                column.CellTemplate = new DataGridViewTextBoxCell();
                dataGridView1.Columns.Add(column);
            }
            DataGridViewRow rowX = new DataGridViewRow(),
                            rowF = new DataGridViewRow();

            rowX.HeaderCell.Value = xRowString;
            rowF.HeaderCell.Value = fRowString;

            for (int i = 0; i < x.Length; i++)
            {
                rowX.Cells.Add(new DataGridViewTextBoxCell());
                rowF.Cells.Add(new DataGridViewTextBoxCell());

                rowX.Cells[i].Value = x[i].ToString();
                rowF.Cells[i].Value = f[i].ToString();
            }

            dataGridView1.Rows.Add(rowX);
            dataGridView1.Rows.Add(rowF);
        }
        private void InitializePolynomials()
        {
            LPoly = Approximator.LagranzPolynomial(x, f);
            PPoly = Approximator.NewtonPolynomial(x, f);
            QPoly = Approximator.MinSquares(x, f, mQPolyIndex);
            standardDeviation = Approximator.StandardDeviation(x, f, QPoly);
        }


        private void button1_Click(object sender, EventArgs e)
        {
            ShowPoly frm = new ShowPoly(LPoly.ToString(lPolyString), 
                PPoly.ToString(pPolyString), 
                QPoly.ToString(qPolyString), 
                standatdDeviationString + standardDeviation.ToString());

            frm.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Table frm = new Table(LPoly, PPoly, QPoly, x, f);

            frm.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Graph frm = new Graph(LPoly, PPoly, QPoly, x, f);

            frm.Show();
        }
    }
}
