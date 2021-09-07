using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI.NeuralNetwork
{
    public interface INeuronReceptor
    {
        Dictionary<INeuronSignal, INeuralFactor> Input { get; }
    }
}
