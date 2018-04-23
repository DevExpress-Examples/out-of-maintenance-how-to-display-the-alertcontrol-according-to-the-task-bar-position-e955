using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars.Alerter;
using System.Runtime.InteropServices;

namespace SmartAlertControl {
    public partial class Form1 : Form {
        public Form1() {
            InitializeComponent();
        }

        private void simpleButton1_Click(object sender, EventArgs e) {
            AlertInfo info = new AlertInfo("DX Sample", "This alert takes into account the taskbar position");
            AlertHelper.ShowAlertNearTaskBar(alertControl1, this, info);
        }
    }
}