﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Wil
{
    public partial class frmVehicleManager : UserControl
    {
        public frmVehicleManager()
        {
            InitializeComponent();
        }
        static DBAccess _DBAccess = new DBAccess();

        private void frmVehicleManager_Load(object sender, EventArgs e)
        {
            string sGetCategories = @"
                        SELECT LTrim(RTrim(CatDesc))[CatDesc], CatID
                        FROM tblCategory
                        ";

            _DBAccess.Do_SQLQueryAlt(sGetCategories);

            comboBoxCategory.DataSource = _DBAccess.bndSrcAlt;
            comboBoxCategory.DisplayMember = "CatDesc"; //column you want to show in comboBox
            comboBoxCategory.ValueMember = "CatID"; //column you want to use in the background (not necessary)!
        }

        private void buttonSubmit_Click(object sender, EventArgs e)
        {
            int isChecked = 0;
            int sVehicleType;

            if(comboBoxTrailerOrVehicle.SelectedIndex == 0)
            {
                sVehicleType = 1;
            }
            else
            {
                sVehicleType = 2;
            }

            if (checkBoxTrailerAttachable.Checked)
            {
                isChecked = 1;
            }

            string sInsertVehicle = String.Format(
                    @"INSERT tblVehicle VALUES
                    ('{0}', '{1}', '{2}', '{3}', {4}, {5}, {6})", textBoxRegNumber.Text, textBoxVinNumber.Text, textBoxVehicleDesc.Text, textBoxVehicleKm.Text, sVehicleType, comboBoxCategory.SelectedValue, isChecked);

           _DBAccess.Do_SQLQuery(sInsertVehicle);

           MessageBox.Show("Vehicle added successfuly");

        }
    }
}
