using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork
{
    public interface INeuralFactor
    {
        double Weight { get; set; }
        double H_Vector { get; set; }
        double Last_H_Vector { get; }

        void ApplyWeightChange(ref double learningRate);
        void ResetWeightChange();
    }
}
