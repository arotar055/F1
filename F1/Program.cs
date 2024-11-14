using Race;
using System;
using System.Diagnostics;
using System.Runtime.ConstrainedExecution;
namespace Racce
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Game game = new Game();

                game.FinishedDrive += (sender, winner) =>
                {

                    Console.WriteLine($"\nFinish! {winner}");
                };

                Cars Formula1 = new Formula1("Formula1", 0, 1);
                Cars LightningMcQueen = new LightningMcQueen("LightningMcQueen", 0, 2);
                Cars NissanGTR = new NissanGTR("NissanGTR", 0, 3);
                Cars Ferrari = new Ferrari("Ferrari", 0, 4);

                game.AddCar(Formula1);
                game.AddCar(LightningMcQueen);
                game.AddCar(NissanGTR);
                game.AddCar(Ferrari);

                game.StartRace += () => game.DrivePrepare();
                game.StepRace += () =>
                {
                    foreach (var car in game.Cars)
                    {
                        car.RandomSpeed();
                        car.Drive();
                    }
                };

                game.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

    }
}