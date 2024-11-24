using Analytics.Models;

namespace Analytics
{
    public class Smoothing
    {
        public static List<DataPoint> SmoothData(List<DataPoint> data, int windowSize)
        {
            if (windowSize <= 0)
            {
                throw new ArgumentException("Window size must be greater than zero.");
            }

            List<DataPoint> smoothedData = new List<DataPoint>();

            // Извлечение значений из DataPoint
            List<double> values = new List<double>();
            foreach (var point in data)
            {
                values.Add(point.Value);
            }

            // Применение скользящего среднего
            var smoothedValues = SimpleMovingAverage(values.ToArray(), windowSize);

            // Создание нового списка DataPoint с сглаженными значениями
            for (int i = 0; i < data.Count; i++)
            {
                smoothedData.Add(new DataPoint
                {
                    Date = data[i].Date,
                    Value = smoothedValues[i]
                });
            }

            return smoothedData;
        }

        private static double[] SimpleMovingAverage(double[] values, int windowSize)
        {
            double[] result = new double[values.Length];
            for (int i = 0; i < values.Length; i++)
            {
                int start = Math.Max(0, i - windowSize + 1);
                int end = i + 1;
                double sum = 0;
                for (int j = start; j < end; j++)
                {
                    sum += values[j];
                }
                result[i] = sum / (end - start);
            }
            return result;
        }


        public static List<DataPoint> SmoothDataSpline(List<DataPoint> data, double smoothingFactor)
        {
            if (smoothingFactor <= 0 || smoothingFactor > 1)
            {
                throw new ArgumentException("Smoothing factor must be between 0 and 1.");
            }

            List<DataPoint> smoothedData = new List<DataPoint>();

            // Инициализация первого сглаженного значения как первое наблюдение
            double smoothedValue = data[0].Value;

            // Проход по данным для вычисления сглаженных значений
            for (int i = 0; i < data.Count; i++)
            {
                double currentValue = data[i].Value;
                smoothedValue = smoothingFactor * currentValue + (1 - smoothingFactor) * smoothedValue;

                smoothedData.Add(new DataPoint
                {
                    Date = data[i].Date,
                    Value = smoothedValue
                });
            }

            return smoothedData;
        }
    }
}
