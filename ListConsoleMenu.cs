using BSUIR_Lab_7_Tasks1_2;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace BSUIR_Lab_8
{
    internal class ListConsoleMenu<T> where T : IFlay
    {
        public List<T> list;
        public ListConsoleMenu() { list = new List<T>(0); }
        public ListConsoleMenu(List<T> list)
        {
            this.list = list;
        }

        private string textMenu = "1 – просмотр коллекции\n2 – добавление элемента\n3 – добавление элемента по указанному индексу\n" +
            "4 – нахождение элемента с начала коллекции\n5 – нахождение элемента с конца коллекции\n6 – удаление элемента по индексу\n" +
            "7 – удаление элемента по значению\n8 – реверс коллекции\n9 – сортировка\n" +
            "10 – выполнение методов всех объектов, поддерживающих Interface2\n0 – выход";

        public override string ToString()
        {
            return $"Console menu collections list {list.ToString()}. Numbers objects {list.Count()}";
        }
        public void PrintStartMenu() => Console.WriteLine(textMenu);
        private int inputNumber()
        {
            Console.WriteLine("Введите номер операции: ");
            int number = 999;
            bool flag = int.TryParse(Console.ReadLine(), out number);
            if (!flag) number = 999999;
            return number;
        }
        private int inputIndex()
        {
            int index = 0;
            Console.WriteLine($"Введите индекс от 0 до {list.Count - 1}: ");
            if (int.TryParse(Console.ReadLine(), out index) && index <= list.Count - 1) return index;
            else throw new ArgumentOutOfRangeException();
        }
        private string inputName()
        {
            Console.WriteLine("Введите имя: ");
            string? name = Console.ReadLine();
            return (name??="No name");
        }        
        private object createObject()
        {
            // Создает новый объект 
            Console.WriteLine("Какой тип вы хотите добавить, птица или пчела?");
            string? response = Console.ReadLine();
            if (response != null && response == "пчела")
            {
                Console.WriteLine("Введите имя пчелы: ");
                string? name = Console.ReadLine();
                Bee bee = new Bee(name??="No name");
                return bee;
            }
            if (response != null && response == "птица")
            {
                Console.WriteLine("Введите имя птицы: ");
                string? name = Console.ReadLine();
                Console.WriteLine("Введите вид птицы: ");
                string? species = Console.ReadLine();
                Bird bird = new Bird(name ??= "No name", species ??= "unknown");
                return bird;
            }
            else throw new ArgumentOutOfRangeException();
        }

        public void Start()
        {
            bool flagON = true;
            while (flagON)
            {
                PrintStartMenu();
                int number = inputNumber();
                
                switch(number)
                {
                    case 0:
                        flagON = false; break;
                    case 1:
                        // Отображение коллекции
                        foreach (var item in list) Console.WriteLine(item); break;
                    case 2:
                        // Добавление нового объекта
                        try
                        {
                            object newObj = createObject();
                            if (newObj is T obj)
                            {
                                list.Add(obj);
                                Console.WriteLine($"Добавлен объект: {obj}");
                            }               
                        }
                        catch(ArgumentOutOfRangeException) { Console.WriteLine("Указан не поддерживаймый тип добавляемого объекта");}
                        break;
                    case 3:
                        // Вставка нового объекта по индексу
                        try
                        {
                            int index = inputIndex();
                            object newObj = createObject();
                            if (newObj is T obj)
                            {
                                list.Insert(index, obj);
                                Console.WriteLine($"По индексу {index} добавлен объект: {obj}");
                            }
                        }
                        catch (ArgumentOutOfRangeException) { Console.WriteLine("Неверно указан индекс или тип добавляемого объекта"); };
                        break;
                    case 4:
                        // Поиск объекта по имени
                        string name = inputName();
                        T? searchObj = list.Find((item) => ((item as Bird)?.name == name) || (item as Bee)?.name == name);
                        if (searchObj is null) Console.WriteLine($"Объект с именем '{name}' не найден.");
                        else { Console.WriteLine(searchObj);}
                        break;
                    case 5:
                        string name1 = inputName();
                        T? searchObj2 = list.FindLast((item) => ((item as Bird)?.name == name1) || (item as Bee)?.name == name1);
                        if (searchObj2 is null) Console.WriteLine($"Объект с именем '{name1}' не найден.");
                        else { Console.WriteLine(searchObj2); }
                        break;
                    case 6:
                        // Удаление по индексу
                        try
                        {
                            int index = inputIndex();
                            list.RemoveAt(index);
                            Console.WriteLine($"Объект с индексом {index} удален!");
                        }
                        catch { Console.WriteLine("Такого индекса не существует"); };
                        break;
                    case 7:
                        // Удаление по имени
                        string nameRemove = inputName();
                        List<T> TempRemoverObjs = list.FindAll((item) => ((item as Bird)?.name == nameRemove) || (item as Bee)?.name == nameRemove);

                        if (TempRemoverObjs.Count > 0)
                        {
                            foreach (var item in TempRemoverObjs)
                            {
                                list.Remove(item);
                                Console.WriteLine($"Удален объект: {item}");
                            }
                        }
                        else { Console.WriteLine($"Объекты с именем '{nameRemove}' не найдены."); }
                        break;
                    case 8:
                        // Реверс колекции
                        list.Reverse();
                        Console.WriteLine("Коллекция отражена!");
                        break;
                    case 9:
                        // Сортировка если поддерживается
                        try
                        {
                            list.Sort();
                            Console.WriteLine("Коллекция отсортирована!");
                        }
                        catch(InvalidOperationException)
                        {
                            Console.WriteLine("Коллекция не отсортирована. Не реализована метод CompareTo.");
                        }
                        break;
                    case 10:
                        // Вызов метода другого интерфейса.
                        foreach (var item in list)
                        {
                            (item as IWalk)?.AddStep(5);

                        }
                        break;
                    default:
                        Console.WriteLine("Неверный ввод. Введите число от 0 до 10: ");
                        break;
                }
            }
        }
    }
}
