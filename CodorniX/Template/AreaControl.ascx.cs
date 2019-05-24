using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CodorniX.Template
{
    public partial class AreaControl : System.Web.UI.UserControl
    {
        public event EventHandler AddArea;
        public event EventHandler RemoveArea;

        
        public bool Enable
        {
            get
            {
                if (ViewState["Enable"] == null)
                    ViewState["Enable"] = true;

                return (bool)ViewState["Enable"];
            }
            set
            {
                ViewState["Enable"] = value;

                if (value)
                {
                    EnableEdition(false);
                }
                else
                {
                    DisableAll();
                }
            }
        }
        public string Text
        {
            get { return textBox.Text; }
            set { textBox.Text = value; }
        }

        public string Uid
        {
            get { return uid.Text; }
            set { uid.Text = value; }
        }

        public bool LastItem
        {
            get
            {
                if (ViewState["LastItem"] == null)
                {
                    ViewState["LastItem"] = false;
                }
                return (bool)ViewState["LastItem"];
            }
            set
            {
                ViewState["LastItem"] = value;

                if (value)
                {
                    add.CssClass = add.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                    add.Enabled = true;
                    edit.CssClass += " disabled hidden";
                    edit.Enabled = false;
                    remove.CssClass += " disabled hidden";
                    remove.Enabled = false;
                }
                else
                {
                    textBox.CssClass += " disabled";
                    textBox.Enabled = false;
                    edit.CssClass = edit.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                    edit.Enabled = true;
                    remove.CssClass = remove.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                    remove.Enabled = true;
                    add.CssClass += " disabled hidden";
                    add.Enabled = false;
                }
            }
        }


        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            textBox.Text = ViewState["ActualText"].ToString();
            EnableEdition(false);
        }

        protected void add_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(textBox.Text))
                AddArea?.Invoke(this, e);
        }

        protected void remove_Click(object sender, EventArgs e)
        {
            RemoveArea?.Invoke(this, e);
        }

        protected void edit_Click(object sender, EventArgs e)
        {
            ViewState["ActualText"] = textBox.Text;
            EnableEdition(true);
        }

        private void EnableEdition(bool enable)
        {
            if (enable)
            {
                textBox.CssClass = textBox.CssClass.Replace("disabled", "").Trim();
                textBox.Enabled = true;
                edit.CssClass += " disabled hidden";
                edit.Enabled = false;
                ok.CssClass = ok.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                ok.Enabled = true;
                cancel.CssClass = cancel.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                cancel.Enabled = true;

                if (LastItem)
                {
                    add.CssClass += " disabled hidden";
                    add.Enabled = false;
                }
                else
                {
                    remove.CssClass += " disabled hidden";
                    remove.Enabled = false;
                }
            }
            else
            {
                textBox.CssClass += " disabled";
                textBox.Enabled = false;
                edit.CssClass = edit.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                edit.Enabled = true;
                ok.CssClass += " disabled hidden";
                ok.Enabled = false;
                cancel.CssClass += " disabled hidden";
                cancel.Enabled = false;

                if (LastItem)
                {
                    add.CssClass = add.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                    add.Enabled = true;
                }
                else
                {
                    remove.CssClass = remove.CssClass.Replace("disabled", "").Replace("hidden", "").Trim();
                    remove.Enabled = true;
                }

            }
        }

        private void DisableAll()
        {
            if (LastItem)
            {
                add.CssClass += " disabled hidden";
                add.Enabled = false;
            }
            else
            {
                edit.CssClass += " disabled hidden";
                edit.Enabled = false;
                
                remove.CssClass += " disabled hidden";
                remove.Enabled = false;
            }

            textBox.CssClass += " disabled";
            textBox.Enabled = false;

            ok.CssClass += " disabled hidden";
            ok.Enabled = false;

            cancel.CssClass += " disabled hidden";
            cancel.Enabled = false;

        }

        protected void ok_Click(object sender, EventArgs e)
        {
            ViewState["ActualText"] = null;
            EnableEdition(false);
        }

        [Serializable]
        public class ControlInformation
        {
            public string ID { get; set; }
            public bool LastItem { get; set; }
        }

    }
}