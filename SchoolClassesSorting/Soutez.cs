using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using OfficeOpenXml;

namespace SchoolClassesSorting
{
    public class Soutez
    {
        private List<Zak> _zaci;

        private readonly List<Zak> _rozrazeniZaci = new List<Zak>();

        public readonly int PocetTrid;

        public readonly int PocetZakuNaTridu;

        private readonly List<Trida> _tridy;

        public Trida GetTrida(int i)
        {
            if (i < 0 || i >= _tridy.Count)
                return null;
            return _tridy.ElementAt(i);
        }

        public Soutez(List<Zak> zaci, int pocetTrid, int pocetZakuNaTridu)
        {
            _zaci = zaci;
            _tridy = new List<Trida>();
            PocetTrid = pocetTrid;
            PocetZakuNaTridu = pocetZakuNaTridu;
            if (_zaci.ElementAt(0).Id != -1)
            {
                CleanCompetition();
                LoadData();
            }
        }

        public int PocetZaku()
        {
            return _zaci.Count;
        }

        private void CleanCompetition()
        {
            var random = new Random();
            _zaci = _zaci.OrderBy(item => random.Next()).ToList();
            _rozrazeniZaci.Clear();
            _tridy.Clear();
            for (var i = 0; i < PocetTrid; i++)
                _tridy.Add(new Trida(PocetZakuNaTridu));
        }

        public bool Rozrad()
        {
            CleanCompetition();
            var idZaka = 0;
            foreach (var trida in _tridy)
            {
                for (var x = 0; x < trida.Rozlozeni.GetLength(0); x++)
                {
                    for (var y = 0; y < trida.Rozlozeni.GetLength(1); y++)
                    {
                        idZaka++;
                        foreach (var zak in _zaci)
                        {
                            if (_rozrazeniZaci.Contains(zak))
                                continue;
                            if (trida.AcceptablePlace(zak, x, y))
                            {
                                zak.Id = idZaka;
                                trida.Rozlozeni[x, y] = zak;
                                _rozrazeniZaci.Add(zak);
                                break;
                            }
                        }
                    }
                }
            }

            return _rozrazeniZaci.Count == _zaci.Count;
        }

        private void LoadData()
        {
            var idZaka = 0;
            foreach (var trida in _tridy)
            {
                for (var x = 0; x < trida.Rozlozeni.GetLength(0); x++)
                {
                    for (var y = 0; y < trida.Rozlozeni.GetLength(1); y++)
                    {
                        idZaka++;
                        foreach (var zak in _zaci)
                        {
                            if (_rozrazeniZaci.Contains(zak))
                                continue;
                            if (zak.Id == idZaka)
                            {
                                trida.Rozlozeni[x, y] = zak;
                                _rozrazeniZaci.Add(zak);
                                break;
                            }
                        }
                    }
                }
            }
        }

        public string CreateXlxsFile()
        {
            if (_rozrazeniZaci.Count == 0)
                return string.Empty;
            var now = DateTime.Now;
            string directory =
                $"Vysledky\\{now.Day}.{now.Month}. {now.Hour}.{now.Minute}.{now.Second}.{now.Millisecond};{_tridy.Count} trid";
            Directory.CreateDirectory(directory);
            var newFile = new FileInfo(AppDomain.CurrentDomain.BaseDirectory + directory + @"\Zaci.xlsx");
            using (var package = new ExcelPackage(newFile))
            {
                var worksheet = package.Workbook.Worksheets.Add("Zaci");
                worksheet.Cells[1, 3].Value = PocetTrid;
                worksheet.Cells[1, 4].Value = PocetZakuNaTridu;
                worksheet.Cells[2, 1].Value = "ID";
                worksheet.Cells[2, 2].Value = "Jméno";
                worksheet.Cells[2, 3].Value = "Kategorie";
                worksheet.Cells[2, 4].Value = "Škola";
                worksheet.Column(2).Width *= 2.3;

                int radek = 3;
                foreach (var trida in _tridy)
                {
                    for (var x = 0; x < trida.Rozlozeni.GetLength(0); x++)
                    {
                        for (var y = 0; y < trida.Rozlozeni.GetLength(1); y++)
                        {
                            var zak = trida.Rozlozeni[x, y];
                            if (zak != null)
                            {
                                worksheet.Cells["A" + radek].Value = zak.Id;
                                worksheet.Cells["B" + radek].Value = zak.Jmeno;
                                worksheet.Cells["C" + radek].Value = zak.Kategorie.ToRoman();
                                worksheet.Cells["D" + radek].Value = zak.Skola.ToString();
                                radek++;
                            }
                        }
                    }

                    radek += 2;
                }

                package.Save();
            }

            return newFile.FullName;
        }
    }
}
