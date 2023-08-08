using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading; 
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace Предсказатель
{
    public partial class Form1 : Form
    {
        private const string APP_NAME = "ULTIMATE PREDICTOR";
      
        private readonly string PREDICTIONS_CONFIG_PATH = $"{Environment.CurrentDirectory}\\PredictorConfig.json";

        private string[] _predictions;

        Random random = new Random();
        public Form1()
        {
            InitializeComponent();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            await Task.Run(()=>
            {
                for (int i = 1; i <= 100; i++)
                {
                    this.Invoke(new Action(() =>
                    {
                        UpdateProgressBar(i);
                        this.Text = $"%{i}";
                    }));
                    Thread.Sleep(20);
                }
            });
            var index = random.Next(_predictions.Length);
            MessageBox.Show(_predictions[index]+"!");
            progressBar1.Value = 0;
            this.Text = APP_NAME;
            button1.Enabled = true;
        }

        private void UpdateProgressBar(int i)
        {
            if (i == progressBar1.Maximum)
            {
                progressBar1.Maximum = i + 1;
                progressBar1.Value = i + 1;
                progressBar1.Maximum = i;
            }
            else
            {
                progressBar1.Value = i + 1;
            }
            progressBar1.Value = i;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = APP_NAME;

            try
            {
                var data = File.ReadAllText(PREDICTIONS_CONFIG_PATH);
                _predictions = JsonConvert.DeserializeObject<string[]>(data);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);                
            }
            finally
            {
                if (_predictions == null)
                {
                    this.Close();
                }
                else if(_predictions.Length == 0)
                {
                    MessageBox.Show("Предсказания закончились, кина не будет !!! =)");
                    Close();
                }
            } 
        }
    }
}
