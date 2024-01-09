using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lr3
{
    internal class Warhole
    {
        private int gold;
        public static int unitsAmount;
        private Mine mine;
        private bool isWorking = true;
        private List<Task> minerTasks = new List<Task>();
        private Task task;

        private Mutex mutex = new Mutex();

        public Warhole(Mine mine)
        {
            gold = 100;
            unitsAmount = 0;
            this.mine = mine;
        }

        public void recieveGold(int gold)
        {
            mutex.WaitOne();

            try
            {
                if (gold == 0)
                {
                    stop();
                }
                else
                {
                    this.gold += gold;
                    Console.WriteLine($"Warhole: +{gold} gold. Current gold: {this.gold}");
                }
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }

        private void stop()
        {
            isWorking = false;
        }

        private void createMiner()
        {
            task = Task.Run(() =>
            {
                Miner miner = new Miner(mine, this);
                miner.work();
            });

            gold -= 100;
            minerTasks.Add(task);
        }

        public void work()
        {
            while (isWorking)
            {
                if (gold >= 100)
                {
                    createMiner();
                }
            }

            Task.WaitAll(minerTasks.ToArray());
            Console.WriteLine("Warhole: stopped working. All miners tasks are stopped too.");
            return;
        }



    }
}
