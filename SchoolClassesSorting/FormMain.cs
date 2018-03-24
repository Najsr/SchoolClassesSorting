using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IniParser;
using IniParser.Model;
using OfficeOpenXml;

namespace SchoolClassesSorting
{
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private readonly FileIniDataParser _parser = new FileIniDataParser();

        private readonly string _configFile = AppDomain.CurrentDomain.BaseDirectory + "config.ini";

        private Config _config;

        private readonly List<Zak> _zaci = new List<Zak>();

        private void FormMain_Load(object sender, EventArgs e)
        {
            if (!File.Exists(_configFile))
            {
                File.Create(_configFile).Close();
                var iniData = new IniData();
                iniData.Sections.AddSection("Nastaveni");
                var sectionData = iniData.Sections.GetSectionData("Nastaveni");
                sectionData.Keys.AddKey("jmeno", "Jméno");
                sectionData.Keys.AddKey("kategorie", "Kategorie");
                sectionData.Keys.AddKey("skola", "Škola");
                sectionData.Keys.AddKey("headerRow", "1");
                sectionData.Keys.AddKey("pupilsPerClass", "30");
                sectionData.Keys.AddKey("pokusy", "5");
                _parser.WriteFile(_configFile, iniData, Encoding.UTF8);
                MessageBox.Show(@"Prosím upravte Config.ini", @"Upravte config", MessageBoxButtons.OK, MessageBoxIcon.Information);
                Application.Exit(new CancelEventArgs());
            }

            var data = _parser.ReadFile(_configFile)["Nastaveni"];
            _config = new Config { Jmeno = data["jmeno"], HeaderRow = int.Parse(data["headerRow"]), Kategorie = data["kategorie"], PupilsPerClass = int.Parse(data["pupilsPerClass"]), Skola = data["skola"], Pokusy = int.Parse(data["pokusy"]) };
            numericUpDownPupils.Value = _config.PupilsPerClass >= numericUpDownPupils.Minimum && _config.PupilsPerClass <= numericUpDownPupils.Maximum ? _config.PupilsPerClass : numericUpDownPupils.Minimum;
            numericUpDownTries.Value = _config.Pokusy >= numericUpDownTries.Minimum && _config.Pokusy <= numericUpDownTries.Maximum ? _config.Pokusy : numericUpDownTries.Minimum;
        }

        private void listViewPupils_ColumnWidthChanging(object sender, ColumnWidthChangingEventArgs e)
        {
            e.NewWidth = listViewPupils.Columns[e.ColumnIndex].Width;
            e.Cancel = true;
        }

        private void buttonOpenTable_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.Title = @"Prosím vyberte Excel soubor";
            openFileDialog1.FileName = string.Empty;
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                LoadData(new FileInfo(openFileDialog1.FileName));
                if (listViewPupils.Items.Count > 0)
                    buttonSort.Enabled = true;
            }
        }

        public void LoadData(FileInfo fileinfo)
        {
            var jmenoRow = ';';
            var kategorieRow = ';';
            var skolaRow = ';';
            using (var package = new ExcelPackage(fileinfo))
            {
                var worksheet = package.Workbook.Worksheets[1];
                for (var i = 65; i < 91; i++)
                {
                    var value = worksheet.Cells[((char)i).ToString() + _config.HeaderRow].Value;
                    if (value == null)
                        continue;
                    if (value.Equals(_config.Jmeno))
                        jmenoRow = (char)i;
                    else if (value.Equals(_config.Kategorie))
                        kategorieRow = (char)i;
                    else if (value.Equals(_config.Skola))
                        skolaRow = (char)i;
                }

                if (jmenoRow == ';' || kategorieRow == ';' || skolaRow == ';')
                {
                    MessageBox.Show(@"Hlavičky sloupců nenalezeny!", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                listViewPupils.View = View.Details;
                listViewPupils.Columns.Add("Jméno", 150, HorizontalAlignment.Left);
                listViewPupils.Columns.Add("Kategorie", 60, HorizontalAlignment.Left);
                listViewPupils.Columns.Add("Škola", 50, HorizontalAlignment.Left);
                var index = 2;
                while (true)
                {
                    var jmeno = worksheet.Cells[jmenoRow.ToString() + index].Value;
                    var kategorie = worksheet.Cells[kategorieRow.ToString() + index].Value;
                    var skola = worksheet.Cells[skolaRow.ToString() + index].Value;
                    if (jmeno == null || kategorie == null || skola == null)
                    {
                        break;
                    }
                    try
                    {
                        var zak = new Zak(jmeno.ToString(), Konstanty.RomanToInteger(kategorie.ToString()), char.ToUpper(Convert.ToChar(skola)));
                        listViewPupils.Items.Add(new ListViewItem(new[] { zak.Jmeno, zak.Kategorie.ToRoman(), zak.Skola.ToString() }));
                        _zaci.Add(zak);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                    index++;
                }
                numericUpDownClasses.Value = Math.Ceiling(listViewPupils.Items.Count / numericUpDownPupils.Value);
            }
        }

        private void numericUpDownClasses_ValueChanged(object sender, EventArgs e)
        {
            if (listViewPupils.Items.Count > 0 && Math.Ceiling(listViewPupils.Items.Count / numericUpDownPupils.Value) > numericUpDownClasses.Value)
            {
                numericUpDownClasses.Value = Math.Ceiling(listViewPupils.Items.Count / numericUpDownPupils.Value);
            }
        }

        private void numericUpDownPupils_ValueChanged(object sender, EventArgs e)
        {
            if (listViewPupils.Items.Count > 0 && Math.Ceiling(listViewPupils.Items.Count / numericUpDownPupils.Value) > numericUpDownClasses.Value)
            {
                numericUpDownClasses.Value = Math.Ceiling(listViewPupils.Items.Count / numericUpDownPupils.Value);
            }
        }

        private void listViewPupils_KeyDown(object sender, KeyEventArgs e)
        {
            if (Keys.Delete == e.KeyCode)
            {
                foreach (ListViewItem listViewItem in ((ListView)sender).SelectedItems)
                {
                    listViewItem.Remove();
                    var zak = _zaci.Find(x => x.Jmeno == listViewItem.SubItems[0].Text && x.Kategorie == Convert.ToByte(Konstanty.RomanToInteger(listViewItem.SubItems[1].Text)) && x.Skola == Convert.ToChar(listViewItem.SubItems[2].Text));
                    _zaci.Remove(zak);
                }
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            if (numericUpDownPupils.Value % 6 != 0)
            {
                MessageBox.Show(@"Počet žáků na třídu musí být dělitelný 6", @"ERROR", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            var soutez = new Soutez(_zaci, (int)numericUpDownClasses.Value, (int)numericUpDownPupils.Value);
            for (var i = 0; i < _config.Pokusy; i++)
            {
                if (soutez.Rozrad())
                {
                    var path = soutez.CreateXlxsFile();
                    if (!path.Equals(string.Empty))
                        Task.Run(() => Process.Start(path));
                    MessageBox.Show(@"Úspěšně rozřazeno!");
                    break;
                }
            }
        }

        private void buttonClassViewer_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = @"Excel Files|*.xls;*.xlsx;*.xlsm";
            openFileDialog1.Title = @"Prosím vyberte Excel soubor";
            openFileDialog1.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory + @"Vysledky";
            openFileDialog1.FileName = string.Empty;
            var dialogResult = openFileDialog1.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var soutez = LoadSoutez(new FileInfo(openFileDialog1.FileName));
                if (soutez == null)
                {
                    MessageBox.Show(@"Neplatný soubor!");
                }
                else
                {
                    var viewerForm = new ClassViewerForm(this, soutez);
                    viewerForm.Show();
                }
            }
        }

        private Soutez LoadSoutez(FileInfo fileInfo)
        {
            using (var package = new ExcelPackage(fileInfo))
            {
                var worksheet = package.Workbook.Worksheets[1];
                var pocetTridObj = worksheet.Cells[1, 3].Value;
                var pocetZakuNaTriduObj = worksheet.Cells[1, 4].Value;
                if (pocetTridObj == null || pocetZakuNaTriduObj == null || !int.TryParse(pocetTridObj.ToString(), out var pocetTrid) || !int.TryParse(pocetZakuNaTriduObj.ToString(), out var pocetZakuNaTridu))
                    return null;

                var localZaci = new List<Zak>();
                for (var i = 3; i <= pocetTrid * pocetZakuNaTridu + 3 + pocetTrid * 2; i++)
                {
                    var id = worksheet.Cells["A" + i].Value;
                    var jmeno = worksheet.Cells["B" + i].Value;
                    var kategorie = worksheet.Cells["C" + i].Value;
                    var skola = worksheet.Cells["D" + i].Value;
                    if (jmeno == null || kategorie == null || skola == null || id == null)
                    {
                        continue;
                    }
                    try
                    {
                        var zak = new Zak(jmeno.ToString(), Konstanty.RomanToInteger(kategorie.ToString()), char.ToUpper(Convert.ToChar(skola)))
                        { Id = Convert.ToInt32(id) };
                        localZaci.Add(zak);
                    }
                    catch (Exception)
                    {
                        // ignored
                    }

                }
                var soutez = new Soutez(localZaci, pocetTrid, pocetZakuNaTridu);

                return soutez;
            }
        }
    }
}
