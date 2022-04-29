using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.UseCases.DisplayOnly.UseCase1
{
    /// <summary>
    /// Represents example data that might be gathered by some form of sensor.
    /// </summary>
    public class SensorData
    {
        /// <summary>
        /// Initializes this class's property with default values and starts tasks to
        /// simulate live data generation, as a sensor would do.
        /// </summary>
        public SensorData()
        {
            this.AverageTempAcrossWeek = 27.6;
            this.AveragePowerConsumptionAcrossWeek = 170.59;
            this.CurrentTemperature = 35.9;
            this.CurrentAirHumidity = 70;
            this.IsPoweredOn = true;

            var random = new Random();

            // To simulate the data being gathered in real time
            _ = Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(random.Next(100, 10001));

                    CurrentTemperature = random.Next(0, 101) * 1.0;
                }
            });

            _ = Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(random.Next(100, 10001));

                    CurrentAirHumidity = random.Next(10, 101) * 1.0;
                }
            });

            _ = Task.Run(() =>
            {
                while (true)
                {
                    Task.Delay(random.Next(5000, 30001));

                    IsPoweredOn = !IsPoweredOn;
                }
            });
        }

        public double AverageTempAcrossWeek { get; private set; }

        public double AveragePowerConsumptionAcrossWeek { get; private set; }

        public double CurrentTemperature { get; private set; }

        public double CurrentAirHumidity { get; private set; }

        public bool IsPoweredOn { get; private set; }
    }
}
