using core.UseCase.ConvertData;
using core.UseCase.DownloadData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace WinApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
            checkedListBox1.DataSource = Enum.GetValues(typeof(core.Repository.Types.CommerceType));
        }

        private void Main_Load(object sender, EventArgs e)
        {

        }

        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog search = new FolderBrowserDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = search.SelectedPath;
                textBox2.Enabled = true;
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
        }

        private void btnChargeFileInput_Click(object sender, EventArgs e)
        {
            OpenFileDialog search = new OpenFileDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                string ext = Path.GetExtension(search.FileName);
                if (ext != "")
                    MessageBox.Show("El archivo no debe tener extensión");
                else
                {
                    textBoxInput.Text = search.FileName;
                    textBoxInput.Enabled = true;
                }
            }
        }

        private void textBox_Input(object sender, EventArgs e)
        {

        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            try
            {
                button2.Enabled = false;
                Cursor = Cursors.WaitCursor; // change cursor to hourglass type


                if (textBoxInput.Text != "")
                {
                    var ret = new ChargeFile().build(textBoxInput.Text);
                    MessageBox.Show("Se ha cargado correctamente");
                    button3.Enabled = true;

                }
                else
                {
                    MessageBox.Show("Debe seleccionar un archivo para cargar");
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;
                MessageError(ex);
            }
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                var rute = textBox2.Text;
                List<core.Repository.Types.CommerceType> subfolder = checkedListBox1.CheckedItems.OfType<core.Repository.Types.CommerceType>().ToList();
                if (rute != "")
                {
                    if (subfolder.Count() != 0)
                    {
                        button2.Enabled = false;
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        var res = new DownloadFiles().Build(rute, subfolder);
                        MessageBox.Show("Se ha generado con Éxito");
                    }
                    else
                    {
                        MessageBox.Show("Debe seleccionar al menos un item");
                    }
                }
                else
                {
                    MessageBox.Show("No a seleccionado una ruta");
                }
                Cursor = Cursors.Arrow; // change cursor to normal type
                button2.Enabled = true;

            }
            catch (Exception ex)
            {
                button2.Enabled = true;
                Cursor = Cursors.Arrow; // change cursor to normal type
                MessageError(ex);
            }
        }

        private static void MessageError(Exception ex)
        {
            Console.WriteLine(ex.Message);
            MessageBox.Show("Hubo un error comuniquese con el administrador");
        }
    }
}
