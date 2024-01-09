using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotnet_lr3
{
    internal class Miner
    {
        private int gold;
        private const int goldCapacity = 50;
        private Mine mine;
        private Warhole warhole;
        private bool isWorking;
        private string name;
        Random random;

        public Miner(Mine mine, Warhole warhole)
        {
            this.mine = mine;
            this.warhole = warhole;
            gold = 0;
            isWorking = true;
            random = new Random();
            Warhole.unitsAmount++;
            name = "Miner" + Warhole.unitsAmount.ToString();
            Console.WriteLine($"Created {name} with {goldCapacity} gold capacity");
        }

        private void getGold()
        {
            if (gold == 0)
                gold = mine.reduceGold(goldCapacity);
        }

        private void giveGold()
        {
            if (gold == 0)
            {
                Console.WriteLine($"{name}: I brought 0 gold, mine is empty. My work is done");
                warhole.recieveGold(gold);
                stop();
            }
            else
            {
                warhole.recieveGold(gold);
                gold = 0;
            }
        }

        private void stop()
        {
            isWorking = false;
        }

        private void walk()
        {
            TimeSpan delay = TimeSpan.FromSeconds(random.Next(1, 5));
            Thread.Sleep(delay);
        }

        public void work()
        {
            while (isWorking)
            {
                walk();
                getGold();
                walk();
                giveGold();
            }

            return;
        }

    }
}
