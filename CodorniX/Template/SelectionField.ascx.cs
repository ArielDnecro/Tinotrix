using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodorniX.Template
{
    public partial class SelectionField : System.Web.UI.UserControl
    {

        public delegate void SelectionFieldEvent (object sender, EventArgs e, SelectionField field);
        public event SelectionFieldEvent SelectedIndexChanged;
        public event SelectionFieldEvent AddClick;
        public event SelectionFieldEvent RemoveClick;
        public string SelectedValue
        {
            get { return dropdown.SelectedValue; }
            set { dropdown.SelectedValue = value; }
        }
        public object DataSource
        {
            get { return dropdown.DataSource; }
            set { dropdown.DataSource = value; }
        }
        public string DataValueField
        {
            get { return dropdown.DataValueField; }
            set { dropdown.DataValueField = value; }
        }

        public string DataTextField
        {
            get { return dropdown.DataTextField; }
            set { dropdown.DataTextField = value; }
        }
        public override void DataBind()
        {
            dropdown.DataBind();
        }

        [PersistenceMode(PersistenceMode.InnerProperty), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public string LabelText
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void add_Click(object sender, EventArgs e)
        {
            AddClick?.Invoke(sender, e, this);
        }

        protected void remove_Click(object sender, EventArgs e)
        {
            RemoveClick?.Invoke(sender, e, this);
        }

        [Serializable]
        public class ControlInformation
        {
            public string SelectedValue { get; set; }
            public string LabelText { get; set; }
            public string ID { get; set; }
        }

        protected void dropdown_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedIndexChanged?.Invoke(sender, e, this);
        }
    }
}