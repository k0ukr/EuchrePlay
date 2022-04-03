using OnlineEuchre.Extras;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OnlineEuchre
{
    public partial class frmAsk : Form
    {
        public frmAsk()
        {
            InitializeComponent();
        }

        private void frmAsk_Activated(object sender, EventArgs e)
        {
        }

        private void btnPass_Click(object sender, EventArgs e)
        {
            Globals.PersonPassed = true;
            this.Hide();
        }

        private void btnCall_Click(object sender, EventArgs e)
        {
            Globals.TrumpCalled = true;
            this.Hide();
        }
    }
}
