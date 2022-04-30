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
        // To simulate sensor getting data in real time.
        private Random random;

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

            this.random = new Random();
            var timer = new Timer(this.Elapsed, null, 2000, 1000);
        }

        public double AverageTempAcrossWeek { get; private set; }

        public double AveragePowerConsumptionAcrossWeek { get; private set; }

        public double CurrentTemperature { get; private set; }

        public double CurrentAirHumidity { get; private set; }

        public bool IsPoweredOn { get; private set; }


        private void Elapsed(object? state)
        {
            Console.WriteLine("Elapsed");
            this.CurrentTemperature = this.random.Next(0, 51);
            this.CurrentAirHumidity = this.random.Next(0, 101);

            if (this.random.Next(0, 11) == 10)
            {
                this.IsPoweredOn = !this.IsPoweredOn;
            }
        }
    }
}
