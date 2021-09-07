using AI.NeuralNetwork;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            high = .99;
            low = .01;
            mid = .5;
        }

        #region Member Variables

        private NeuralNet net;
        double high, mid, low;

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            #region Declarations

            double ll, lh, hl, hh;
            int count, iterations;
            bool verbose;
            double[][] input, output;
            StringBuilder bld;

            #endregion

            #region Initialization

            net = new NeuralNet();
            bld = new StringBuilder();
            txtDebug.Text = "";
           input = new double[4][];
            input[0] = new double[] { high, high };
            input[1] = new double[] { low, high };
            input[2] = new double[] { high, low };
            input[3] = new double[] { low, low };

            output = new double[4][];
            output[0] = new double[] { low };
            output[1] = new double[] { high };
            output[2] = new double[] { high };
            output[3] = new double[] { low };

            verbose = true;
            count = 0;
            iterations = 5;

            #endregion

            #region Execution

            // initialize with 
            //   2 perception neurons
            //   2 hidden layer neurons
            //   1 output neuron
            net.Initialize(1, 2, 2, 1);

            do
            {
                count++;

                net.LearningRate = 3;
                net.Train(input, output, TrainingType.BackPropogation, iterations);

                net.PerceptionLayer[0].Output = low;
                net.PerceptionLayer[1].Output = low;

                net.Pulse();

                ll = net.OutputLayer[0].Output;

                net.PerceptionLayer[0].Output = high;
                net.PerceptionLayer[1].Output = low;

                net.Pulse();

                hl = net.OutputLayer[0].Output;

                net.PerceptionLayer[0].Output = low;
                net.PerceptionLayer[1].Output = high;

                net.Pulse();

                lh = net.OutputLayer[0].Output;

                net.PerceptionLayer[0].Output = high;
                net.PerceptionLayer[1].Output = high;

                net.Pulse();

                hh = net.OutputLayer[0].Output;

                #region verbose

                if (verbose)
                {
                    bld.Remove(0, bld.Length);
                    bld.Append("PERCEPTION LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                    foreach (INeuron pn in net.PerceptionLayer)
                        AppendNeuronInfo(bld, pn);

                    bld.Append("\nHIDDEN LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                    foreach (INeuron hn in net.HiddenLayer)
                        AppendNeuronInfo(bld, hn);

                    bld.Append("\nOUTPUT LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                    foreach (INeuron on in net.OutputLayer)
                        AppendNeuronInfo(bld, on);

                    bld.Append("\n");

                    bld.Append("hh: \t").Append(hh.ToString()).Append(" \t< .5\n");
                    bld.Append("ll: \t").Append(ll.ToString()).Append(" \t< .5\n");

                    bld.Append("hl: \t").Append(hl.ToString()).Append(" \t> .5\n");
                    bld.Append("lh: \t").Append(lh.ToString()).Append(" \t> .5\n");

                    // MessageBox.Show(bld.ToString());
                }

                #endregion

                this.Refresh();
                if(count>200)
                    break;
            }
            // really train this thing well...
            while (hh > (mid + low) / 2
                || lh < (mid + high) / 2
                || hl < (mid + low) / 2
                || ll > (mid + high) / 2);


            if (verbose)
            {
                this.Refresh();
                txtDebug.Text = bld.ToString();
            }

            net.PerceptionLayer[0].Output = low;
            net.PerceptionLayer[1].Output = low;

            net.Pulse();

            ll = net.OutputLayer[0].Output;

            net.PerceptionLayer[0].Output = high;
            net.PerceptionLayer[1].Output = low;

            net.Pulse();

            hl = net.OutputLayer[0].Output;

            net.PerceptionLayer[0].Output = low;
            net.PerceptionLayer[1].Output = high;

            net.Pulse();

            lh = net.OutputLayer[0].Output;

            net.PerceptionLayer[0].Output = high;
            net.PerceptionLayer[1].Output = high;

            net.Pulse();

            hh = net.OutputLayer[0].Output;

            bld.Remove(0, bld.Length);
            bld.Append((count * iterations).ToString()).Append(" iterations required for training\n");

            bld.Append("hh: ").Append(hh.ToString()).Append(" < .5\n");
            bld.Append("ll: ").Append(ll.ToString()).Append(" < .5\n");

            bld.Append("hl: ").Append(hl.ToString()).Append(" > .5\n");
            bld.Append("lh: ").Append(lh.ToString()).Append(" > .5\n");

            MessageBox.Show(bld.ToString());

            #endregion

        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            if (net == null)
            {
                lblResult.Text = "Error: must 'perform X-OR training'";
                return;
            }


            #region Declarations

            bool result, verbose;
            StringBuilder bld;

            #endregion

            #region Initialization

            verbose = false;
            bld = new StringBuilder();

            #endregion

            #region Execution

            net.PerceptionLayer[0].Output = (ckA.Checked) ? high : low;
            net.PerceptionLayer[1].Output = (ckB.Checked) ? high : low;
            net.Pulse();
            result = net.OutputLayer[0].Output > .5;

            #region verbose

            if (verbose)
            {
                bld.Remove(0, bld.Length);

                bld.Append("PERCEPTION LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                foreach (INeuron pn in net.PerceptionLayer)
                    AppendNeuronInfo(bld, pn);

                bld.Append("\nHIDDEN LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                foreach (INeuron hn in net.HiddenLayer)
                    AppendNeuronInfo(bld, hn);

                bld.Append("\nOUTPUT LAYER <<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<\n");
                foreach (INeuron on in net.OutputLayer)
                    AppendNeuronInfo(bld, on);

                bld.Append("\n");

                MessageBox.Show(bld.ToString());
            }

            #endregion

            lblResult.Text = result.ToString();

            #endregion
        }

        private static void AppendNeuronInfo(StringBuilder bld, INeuron neuron)
        {
            #region Declarations

            int i;
            double value;

            #endregion

            #region Initialization

            i = 1;
            value = 0;

            #endregion

            #region Execution

            bld.Append("========== NEURON ========== \n");
            bld.Append(" output\t: ").Append(neuron.Output.ToString()).Append("\n");
            bld.Append(" error\t: ").Append(neuron.Error.ToString());
            bld.Append("\t last error:\t").Append(neuron.LastError.ToString()).Append("\n");
            //bld.Append(" bias value \t: ").Append(neuron.BiasValue.ToString()).Append("\n");
            bld.Append(" bias\t: ").Append(neuron.Bias.Weight.ToString()).Append("\n\n");



            foreach (KeyValuePair<INeuronSignal, INeuralFactor> f in neuron.Input)
            {
                bld.Append("input ").Append(i++.ToString()).Append(" value= ").Append(f.Key.Output.ToString()); //.Append("\n");
                bld.Append("  \tweight = ").Append(f.Value.Weight).Append("\n");


                value += f.Value.Weight * f.Key.Output;
                //bld.Append("\tSig(").Append((f.Key.Output * f.Value.Weight).ToString()).Append(")=").Append(Neuron.Sigmoid(f.Value.Weight + )).Append("\n");
            }

            bld.Append("parent.bias = ").Append(neuron.Bias.Weight).Append("\n");
            bld.Append("sigmoid=").Append(NeuralNet.Sigmoid(value + neuron.Bias.Weight)).Append("\n");
            bld.Append("============================ \n\n");

            #endregion


        }

    }
}