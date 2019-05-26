namespace MachineLearningLIB.ActivationFunctions
{
    public abstract class ActivationFunc
    {
        public double DendriteMaxGen => 0.5;
        public double DendriteMinGen => -0.5;

        public abstract double Function(double x);
        public abstract double Derivative(double x);
    }
}
