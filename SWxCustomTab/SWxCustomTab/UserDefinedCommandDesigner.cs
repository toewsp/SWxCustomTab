using System;
using System.ComponentModel;
using System.Reflection;
using System.Windows.Forms;

namespace SWxCustomTab
{
    public partial class UserDefinedCommandDesigner : Form
    {

        BindingSource bindingSource;
        SWxCustomTab ParentClass { get; set; }

        public UserDefinedCommandDesigner(SWxCustomTab parentClass)
        {

            InitializeComponent();
            ParentClass = parentClass;

            typeof(DataGridView).InvokeMember("DoubleBuffered", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetProperty, null, dgv, new object[] { true });

            colDocType.DataSource = Enum.GetValues(typeof(EnumShowInDocType));
            colDocType.ValueType = typeof(EnumShowInDocType);

            bindingSource = new BindingSource();
            bindingSource.DataSource = ParentClass.UserDefinedCommandManager.Commands;
            bindingSource.AddingNew += BindingSource_AddingNew;
            bindingSource.ListChanged += BindingSource_ListChanged;
            
            dgv.DataSource = bindingSource;

        }

        private void BindingSource_ListChanged(object sender, ListChangedEventArgs e)
        {

            btnSave.Enabled = true;

        }

        private void BindingSource_AddingNew(object sender, AddingNewEventArgs e)
        {

            UserDefinedCommand cmd = new UserDefinedCommand();
            cmd.SetParent(ParentClass);
            cmd.Name = "neu";
            cmd.DocType = EnumShowInDocType.Teil;
            cmd.CommandType = "Macro";

            e.NewObject = cmd;
            
        }

        private void btnClose_Click(object sender, EventArgs e)
        {

            this.Close();

        }

        private void UserDefinedCommandDesigner_FormClosing(object sender, FormClosingEventArgs e)
        {

            if (btnSave.Enabled)
            {
                
                DialogResult dr = MessageBox.Show("Sollen die Änderungen gespeichert werden?", "Einstellungen", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (dr == DialogResult.Cancel) e.Cancel = true;
                if (dr == DialogResult.Yes) ParentClass.UserDefinedCommandManager.AcceptChanges();
                if (dr == DialogResult.No) ParentClass.UserDefinedCommandManager.DiscardChanges();

            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            ParentClass.UserDefinedCommandManager.AcceptChanges();
            btnSave.Enabled = false;

        }
    }
}
