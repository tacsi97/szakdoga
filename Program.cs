using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaladFaTxt
{
    class Program
    {
        public static ObservableCollection<Person> people = new ObservableCollection<Person>();
        static void Main(string[] args)
        {
            Person _1 = new Person("Nagy", "Mária", DateTime.Parse("1944-02-18"), null, null, Gender.Female);
            Person _2 = new Person("Tóth", "Győző", DateTime.Parse("1942-05-14"), null, null, Gender.Male);
            Person _3 = new Person("Horváth", "Anna", DateTime.Parse("1941-04-18"), null, null, Gender.Female);
            Person _4 = new Person("Vidos", "Pál", DateTime.Parse("1940-01-26"), null, null, Gender.Male);
            Person _5 = new Person("Attila", "Mama1", DateTime.Parse("1944-02-18"), null, null, Gender.Female);
            Person _6 = new Person("Attila", "Papa1", DateTime.Parse("1944-02-18"), null, null, Gender.Male);
            Person _7 = new Person("Attila", "Mama2", DateTime.Parse("1944-02-18"), null, null, Gender.Female);
            Person _8 = new Person("Attila", "Papa2", DateTime.Parse("1944-02-18"), null, null, Gender.Male);
            Person A1 = new Person("Vidos", "Erzsébet", DateTime.Parse("2010-01-15"), _3, _4, Gender.Female);
            Person B1 = new Person("Tóth", "László", DateTime.Parse("2009-05-15"), _1, _2, Gender.Male);
            Person AB1 = new Person("Tóth", "Klaudia", DateTime.Parse("2017-05-17"), A1, B1, Gender.Female);
            Person AB2 = new Person("Tóth", "László", DateTime.Parse("2018-01-15"), A1, B1, Gender.Male);
            Person D1 = new Person("Fodor", "Hajnalka", DateTime.Parse("2018-01-15"), null, null, Gender.Female);
            Person AB3 = new Person("Tóth", "Péter", DateTime.Parse("2017-06-23"), A1, B1, Gender.Male);
            Person C2 = new Person("Horváth", "Katalin", DateTime.Parse("2017-01-01"), _5, _6, Gender.Female);
            Person C3 = new Person("Láng", "József", DateTime.Parse("2017-01-01"), _7, _8, Gender.Male);
            Person C1 = new Person("Láng", "Attila", DateTime.Parse("2019-01-01"), C2, C3, Gender.Male);
            Person ABC1 = new Person("Láng", "Máté", DateTime.Parse("2017-01-01"), AB1, C1, Gender.Male);
            _1.Pair = _2;
            _3.Pair = _4;
            _5.Pair = _6;
            _7.Pair = _8;
            A1.Pair = B1;
            AB1.Pair = C1;
            C1.Pair = AB1;
            C2.Pair = C3;
            AB2.Pair = D1;
            D1.Pair = AB2;
            Program.people.Add(A1);
            Program.people.Add(B1);
            Program.people.Add(AB1);
            Program.people.Add(AB2);
            Program.people.Add(AB3);
            Program.people.Add(D1);
            Program.people.Add(C1);
            Program.people.Add(C2);
            Program.people.Add(C3);
            Program.people.Add(ABC1);
            Program.people.Add(_1);
            Program.people.Add(_2);
            Program.people.Add(_3);
            Program.people.Add(_4);
            Program.people.Add(_5);
            Program.people.Add(_6);
            Program.people.Add(_7);
            Program.people.Add(_8);

            Tree tree = new Tree(A1);
            tree.ParentNodesToDictionary();
            tree.ChildNodes();
            tree.WriteTheTree();
            Console.WriteLine("----------------------------------------");
            Tree tree1 = new Tree(AB1);
            tree1.ParentNodesToDictionary();
            tree1.ChildNodes();
            tree1.WriteTheTree();
            Console.WriteLine("----------------------------------------");
            Tree tree2 = new Tree(ABC1);
            tree2.ParentNodesToDictionary();
            tree2.ChildNodes();
            tree2.WriteTheTree();
            Console.WriteLine("----------------------------------------");
            do
            {

            } while ()
            Console.ReadKey();
        }
    }
}
