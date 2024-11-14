using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace Race
{
    class Game
    {
        List<Cars> cars;
        bool raceInProgress;
        Cars winnerCar;
        public List<Cars> Cars => cars;
        public delegate void CarAction();
        public event CarAction StartRace;
        public event CarAction StepRace;
        public event EventHandler<string> FinishedDrive;

        public Game()
        {
            cars = new List<Cars>();
            raceInProgress = false;
        }

        public void AddCar(Cars car)
        {
            car.Finished += OnCarFinished;
            cars.Add(car);
        }

        public void Start()
        {
            Console.WriteLine("Race started 3.2.1!\n");

            StartRace?.Invoke();

            Thread.Sleep(3000);

            raceInProgress = true;
            Timer timer = new Timer(RaceStep, null, 0, 500);

            while (raceInProgress) { }

            timer.Dispose();

        }

        private void RaceStep(object state)
        {
            StepRace?.Invoke();

            Console.Clear();
            Console.WriteLine("Posision:");

            foreach (var car in cars)
            {
                car.Description();
            }

            foreach (var car in cars)
            {
                car.Print();
            }
        }
        public void DrivePrepare()
        {
            Console.WriteLine("Starting positions:");
            foreach (var car in cars)
            {
                car.Description();
            }
            Console.WriteLine();
        }

        private void OnCarFinished(object sender, EventArgs e)
        {
            if (sender is Cars car && raceInProgress)
            {
                raceInProgress = false;
                winnerCar = car;

                Console.Clear();
                FinishedDrive?.Invoke(this, $"Winner: #{winnerCar.Number} - {winnerCar.Name}");
            }

        }
    }
}