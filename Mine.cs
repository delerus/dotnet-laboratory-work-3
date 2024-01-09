using System;

namespace dotnet_lr3
{
    internal class Mine
    {
        private int gold;
        private Mutex mutex = new Mutex();

        public Mine(int gold)
        {
            this.gold = gold;
            Console.WriteLine($"Created Mine with {gold} gold");
        }

        public Mine()
        {
            gold = 1000;
            Console.WriteLine($"Created Mine with {gold} gold");
        }

        public int reduceGold(int gold)
        {
            mutex.WaitOne();

            try
            {
                this.gold -= gold;

                if (this.gold < 0)
                {
                    gold += this.gold;
                    this.gold = 0;
                }

                return gold;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }
}
