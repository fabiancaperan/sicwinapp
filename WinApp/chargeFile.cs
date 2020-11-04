using core.UseCase.ConvertData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace WinApp
{
    public partial class chargeFile : Form
    {
        public chargeFile()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(search.FileName);
                if (ext != "")
                    MessageBox.Show("El archivo no debe tener extensión");
                else 
                {
                    textBox1.Text = search.FileName;
                    textBox1.Enabled = true;
                }
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                
                
                if (textBox1.Text != "")
                {
                    var ret = new ChargeFile().build(textBox1.Text);
                    MessageBox.Show("Se ha cargado correctamente");
                }
                else
                {
                    MessageBox.Show("Debe seleccionar un archivo para cargar");
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
            }
            //ProcessStartInfo psi = new ProcessStartInfo
            //{
            //    FileName = textBox1.Text,
            //    UseShellExecute = true
            //};
            //Process.Start(psi);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
