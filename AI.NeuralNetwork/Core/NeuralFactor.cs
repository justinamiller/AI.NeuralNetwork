using System;

namespace AI.NeuralNetwork.Core
{
    class NeuralFactor: INeuralFactor
    {
        public NeuralFactor(double weight)
        {
            m_weight = weight;
            m_lastDelta = m_delta = 0;
        }

        private double m_weight, m_lastDelta, m_delta;

        public double Weight
        {
            get { return m_weight; }
            set { m_weight = value; }
        }

        public double H_Vector
        {
            get { return m_delta; }
            set { m_delta = value; }
        }

        public double Last_H_Vector
        {
            get { return m_lastDelta; }
            //set { m_lastDelta = value; }
        }

        public void ApplyWeightChange(ref double learningRate)
        {
            m_lastDelta = m_delta;
            m_weight += m_delta * learningRate;
        }

        public void ResetWeightChange()
        {
            m_lastDelta = m_delta = 0;
        }
    }
}
