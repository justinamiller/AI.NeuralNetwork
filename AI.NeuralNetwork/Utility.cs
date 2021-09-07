using AI.NeuralNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork
{
    class Utility
    {
        internal static void Initialize(NeuralNet net, int randomSeed, int inputNeuronCount, 
            int hiddenNeuronCount, int outputNeuronCount)
        {

            int i, j;
            Random rand;

            rand = new Random(randomSeed);

            net.m_inputLayer = new NeuralLayer();
            net.m_outputLayer = new NeuralLayer();
            net.m_hiddenLayer = new NeuralLayer();

            for (i = 0; i < inputNeuronCount; i++)
                net.m_inputLayer.Add(new Neuron(0));

            for (i = 0; i < outputNeuronCount; i++)
                net.m_outputLayer.Add(new Neuron(rand.NextDouble()));

            for (i = 0; i < hiddenNeuronCount; i++)
                net.m_hiddenLayer.Add(new Neuron(rand.NextDouble()));

            // wire-up input layer to hidden layer
            for (i = 0; i < net.m_hiddenLayer.Count; i++)
                for (j = 0; j < net.m_inputLayer.Count; j++)
                    net.m_hiddenLayer[i].Input.Add(net.m_inputLayer[j], new NeuralFactor(rand.NextDouble()));

            // wire-up output layer to hidden layer
            for (i = 0; i < net.m_outputLayer.Count; i++)
                for (j = 0; j < net.m_hiddenLayer.Count; j++)
                    net.m_outputLayer[i].Input.Add(net.HiddenLayer[j], new NeuralFactor(rand.NextDouble()));
        }

        private static void CalculateErrors(NeuralNet net, double[] desiredResults)
        {
            int i, j;
            double temp, error;
            INeuron outputNode, hiddenNode;



            // Calcualte output error values 
            for (i = 0; i < net.m_outputLayer.Count; i++)
            {
                outputNode = net.m_outputLayer[i];
                temp = outputNode.Output;

                outputNode.Error = (desiredResults[i] - temp) * SigmoidDerivative(temp); //* temp * (1.0F - temp);
            }

            // calculate hidden layer error values
            for (i = 0; i < net.m_hiddenLayer.Count; i++)
            {
                hiddenNode = net.m_hiddenLayer[i];
                temp = hiddenNode.Output;

                error = 0;
                for (j = 0; j < net.m_outputLayer.Count; j++)
                {
                    outputNode = net.m_outputLayer[j];
                    error += (outputNode.Error * outputNode.Input[hiddenNode].Weight) * SigmoidDerivative(temp);// *(1.0F - temp);                   
                }

                hiddenNode.Error = error;

            }
        }

        private static double SigmoidDerivative(double value)
        {
            return value * (1.0F - value);
        }

        internal static void PreparePerceptionLayerForPulse(NeuralNet net, double[] input)
        {
            int i;

            if (input.Length != net.m_inputLayer.Count)
                throw new ArgumentException(string.Format("Expecting {0} inputs for this net", net.m_inputLayer.Count));

            // initialize data
            for (i = 0; i < net.m_inputLayer.Count; i++)
                net.m_inputLayer[i].Output = input[i];

        }

        internal static void CalculateAndAppendTransformation(NeuralNet net)
        {
            int i, j;
            INeuron outputNode, inputNode, hiddenNode;

            // adjust output layer weight change
            for (j = 0; j < net.m_outputLayer.Count; j++)
            {
                outputNode = net.m_outputLayer[j];

                for (i = 0; i < net.m_hiddenLayer.Count; i++)
                {
                    hiddenNode = net.m_hiddenLayer[i];
                    outputNode.Input[hiddenNode].H_Vector += outputNode.Error * hiddenNode.Output;
                }

                outputNode.Bias.H_Vector += outputNode.Error * outputNode.Bias.Weight;
            }

            // adjust hidden layer weight change
            for (j = 0; j < net.m_hiddenLayer.Count; j++)
            {
                hiddenNode = net.m_hiddenLayer[j];

                for (i = 0; i < net.m_inputLayer.Count; i++)
                {
                    inputNode = net.m_inputLayer[i];
                    hiddenNode.Input[inputNode].H_Vector += hiddenNode.Error * inputNode.Output;
                }

                hiddenNode.Bias.H_Vector += hiddenNode.Error * hiddenNode.Bias.Weight;
            }

        }

        internal static void BackPropogation_TrainingSession(NeuralNet net, double[] input, double[] desiredResult)
        {
            PreparePerceptionLayerForPulse(net, input);
            net.Pulse();
            CalculateErrors(net, desiredResult);
            CalculateAndAppendTransformation(net);
        }
    }
}
