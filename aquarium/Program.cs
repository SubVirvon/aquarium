using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aquarium
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Aquarium aquarium = new Aquarium(new List<Fish>(), 6);

            aquarium.Work();
        }
    }

    class Aquarium
    {
        private List<Fish> _fishes;
        private int _maxFishesCount;

        public Aquarium(List<Fish> fishes, int maxFishesCount)
        {
            _fishes = fishes;
            _maxFishesCount = maxFishesCount;
        }

        public void Work()
        {
            bool isFinish = false;
            int time = 1;

            while (isFinish == false)
            {
                Console.SetCursorPosition(0, 10);

                for (int i = 0; i < _fishes.Count; i++)
                {
                    _fishes[i].SkipTime(time);

                    Console.Write($"{i + 1} ");

                    _fishes[i].ShowInfo();
                }

                Console.SetCursorPosition(0, 0);

                ShowCommands();

                Console.Clear();
            }
        }

        private void ShowCommands()
        {
            const string CommandAddFish = "1";
            const string CommandRemoveFish = "2";

            Console.Write($"Выбирете команду:\n{CommandAddFish} Добавить рыбку\n{CommandRemoveFish} Убрать рыбу\nВведите команду (или нажмите enter, чтобы пропустить): ");

            string input = Console.ReadLine();

            switch (input)
            {
                case CommandAddFish:
                    AddFish();
                    break;
                case CommandRemoveFish:
                    RemoveFish();
                    break;
            }
        }

        private void AddFish()
        {
            if(_fishes.Count < _maxFishesCount)
            {
                _fishes.Add(new Fish());

                Console.WriteLine("Рыбка была добавлена");
            }
            else
            {
                Console.WriteLine("Аквариум переполнен");
            }

            Console.ReadKey();
        }

        private void RemoveFish()
        {
            bool isNumber = false;

            while (isNumber == false)
            {
                Console.Clear();
                Console.Write("Введите номер рыбки которую хотите достать: ");

                string input = Console.ReadLine();

                if (int.TryParse(input, out int index))
                {
                    isNumber = true;

                    if (index >= 0 && index < _fishes.Count - 1)
                    {
                        _fishes.RemoveAt(index - 1);
                        Console.WriteLine("Рыбка удалена");
                    }
                    else
                    {
                        Console.WriteLine("Рыбка не найдена");
                    }
                }
                else
                {
                    Console.WriteLine("Некоректная команда");
                }

                Console.ReadKey();
            }
        }
    }

    class Fish
    {
        private int _age;
        private int _lifetime;
        public Fish()
        {
            _lifetime = SetLifetime();
            _age = 0;
        }

        private int SetLifetime()
        {
            Random random = new Random();
            int minLifetime = 2;
            int maxLifetime = 20;

            return random.Next(minLifetime, maxLifetime + 1);
        }

        public void SkipTime(int time)
        {
            _age += time;

            if(_age >= _lifetime)
                _age = _lifetime;
        }
        
        public void ShowInfo()
        {
            if (_age < _lifetime)
                Console.WriteLine($"Возраст: {_age} дней");
            else
                Console.WriteLine($"Рыба умерла в возрасте {_age} дней");
        }
    }
}
