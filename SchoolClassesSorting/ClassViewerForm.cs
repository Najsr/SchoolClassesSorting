using System;
using System.Drawing;
using System.Windows.Forms;

namespace SchoolClassesSorting
{
    public partial class ClassViewerForm : Form
    {
        private readonly FormMain _formMain;
        private readonly Soutez _soutez;

        private const string ClassId = "Třída: ";

        private int _pageId;

        public int PageId
        {
            get => _pageId;
            private set { _pageId = value; LoadValues(); }
        }

        public ClassViewerForm(FormMain formMain, Soutez soutez)
        {
            _formMain = formMain;
            _formMain.Hide();
            _soutez = soutez;
            InitializeComponent();
            label1.Text += _soutez.PocetZaku();
            comboBox1.DataSource = Enum.GetValues(typeof(ViewOptions));
            SetGridLabel();
            buttonBackward_Click(null, null);
        }

        public enum ViewOptions
        {
            Id = 0,
            Kategorie,
            Škola
        }

        private void ClassViewerForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _formMain.Show();
        }

        void SetGridLabel()
        {
            for (var i = 1; i <= _soutez.PocetZakuNaTridu / 6; i++)
            {
                for (var j = 1; j <= 6; j++)
                {
                    var label = new Label
                    {
                        TextAlign = ContentAlignment.MiddleCenter,
                        AutoSize = false,
                        Size = new Size(70, 70),
                        Text = "",
                        Name = $"{j}{i}"
                    };
                    label.Location = new Point(j % 2 == 1 ? j * label.Size.Width : j * label.Size.Width - 25, i * label.Size.Height);

                    if ((i + 1) * label.Size.Height > Size.Height)
                        return;
                    if ((j + 1) * label.Size.Width > Size.Width)
                        break;
                    Controls.Add(label);
                }

            }

        }

        void LoadValues()
        {
            var option = (ViewOptions)comboBox1.SelectedIndex;
            foreach (Control control in Controls)
            {
                if (control.GetType() == typeof(Label) && int.TryParse(control.Name, out var _))
                {
                    var trida = _soutez.GetTrida(PageId);
                    var zak = trida.Rozlozeni[int.Parse(control.Name[0].ToString()) - 1,
                        int.Parse(control.Name[1].ToString()) - 1];
                    if (zak == null)
                    {
                        control.Text = @"XX";
                        continue;
                    }
                    switch (option)
                    {
                        case ViewOptions.Id:
                            control.Text = zak.Id.ToString();
                            break;
                        case ViewOptions.Kategorie:
                            control.Text = zak.Kategorie.ToRoman();
                            break;
                        case ViewOptions.Škola:
                            control.Text = zak.Skola.ToString();
                            break;
                    }
                }
            }
        }

        private void buttonBackward_Click(object sender, EventArgs e)
        {
            PageId = PageId < 1 ? 0 : PageId - 1;
            labelClassId.Text = ClassId + (PageId + 1);
        }

        private void buttonForward_Click(object sender, EventArgs e)
        {
            PageId = PageId < _soutez.PocetTrid - 1 ? PageId + 1 : PageId;
            labelClassId.Text = ClassId + (PageId + 1);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadValues();
        }
    }
}
