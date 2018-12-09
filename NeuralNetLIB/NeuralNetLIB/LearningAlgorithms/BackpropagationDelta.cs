namespace NeuralNetLIB.LearningAlgorithms
{
    public class BackpropagationDelta
    {
        public double PartialDerivative;
        public double BiasUpdate;
        public double[] WeightUpdates;

        public BackpropagationDelta(int numberOfWeights)
        {
            WeightUpdates = new double[numberOfWeights];
            Initialize();
        }

        public void Initialize()
        {
            BiasUpdate = 0;
            PartialDerivative = 0;
            for (int i = 0; i < WeightUpdates.Length; i++)
            {
                WeightUpdates[i] = 0;
            }
        }
    }

}
