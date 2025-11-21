using System;
using System.Collections.Generic;

namespace OfficeEquipmentApp
{
    // ======= БАЗОВЫЙ КЛАСС =======

    public class OfficeEquipment
    {
        private string brand;
        private string model;
        private decimal price;
        private int power;

        public const decimal MIN_PRICE = 0;
        public const decimal MAX_PRICE = 200000;
        public const int MIN_POWER = 0;
        public const int MAX_POWER = 3000;

        public OfficeEquipment(string brand = "Unknown", string model = "Unknown",
                               decimal price = 0, int power = 0)
        {
            Brand = brand;
            Model = model;
            Price = price;
            PowerConsumption = power;
        }

        public string Brand
        {
            get => brand;
            set => brand = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public string Model
        {
            get => model;
            set => model = string.IsNullOrWhiteSpace(value) ? "Unknown" : value;
        }

        public decimal Price
        {
            get => price;
            set => price = (value >= MIN_PRICE && value <= MAX_PRICE) ? value : 0;
        }

        public int PowerConsumption
        {
            get => power;
            set => power = (value >= MIN_POWER && value <= MAX_POWER) ? value : 0;
        }

        public virtual void PrintInfo()
        {
            Console.WriteLine("Тип: Офисная техника");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine("------------------------");
        }

        public string GetBrand() => Brand;
        public string GetModel() => Model;
        public decimal GetPrice() => Price;
        public int GetPowerConsumption() => PowerConsumption;

        public void SetPrice(decimal price) => Price = price;
        public void SetPowerConsumption(int pwr) => PowerConsumption = pwr;
    }

    // ======= ПРОИЗВОДНЫЙ КЛАСС: ПРИНТЕР =======

    public class Printer : OfficeEquipment
    {
        private string technology;
        private int printSpeed;
        private bool isColor;

        public const int MIN_PRINT_SPEED = 1;
        public const int MAX_PRINT_SPEED = 100;

        public Printer(string brand = "Unknown", string model = "Unknown",
                       decimal price = 0, int power = 0,
                       string technology = "Струйный",
                       int printSpeed = 20, bool isColor = false)
            : base(brand, model, price, power)
        {
            Technology = technology;
            PrintSpeed = printSpeed;
            IsColor = isColor;
        }

        public string Technology
        {
            get => technology;
            set => technology = string.IsNullOrWhiteSpace(value) ? "Струйный" : value;
        }

        public int PrintSpeed
        {
            get => printSpeed;
            set => printSpeed = (value >= MIN_PRINT_SPEED && value <= MAX_PRINT_SPEED)
                ? value : 20;
        }

        public bool IsColor
        {
            get => isColor;
            set => isColor = value;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: Принтер");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine($"Технология печати: {Technology}");
            Console.WriteLine($"Скорость печати: {PrintSpeed} стр/мин");
            Console.WriteLine($"Цветная печать: {(IsColor ? "Да" : "Нет")}");
            Console.WriteLine("------------------------");
        }
    }

    // ======= ПРОИЗВОДНЫЙ КЛАСС: ФАКС =======

    public class Fax : OfficeEquipment
    {
        private string connectionType;
        private bool hasAnswerMachine;
        private int memoryPages;

        public const int MIN_MEMORY_PAGES = 1;
        public const int MAX_MEMORY_PAGES = 500;

        public Fax(string brand = "Unknown", string model = "Unknown",
                   decimal price = 0, int power = 0,
                   string connectionType = "Аналоговый",
                   bool hasAnswerMachine = false,
                   int memoryPages = 50)
            : base(brand, model, price, power)
        {
            ConnectionType = connectionType;
            HasAnswerMachine = hasAnswerMachine;
            MemoryPages = memoryPages;
        }

        public string ConnectionType
        {
            get => connectionType;
            set => connectionType = string.IsNullOrWhiteSpace(value) ? "Аналоговый" : value;
        }

        public bool HasAnswerMachine
        {
            get => hasAnswerMachine;
            set => hasAnswerMachine = value;
        }

        public int MemoryPages
        {
            get => memoryPages;
            set => memoryPages =
                (value >= MIN_MEMORY_PAGES && value <= MAX_MEMORY_PAGES) ? value : 50;
        }

        public override void PrintInfo()
        {
            Console.WriteLine("Тип: Факс");
            Console.WriteLine($"Бренд: {Brand}");
            Console.WriteLine($"Модель: {Model}");
            Console.WriteLine($"Цена: {Price} руб.");
            Console.WriteLine($"Потребляемая мощность: {PowerConsumption} Вт");
            Console.WriteLine($"Тип соединения: {ConnectionType}");
            Console.WriteLine($"Автоответчик: {(HasAnswerMachine ? "Да" : "Нет")}");
            Console.WriteLine($"Память: {MemoryPages} страниц");
            Console.WriteLine("------------------------");
        }
    }

    // ======= ПРОГРАММА =======

    public class Program
    {
        public static void Main(string[] args)
        {
            List<OfficeEquipment> equipmentList = new List<OfficeEquipment>();
            bool running = true;

            while (running)
            {
                Console.WriteLine("\n1 - Добавить устройство");
                Console.WriteLine("2 - Показать список");
                Console.WriteLine("3 - Удалить устройство");
                Console.WriteLine("4 - Выход");

                int choice = ReadInt("Выбор: ", 1, 4);

                switch (choice)
                {
                    case 1: AddEquipment(equipmentList); break;
                    case 2: PrintList(equipmentList); break;
                    case 3: RemoveEquipment(equipmentList); break;
                    case 4: running = false; break;
                }
            }

            ClearList(equipmentList);
            Console.WriteLine("Работа завершена.");
        }

        // ======= Безопасный ввод =======

        private static string ReadString(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = Console.ReadLine();
                if (!string.IsNullOrWhiteSpace(s)) return s.Trim();
                Console.WriteLine("Введите значение.");
            }
        }

        private static int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (int.TryParse(Console.ReadLine(), out int n) && n >= min && n <= max)
                    return n;

                Console.WriteLine($"Введите число от {min} до {max}.");
            }
        }

        private static decimal ReadDecimal(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                Console.Write(prompt);
                if (decimal.TryParse(Console.ReadLine(), out decimal d) && d >= min && d <= max)
                    return d;

                Console.WriteLine($"Введите число от {min} до {max}.");
            }
        }

        private static bool ReadBool(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);
                string s = (Console.ReadLine() ?? "").Trim().ToLower();

                if (s == "да" || s == "1" || s == "y" || s == "true") return true;
                if (s == "нет" || s == "0" || s == "n" || s == "false") return false;

                Console.WriteLine("Введите Да/Нет или 1/0.");
            }
        }

        // ======= Работа со списком =======

        private static void AddEquipment(List<OfficeEquipment> list)
        {
            Console.WriteLine("\n1 - Базовое устройство");
            Console.WriteLine("2 - Принтер");
            Console.WriteLine("3 - Факс");

            int type = ReadInt("Тип: ", 1, 3);

            string brand = ReadString("Бренд: ");
            string model = ReadString("Модель: ");
            decimal price = ReadDecimal(
                $"Цена ({OfficeEquipment.MIN_PRICE}-{OfficeEquipment.MAX_PRICE}): ",
                OfficeEquipment.MIN_PRICE, OfficeEquipment.MAX_PRICE);

            int power = ReadInt(
                $"Мощность ({OfficeEquipment.MIN_POWER}-{OfficeEquipment.MAX_POWER}): ",
                OfficeEquipment.MIN_POWER, OfficeEquipment.MAX_POWER);

            OfficeEquipment device = null;

            if (type == 1)
            {
                device = new OfficeEquipment(brand, model, price, power);
            }
            else if (type == 2)
            {
                string tech = ReadString("Технология печати: ");
                int speed = ReadInt(
                    $"Скорость печати ({Printer.MIN_PRINT_SPEED}-{Printer.MAX_PRINT_SPEED}): ",
                    Printer.MIN_PRINT_SPEED, Printer.MAX_PRINT_SPEED);
                bool color = ReadBool("Цветная печать (Да/Нет): ");

                device = new Printer(brand, model, price, power, tech, speed, color);
            }
            else if (type == 3)
            {
                string conn = ReadString("Тип соединения: ");
                bool answer = ReadBool("Автоответчик (Да/Нет): ");
                int memory = ReadInt(
                    $"Память ({Fax.MIN_MEMORY_PAGES}-{Fax.MAX_MEMORY_PAGES}): ",
                    Fax.MIN_MEMORY_PAGES, Fax.MAX_MEMORY_PAGES);

                device = new Fax(brand, model, price, power, conn, answer, memory);
            }

            list.Add(device);
            Console.WriteLine("Добавлено.");
        }

        private static void PrintList(List<OfficeEquipment> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"\n#{i + 1}");
                list[i].PrintInfo();
            }

            Console.WriteLine("Полиморфизм работает.");
        }

        private static void RemoveEquipment(List<OfficeEquipment> list)
        {
            if (list.Count == 0)
            {
                Console.WriteLine("Список пуст.");
                return;
            }

            for (int i = 0; i < list.Count; i++)
                Console.WriteLine($"#{i + 1}: {list[i].GetBrand()} {list[i].GetModel()}");

            int n = ReadInt("Номер для удаления: ", 1, list.Count) - 1;
            list.RemoveAt(n);
            Console.WriteLine("Удалено.");
        }

        private static void ClearList(List<OfficeEquipment> list)
        {
            int count = list.Count;
            list.Clear();
            GC.Collect();
            GC.WaitForPendingFinalizers();
            Console.WriteLine($"Освобождено {count} объект(ов).");
        }
    }
}
