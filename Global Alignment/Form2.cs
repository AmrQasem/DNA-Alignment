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
    public partial class Form2 : Form
    {
       public static Global_Alignment G1 = new Global_Alignment();
       public Form1 form;
       public void Function_Call(Form1 form)
       {
           form = new Form1();
           int[,] MAT = form.G1.Fill_Matrix(ref form.Sequence1, ref form.Sequence2);
           form.G1.back_track(MAT, ref form.Sequence1, ref form.Sequence2, form.Rows, form.Col);
       }
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {  
           Function_Call(form);
        }
    }
}
