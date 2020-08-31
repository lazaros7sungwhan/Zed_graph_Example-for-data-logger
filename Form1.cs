using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace zedgraph_2
{
    public partial class Form1 : Form
    {
        double x, y;
        LineItem stage1;
        PointPairList stage2;
        GraphPane stage3;

        Random _rand;
        Stopwatch sw;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                stage3 = zedGraphControl1.GraphPane;
                stage3.Chart.Fill = new Fill(Color.White, Color.White, 180.0f);
                stage3.Title.Text = "testing graph";

                stage3.XAxis.Title.Text = "time(sec)";
                stage3.YAxis.Title.Text = "value";
                stage3.XAxis.MajorTic.Color = Color.Black;
                stage3.YAxis.MajorTic.Color = Color.Black;

            }
            catch
            {

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                if(!backgroundWorker1.IsBusy){backgroundWorker1.RunWorkerAsync();}
                
            }
            catch
            {

            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            sw = new Stopwatch();
            _rand = new Random();

            stage2 = new PointPairList();
            stage1 = stage3.AddCurve("random value", stage2, Color.Red,SymbolType.None);
            sw.Start();
            try
            {
                while (true)
                {
                    if(backgroundWorker1.CancellationPending==true){e.Cancel = true; break;}
                    x = Convert.ToDouble(sw.ElapsedMilliseconds)/1000;
                    y = _rand.NextDouble();

                    stage2.Add(x, y);
                    zedGraphControl1.AxisChange();
                    zedGraphControl1.Refresh();
                    Thread.Sleep(200);
                }
            }
            catch
            {
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                sw.Stop();
                backgroundWorker1.CancelAsync();
            }
            catch
            {

            }

        }
    }
}
