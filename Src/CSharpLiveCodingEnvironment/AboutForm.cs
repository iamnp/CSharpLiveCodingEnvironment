﻿using System;
using System.Windows.Forms;

namespace CSharpLiveCodingEnvironment
{
    internal partial class AboutForm : Form
    {
        public AboutForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}