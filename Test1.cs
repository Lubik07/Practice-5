using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OfficeEquipmentApp;

// ===================== ВСПОМОГАТЕЛЬНЫЙ КЛАСС =====================

internal static class TestHelpers
{
    public static string CaptureConsole(Action action)
    {
        var sw = new StringWriter();
        var oldOut = Console.Out;
        Console.SetOut(sw);

        try { action(); }
        finally { Console.SetOut(oldOut); }

        return sw.ToString();
    }
}

// ===================== TEST: БАЗОВЫЙ КЛАСС =====================

namespace OfficeEquipmentTests
{
    [TestClass]
    public class OfficeEquipment_Base_Tests
    {
        [TestMethod]
        public void DefaultConstructor_Assigns_DefaultValues()
        {
            var d = new OfficeEquipment();

            Assert.AreEqual("Unknown", d.Brand);
            Assert.AreEqual("Unknown", d.Model);
            Assert.AreEqual(0m, d.Price);
            Assert.AreEqual(0, d.PowerConsumption);
        }

        [TestMethod]
        public void Constructor_Assigns_Values()
        {
            var d = new OfficeEquipment("HP", "LaserJet", 15000m, 800);

            Assert.AreEqual("HP", d.Brand);
            Assert.AreEqual("LaserJet", d.Model);
            Assert.AreEqual(15000m, d.Price);
            Assert.AreEqual(800, d.PowerConsumption);
        }

        [TestMethod]
        public void InvalidBrandModel_BecomesUnknown()
        {
            var d = new OfficeEquipment("", "", 100, 10);

            Assert.AreEqual("Unknown", d.Brand);
            Assert.AreEqual("Unknown", d.Model);

            d.Brand = null;
            d.Model = null;

            Assert.AreEqual("Unknown", d.Brand);
            Assert.AreEqual("Unknown", d.Model);
        }

        [TestMethod]
        public void Price_OutOfRange_BecomesZero()
        {
            var d = new OfficeEquipment();

            d.Price = -5;
            Assert.AreEqual(0m, d.Price);

            d.Price = OfficeEquipment.MAX_PRICE + 1;
            Assert.AreEqual(0m, d.Price);
        }

        [TestMethod]
        public void Power_OutOfRange_BecomesZero()
        {
            var d = new OfficeEquipment();

            d.PowerConsumption = -10;
            Assert.AreEqual(0, d.PowerConsumption);

            d.PowerConsumption = OfficeEquipment.MAX_POWER + 10;
            Assert.AreEqual(0, d.PowerConsumption);
        }

        [TestMethod]
        public void Getters_ReturnValues()
        {
            var d = new OfficeEquipment("Canon", "C1", 999m, 500);

            Assert.AreEqual("Canon", d.GetBrand());
            Assert.AreEqual("C1", d.GetModel());
            Assert.AreEqual(999m, d.GetPrice());
            Assert.AreEqual(500, d.GetPowerConsumption());
        }

        [TestMethod]
        public void PrintInfo_Base_PrintsExpected()
        {
            var d = new OfficeEquipment("HP", "X1", 5000m, 300);

            var output = TestHelpers.CaptureConsole(() => d.PrintInfo());

            StringAssert.Contains(output, "Тип: Офисная техника");
            StringAssert.Contains(output, "Бренд: HP");
            StringAssert.Contains(output, "Модель: X1");
            StringAssert.Contains(output, "Цена: 5000");
            StringAssert.Contains(output, "Потребляемая мощность: 300");
        }
    }

    // ===================== TEST: PRINTER =====================

    [TestClass]
    public class Printer_Tests
    {
        [TestMethod]
        public void DefaultConstructor_Assigns_Defaults()
        {
            var p = new Printer();

            Assert.AreEqual("Unknown", p.Brand);
            Assert.AreEqual("Unknown", p.Model);
            Assert.AreEqual("Струйный", p.Technology);
            Assert.AreEqual(20, p.PrintSpeed);
            Assert.IsFalse(p.IsColor);
        }

        [TestMethod]
        public void Constructor_Assigns_Values()
        {
            var p = new Printer("Canon", "P200", 7000m, 150, "Лазерный", 35, true);

            Assert.AreEqual("Canon", p.Brand);
            Assert.AreEqual("P200", p.Model);
            Assert.AreEqual(7000m, p.Price);
            Assert.AreEqual(150, p.PowerConsumption);
            Assert.AreEqual("Лазерный", p.Technology);
            Assert.AreEqual(35, p.PrintSpeed);
            Assert.IsTrue(p.IsColor);
        }

        [TestMethod]
        public void PrintSpeed_OutOfRange_Defaults()
        {
            var p = new Printer();

            p.PrintSpeed = -10;
            Assert.AreEqual(20, p.PrintSpeed);

            p.PrintSpeed = 1000;
            Assert.AreEqual(20, p.PrintSpeed);
        }

        [TestMethod]
        public void PrintInfo_Printer_PrintsSpecificFields()
        {
            var p = new Printer("HP", "DeskJet", 5000m, 200, "Струйный", 22, true);

            var output = TestHelpers.CaptureConsole(() => p.PrintInfo());

            StringAssert.Contains(output, "Тип: Принтер");
            StringAssert.Contains(output, "Бренд: HP");
            StringAssert.Contains(output, "Модель: DeskJet");
            StringAssert.Contains(output, "Технология печати: Струйный");
            StringAssert.Contains(output, "Скорость печати: 22");
            StringAssert.Contains(output, "Цветная печать: Да");
        }
    }

    // ===================== TEST: FAX =====================

    [TestClass]
    public class Fax_Tests
    {
        [TestMethod]
        public void DefaultConstructor_Assigns_Defaults()
        {
            var f = new Fax();

            Assert.AreEqual("Unknown", f.Brand);
            Assert.AreEqual("Unknown", f.Model);
            Assert.AreEqual("Аналоговый", f.ConnectionType);
            Assert.IsFalse(f.HasAnswerMachine);
            Assert.AreEqual(50, f.MemoryPages);
        }

        [TestMethod]
        public void Constructor_Assigns_Values()
        {
            var f = new Fax("Panasonic", "F20", 3000m, 150, "Цифровой", true, 120);

            Assert.AreEqual("Panasonic", f.Brand);
            Assert.AreEqual("F20", f.Model);
            Assert.AreEqual(3000m, f.Price);
            Assert.AreEqual(150, f.PowerConsumption);
            Assert.AreEqual("Цифровой", f.ConnectionType);
            Assert.IsTrue(f.HasAnswerMachine);
            Assert.AreEqual(120, f.MemoryPages);
        }

        [TestMethod]
        public void Memory_OutOfRange_Defaults()
        {
            var f = new Fax();

            f.MemoryPages = -1;
            Assert.AreEqual(50, f.MemoryPages);

            f.MemoryPages = 1000;
            Assert.AreEqual(50, f.MemoryPages);
        }

        [TestMethod]
        public void PrintInfo_Fax_PrintsSpecificFields()
        {
            var f = new Fax("LG", "FX1", 4999m, 80, "Цифровой", true, 100);

            var output = TestHelpers.CaptureConsole(() => f.PrintInfo());

            StringAssert.Contains(output, "Тип: Факс");
            StringAssert.Contains(output, "Бренд: LG");
            StringAssert.Contains(output, "Модель: FX1");
            StringAssert.Contains(output, "Тип соединения: Цифровой");
            StringAssert.Contains(output, "Автоответчик: Да");
            StringAssert.Contains(output, "Память: 100");
        }
    }

    // ===================== TEST: ПОЛИМОРФИЗМ =====================

    [TestClass]
    public class Polymorphism_Tests
    {
        [TestMethod]
        public void Virtual_PrintInfo_WorksThrough_BaseReference()
        {
            var list = new OfficeEquipment[]
            {
                new OfficeEquipment("A", "Base", 1, 1),
                new Printer("P", "ModelP", 2, 2, "Струйный", 10, false),
                new Fax("F", "ModelF", 3, 3, "Аналоговый", true, 60)
            };

            var output = TestHelpers.CaptureConsole(() =>
            {
                foreach (var item in list)
                    item.PrintInfo();
            });

            StringAssert.Contains(output, "Тип: Офисная техника");
            StringAssert.Contains(output, "Тип: Принтер");
            StringAssert.Contains(output, "Тип: Факс");
        }
    }
}
