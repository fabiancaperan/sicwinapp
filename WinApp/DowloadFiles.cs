using core.UseCase.DownloadData;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security;
using System.Text;
using System.Windows.Forms;

namespace WinApp
{
    public partial class DowloadFiles : Form
    {
        public DowloadFiles()
        {
            InitializeComponent();
            //checkedListBox1.Items.AddRange((object[])Enum.GetValues(typeof(core.Repository.Types.CommerceType)));
            checkedListBox1.DataSource = Enum.GetValues(typeof(core.Repository.Types.CommerceType));
        }


        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog search = new FolderBrowserDialog();

            if (search.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = search.SelectedPath;
                textBox1.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                var rute = textBox1.Text;
                List<core.Repository.Types.CommerceType> subfolder = checkedListBox1.CheckedItems.OfType<core.Repository.Types.CommerceType>().ToList();
                if(rute != "")
                {
                    if(subfolder.Count()!=0)
                    {
                        button2.Enabled = false;
                        Cursor = Cursors.WaitCursor; // change cursor to hourglass type
                        var res = new DownloadFiles().build(rute, subfolder);
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
            catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
                button2.Enabled = true;
                Cursor = Cursors.Arrow; // change cursor to normal type
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        
    }
}
