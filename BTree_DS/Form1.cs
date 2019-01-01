using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BTree;

namespace BTree_DS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtInsertPrint.ScrollBars = ScrollBars.Vertical;
        }
        BTree<int, int> bTree;

        private void txtInsert_TextChanged(object sender, EventArgs e)
        {

        }
       

        private void btnInsert_Click_1(object sender, EventArgs e)
        {
            Random random = new Random();
            int key = random.Next(0, 999);
            int pointer = random.Next(0, 999);
            //if (txtInsert.Text != null)
            try
            {
                if (bTree.Insert(key, pointer))
                {
                    txtInsertPrint.Clear();
                    bTree.print(txtInsertPrint);
                }
                else txtInsertPrint.AppendText(key + " Has Been In The Tree");
            }
            catch (Exception err)
            {
                txtInsertPrint.AppendText(err.ToString());
            }
                
            
            
        }

        private void label1_Click_1(object sender, EventArgs e)
        {
            bTree = new BTree<int, int>(Int32.Parse(MaxDegree.Text));
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {

        }

        private void txtPrint_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                txtDelPrint.Clear();
                if (!bTree.Delete(int.Parse(txtDel.Text)))
                    txtDelPrint.AppendText(txtDel.Text + " Not Found\n");
                else 
                    txtDelPrint.AppendText(txtDel.Text + " has been deleted\n");
                txtDel.Clear();
                
                bTree.print(txtDelPrint);
            }
            catch(Exception err)
            {
                txtDelPrint.AppendText( err.ToString());
            }

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {

        }
    }
}
