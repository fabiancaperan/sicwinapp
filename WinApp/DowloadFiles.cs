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
                List<string> subfolder = checkedListBox1.CheckedItems.OfType<string>().ToList();
                if(rute != "")
                {
                    if(subfolder.Count()!=0)
                    {
                        var res = new DownloadFiles().build(rute, subfolder);
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
                
            }
            catch (SecurityException ex)
            {
                MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                $"Details:\n\n{ex.StackTrace}");
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
