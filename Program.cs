using BSUIR_Lab_7_Tasks1_2;
using BSUIR_Lab_8;

// Колеккции и классы подключены из проекта лабараторной работы 7, задания 1-2. BSUIR_Lab_7_Tasks1_2.dll
// interface IFlay and IWalk
Bird duck = new Bird("Donald", "duck");
Bird stork = new Bird("Busel", "stork");
Bird hawk = new Bird("Raptor", "hawk");
Bird pup = new Bird("Raptor", "pup");

// interface IFlay
Bee bee1 = new Bee("bee_1");
Bee bee2 = new Bee("bee_2");
Bee bee3 = new Bee("bee_3");

List<IFlay> list = new List<IFlay>() { duck, stork, hawk, bee1, pup, bee2, bee3 };

ListConsoleMenu<IFlay> menu  =  new ListConsoleMenu<IFlay>(list);
menu.Start();