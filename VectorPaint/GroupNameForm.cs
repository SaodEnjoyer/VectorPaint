using System;
using System.Windows.Forms;

namespace VectorPaint
{
    public partial class GroupNameForm : Form
    {
        private TextBox textBoxGroupName;
        private Button buttonAccept;
        private Button buttonCancel;

        public string GroupName { get; private set; }

        public GroupNameForm()
        {
            StartPosition = FormStartPosition.CenterParent;
            InitializeComponents();
        }


        private void InitializeComponents()
        {
            textBoxGroupName = new TextBox { Left = 50, Top = 20, Width = 200 };
            buttonAccept = new Button { Text = "Принять", Left = 50, Top = 60, Width = 100 };
            buttonCancel = new Button { Text = "Отмена", Left = 160, Top = 60, Width = 100 };

            buttonAccept.Click += ButtonAccept_Click;
            buttonCancel.Click += (sender, e) => this.DialogResult = DialogResult.Cancel;

            Controls.Add(textBoxGroupName);
            Controls.Add(buttonAccept);
            Controls.Add(buttonCancel);

            Text = "Введите название группы";
            ClientSize = new System.Drawing.Size(300, 120);
        }

        private void ButtonAccept_Click(object sender, EventArgs e)
        {
            GroupName = string.IsNullOrWhiteSpace(textBoxGroupName.Text) ? "Новая группа" : textBoxGroupName.Text;
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}