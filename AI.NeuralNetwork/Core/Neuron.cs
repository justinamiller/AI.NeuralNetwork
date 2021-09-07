using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork.Core
{
    class Neuron : INeuron
    {
        public Neuron(double bias)
        {
            m_bias = new NeuralFactor(bias);
            m_error = 0;
            m_input = new Dictionary<INeuronSignal, INeuralFactor>();
        }

        private readonly object m_lockGate = new object();
        private Dictionary<INeuronSignal, INeuralFactor> m_input;
        double m_output, m_error, m_lastError;
        INeuralFactor m_bias;


        public double Output
        {
            get { return m_output; }
            set { m_output = value; }
        }

        public Dictionary<INeuronSignal, INeuralFactor> Input
        {
            get { return m_input; }
        }

        public void Pulse(INeuralLayer layer)
        {
            lock (m_lockGate)
            {
                m_output = 0;

                foreach (KeyValuePair<INeuronSignal, INeuralFactor> item in m_input)
                    m_output += item.Key.Output * item.Value.Weight;

                m_output += m_bias.Weight;

                m_output = NeuralNet.Sigmoid(m_output);
            }
        }

        public INeuralFactor Bias
        {
            get { return m_bias; }
            set { m_bias = value as NeuralFactor; }
        }

        public double Error
        {
            get { return m_error; }
            set
            {
                m_lastError = m_error;
                m_error = value;
            }
        }

        public void ApplyLearning(INeuralLayer layer, ref double learningRate)
        {
            foreach (KeyValuePair<INeuronSignal, INeuralFactor> m in m_input)
                m.Value.ApplyWeightChange(ref learningRate);

            m_bias.ApplyWeightChange(ref learningRate);
        }

        public void InitializeLearning(INeuralLayer layer)
        {
            foreach (KeyValuePair<INeuronSignal, INeuralFactor> m in m_input)
                m.Value.ResetWeightChange();

            m_bias.ResetWeightChange();
        }

        public double LastError
        {
            get { return m_lastError; }
            set { m_lastError = value; }
        }
    }
}
