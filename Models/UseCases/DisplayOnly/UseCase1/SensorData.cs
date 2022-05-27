using Models.Metadata;
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
            this.AverageTemperature = 27.6;
            this.AveragePowerConsumption = 170.59;
            this.CurrentTemperature = 35.9;
            this.CurrentHumidity = 70;
            this.IsPoweredOn = true;

            this.random = new Random();
            var timer = new Timer(this.Elapsed, null, 2000, 1000);
        }

        public event EventHandler StateHasChanged;

        [ReadOnly]
        public double AverageTemperature { get; private set; }

        [ReadOnly]
        public double AveragePowerConsumption { get; private set; }

        [ReadOnly]
        public double CurrentTemperature { get; private set; }

        [ReadOnly]
        public double CurrentHumidity { get; private set; }

        [ReadOnly]
        [DataToggle]
        public bool IsPoweredOn { get; private set; }


        private void Elapsed(object? state)
        {
            Console.WriteLine("Sensor gathered new data");
            this.CurrentTemperature = this.random.Next(0, 51);
            this.CurrentHumidity = this.random.Next(0, 101);

            if (this.random.Next(0, 11) == 10)
            {
                this.IsPoweredOn = !this.IsPoweredOn;
            }

            this.StateHasChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
