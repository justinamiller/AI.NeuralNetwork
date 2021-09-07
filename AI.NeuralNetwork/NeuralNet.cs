using AI.NeuralNetwork.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork
{
    public enum TrainingType
    {
        BackPropogation=0
    }


    public class NeuralNet : INeuralNet
    {
        public NeuralNet()
        {
            m_learningRate = 0.5;
        }

        private readonly object m_lockGate = new object();
        internal INeuralLayer m_inputLayer;
        internal INeuralLayer m_outputLayer;
        internal INeuralLayer m_hiddenLayer;
        private double m_learningRate;


        public INeuralLayer PerceptionLayer
        {
            get { return m_inputLayer; }
        }

        public INeuralLayer HiddenLayer
        {
            get { return m_hiddenLayer; }
        }

        public INeuralLayer OutputLayer
        {
            get { return m_outputLayer; }
        }

        public double LearningRate
        {
            get { return m_learningRate; }
            set { m_learningRate = value; }
        }

        public void Pulse()
        {
            lock (m_lockGate)
            {
                m_hiddenLayer.Pulse(this);
                m_outputLayer.Pulse(this);
            }
        }

        public void ApplyLearning()
        {
            lock (m_lockGate)
            {
                m_hiddenLayer.ApplyLearning(this);
                m_outputLayer.ApplyLearning(this);
            }
        }

        public void InitializeLearning()
        {
            lock (m_lockGate)
            {
                m_hiddenLayer.InitializeLearning(this);
                m_outputLayer.InitializeLearning(this);
            }
        }

        public void Train(double[][] inputs, double[][] expected, TrainingType type, int iterations)
        {
            int i, j;

            switch (type)
            {
                case TrainingType.BackPropogation:

                    lock (m_lockGate)
                    {

                        for (i = 0; i < iterations; i++)
                        {

                            InitializeLearning(); // set all weight changes to zero

                            for (j = 0; j < inputs.Length; j++)
                                Utility.BackPropogation_TrainingSession(this, inputs[j], expected[j]);

                            ApplyLearning(); // apply batch of cumlutive weight changes
                        }

                    }
                    break;
                default:
                    throw new ArgumentException("Unexpected TrainingType");
            }
        }

        public void Initialize(int randomSeed,
            int inputNeuronCount, int hiddenNeuronCount, int outputNeuronCount)
        {
            Utility.Initialize(this, randomSeed, inputNeuronCount, hiddenNeuronCount, outputNeuronCount);
        }

        public void PreparePerceptionLayerForPulse(double[] input)
        {
            Utility.PreparePerceptionLayerForPulse(this, input);
        }

        public static double Sigmoid(double value)
        {
            return 1 / (1 + Math.Exp(-value));
        }
    }
}
