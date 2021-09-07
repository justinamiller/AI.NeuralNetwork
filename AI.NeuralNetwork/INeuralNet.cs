using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork
{
    public interface INeuralNet
    {
        INeuralLayer PerceptionLayer { get; }
        INeuralLayer HiddenLayer { get; }
        INeuralLayer OutputLayer { get; }

        double LearningRate { get; set; }

        void Pulse();
        void ApplyLearning();
        void InitializeLearning();
    }
}
