using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PSP_E1_A8_Markel
{
    public partial class Form1 : Form
    {
        private BlockingCollection<Fitxategia> fitx;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        { }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            listBox1.Items.Clear();

            fitx = new BlockingCollection<Fitxategia>
            {
                new Fitxategia("Fitxategi 1", 100),
                new Fitxategia("Fitxategi 2", 200),
                new Fitxategia("Fitxategi 3", 80),
                new Fitxategia("Fitxategi 4", 250),
                new Fitxategia("Fitxategi 5", 180)
            };

            fitx.CompleteAdding();

            Task t1 = Task.Run(() => prozesatuFitx(label1, progressBar1));
            Task t2 = Task.Run(() => prozesatuFitx(label2, progressBar2));

            await Task.WhenAll(t1, t2);

            button1.Enabled = true;
        }

        private async Task prozesatuFitx(Label lblProgreso, ProgressBar progressBar)
        {
            foreach (var f in fitx.GetConsumingEnumerable())
            {
                int deskargatuta = 0;

                Invoke(new Action(() =>
                {
                    if (progressBar != null)
                    {
                        progressBar.Minimum = 0;
                        progressBar.Maximum = f.Tamaina;
                        progressBar.Value = 0;
                    }
                }));

                while (deskargatuta < f.Tamaina)
                {
                    await Task.Delay(200);
                    deskargatuta += 10;

                    if (deskargatuta > f.Tamaina)
                    {
                        deskargatuta = f.Tamaina;
                    }

                    Invoke(new Action(() =>
                    {
                        if (lblProgreso != null)
                        {
                            lblProgreso.Text = $"{f.Izena} {deskargatuta}MB / {f.Tamaina}MB";
                        }
                        if (progressBar != null)
                        {
                            progressBar.Value = deskargatuta;
                        }
                    }));
                }

                Invoke(new Action(() =>
                {
                    listBox1.Items.Add($"{f.Izena} ({f.Tamaina}) OK!");
                }));
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}