using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Global_Alignment
{
    public partial class Form1 : Form
    {
        public string Sequence1;
        public string Sequence2;
        public int[,] Matrix;
        public int Rows;
        public int Col;
        public Global_Alignment G1;
        public Form1()
        {
            InitializeComponent();
            Sequence1="";
            Sequence2="";
            G1 = new Global_Alignment();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide(); 
            Form2 form2 = new Form2();
            //form2.Visible = true;
            form2.Show();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            Sequence1 = textBox1.Text.ToUpper();
            if (Sequence1.Trim() == "")        //Removes Any Whitespace From our string
            {
                textBox1.Clear();
                return;
            }
            for (int i = 0; i < Sequence1.Length; i++)
            {
                if (char.IsNumber(Sequence1[i]))
                {
                    MessageBox.Show("Your Sequence containts Number");
                    textBox1.Clear();
                    return;
                }
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            Sequence2 = textBox2.Text.ToUpper();
            if (Sequence2.Trim() == "")        //Removes Any Whitespace From our string
            {
                textBox2.Clear();
                return;
            }
            for (int i = 0; i < Sequence2.Length; i++)
            {
                if (char.IsNumber(Sequence2[i]))
                {
                    MessageBox.Show("Your Sequence containts Number");
                    textBox2.Clear();
                    return;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
    public class Global_Alignment
    {
        int GAP = -1;
        public int Get_Secore(char Seq1, char Seq2)
        {
            if (Seq1 == Seq2)
            {
                return 1;          //Match
            }
            return -1;             //DisMatch
        }
        public int Value_or_Zero(int Value)    // Return the value or return zero 
        {
            if (Value > 0)
            {
                return Value;
            }
            return 0;
        }
        public int Get_Max(int match, int insert, int delete)
        {
            if (match > insert)
            {
                if (match > delete)
                {
                    return match;
                }
                return delete;
            }
            if (insert > delete)
            {
                return insert;
            }
            return delete;
        }
        public int[,] Fill_Matrix(ref string Sequence1, ref string Sequence2)
        {
            Form1 form = Application.OpenForms["Form1"] as Form1;   // Get values of 2 Sequences 
            Sequence1 = form.textBox1.Text;
            Sequence2 = form.textBox2.Text;
            int Match, Insert, Delete = 0;
            form.Rows = Sequence1.Length + 1;
            form.Col = Sequence2.Length + 1;
            form.Matrix = new int[form.Rows, form.Col];
            for (int i = 0; i < form.Rows; i++)          //Intialize Row 1 With Zeros
            {
                form.Matrix[i, 0] = 0;
            }//end for Rows
            for (int j = 0; j < form.Col; j++)          //Intialize Col 1 With Zeros
            {
                form.Matrix[0, j] = 0;
            }//end for Col
            for (int i = 1; i < form.Rows; i++)
            {
                for (int j = 1; j < form.Col; j++)
                {
                    //Match case 1
                    Match = form.Matrix[i - 1, j - 1] + Get_Secore(Sequence1[i-1], Sequence2[j-1]);
                    Match = Value_or_Zero(Match);
                    //Insert case 2
                    Insert = form.Matrix[i - 1, j] + GAP;
                    Insert = Value_or_Zero(Insert);
                    //Delete case 3
                    Delete = form.Matrix[i, j - 1] + GAP;
                    Delete = Value_or_Zero(Delete);
                    form.Matrix[i, j] = Get_Max(Match, Insert, Delete);
                }                                   //End Col
            }                                       //End Rows

            return form.Matrix;
        }    //End FillMatrix
        public void back_track(int[,] Mat, ref string sequence1, ref string sequence2, int i, int j)
        {
            Form1 form = Application.OpenForms["Form1"] as Form1;   // Get values of 2 Sequences 
            sequence1 = form.textBox1.Text;
            sequence1 = form.textBox2.Text;

            string Sequence1_Align = "";
            string Sequence2_Align = "";
            i = sequence1.Length;
            j = sequence2.Length;
            while (i > 0 && j > 0)
            {
                int Score = Mat[i, j];
                int Score_Diagonal = Mat[i - 1, j - 1];
                int Score_UP = Mat[i, j - 1];
                int Score_Left = Mat[i - 1, j];
                if (Score == Score_Diagonal +Get_Secore(sequence1[i-1],sequence2[j-1]))
                {
                    Sequence1_Align = sequence1[i-1] + Sequence1_Align;
                    Sequence2_Align = sequence2[j-1] + Sequence2_Align;
                    i = i - 1;
                    j = j - 1; 
                }
                else if (Score == Score_Left + GAP)
                {
                    Sequence1_Align = sequence1[i-1] + Sequence1_Align;
                    Sequence2_Align = "-" + Sequence2_Align;
                    i = i - 1;
                }
                else if (Score == Score_UP + GAP)
                {
                    Sequence1_Align = "-" + Sequence1_Align;
                    Sequence2_Align = sequence2[j-1] + Sequence2_Align;
                    j = j - 1;
                }

            }       // End While
            TextBox Seq1 = Application.OpenForms["Form2"].Controls["textBox1"] as TextBox;
            Seq1.Text = Sequence1_Align;
            TextBox Seq2 = Application.OpenForms["Form2"].Controls["textBox2"] as TextBox;
            Seq2.Text = Sequence2_Align;
        } //End class global

    }
}//End namespae
