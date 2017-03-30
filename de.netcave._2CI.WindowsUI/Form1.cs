using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using de.netcrave._2CI.Model;
using System.Collections.Concurrent;
using de.netcrave._2CI.DB;
namespace de.netcrave._2CI.WindowsUI
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            
            MusicDatabase._.InitializeFromPath(@"G:\New folder (2)", true);
            MusicDatabase._.InitializeFromPath(@"H:\incoming3\files.sq10.net", true);
            MusicDatabase._.InitializeFromPath(@"K:\finished bt downloads", true);
            MusicDatabase._.InitializeFromPath(@"G:\Vyzo Music", true);
            MusicDatabase._.InitializeFromPath(@"K:\new bt downloads\Ministry Audiophile Vinyl 24-96 FLAC", true);
            dataGridView1.DataSource = MusicDatabase._.FragmentIndex.fragments.SelectMany(s => s.AudioFiles).OrderBy(o => o.Artist).ToList();
        }
     
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            var asdf = e;
        }
    }
}
