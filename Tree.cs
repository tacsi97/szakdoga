using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaladFaTxt
{
    class Tree
    {
        private Person root;
        private Dictionary<int, List<Person>> peopleInLevels = new Dictionary<int, List<Person>>();
        public Dictionary<int, List<Person>> PeopleInLevels
        {
            get { return peopleInLevels; }
            set { peopleInLevels = value; }
        }
        public Person Root
        {
            get { return root; }
            set { root = value; }
        }
        public Tree(Person root)
        {
            Root = root;
        }
        private string NumberOfTabulators(int number)
        {
            StringBuilder tabs = new StringBuilder();
            for (int i = 0; i < number; ++i)
                tabs.Append('\t');
            return tabs.ToString();
        }
        public void WriteTheTree()
        {
            int offset = CountNegativeInPeopleMap();
            for (int j = PeopleInLevels.Count - offset - 1; j >= -offset; --j)
            {
                for (int i = 0; i < PeopleInLevels[j].Count; ++i)
                {
                    if (PeopleInLevels[j][i].Pair != null)
                    {
                        Console.Write(
                            NumberOfTabulators(PeopleInLevels.Count - j - (offset + 1))
                            );
                        Console.WriteLine(PeopleInLevels[j][i].ToString() + ", " + PeopleInLevels[j][i].Pair.ToString());
                        ++i;
                    }
                    else
                    {
                        Console.Write(
                            NumberOfTabulators(PeopleInLevels.Count - j - (offset + 1))
                            );
                        Console.WriteLine(PeopleInLevels[j][i].ToString());
                    }
                }
            }
        }
        private void addToDictionary(int level, Person person)
        {
            try
            {
                peopleInLevels[level].Add(person);
            }
            catch (Exception e)
            {
                peopleInLevels[level] = new List<Person>();
                peopleInLevels[level].Add(person);
            }
        }
        public void ParentNodesToDictionary()
        {
            var branch = new Stack<Person>();
            if (Root.Gender.Equals(Gender.Male))
            {
                Root.Prefix = "2";
                if(Root.Pair != null)
                    Root.Pair.Prefix = "1";
            }
            else
            {
                Root.Prefix = "1";
                if (Root.Pair != null)
                    Root.Pair.Prefix = "2";
            }
            branch.Push(Root);
            var level = 0;
            while(branch.Count != 0)
            {
                Root.Touched = true;
                if (Root.Mother != null && Root.Mother.Touched != true)
                {
                    Person.SetPrefixFromChildNode(Root.Mother, Root);
                    Root = Root.Mother;
                    branch.Push(Root);
                    ++level;
                    addToDictionary(level, Root);
                }
                else if (Root.Father != null && Root.Father.Touched != true)
                {
                    Person.SetPrefixFromChildNode(Root.Father, Root);
                    Root = Root.Father;
                    branch.Push(Root);
                    ++level;
                    addToDictionary(level, Root);
                }
                else if(Root.Pair != null && Root.Pair.Touched != true)
                {
                    addToDictionary(level, Root);
                    branch.Pop();
                    if(branch.Count != 0)
                        Person.SetPrefixFromChildNode(Root.Pair, branch.Peek());
                    Root = Root.Pair;
                    branch.Push(Root);
                }
                else
                {
                    if (level == 0)
                    {
                        addToDictionary(level, Root);
                    }
                    branch.Pop();
                    --level;
                    if (branch.Count != 0)
                    {
                        Root = branch.Peek();
                    }
                }
            }
            if (Root.Pair != null)
                Root = Root.Pair;
            SetTouchedToFalse();
        }
        private void SetTouchedToFalse()
        {
            foreach (var person in Program.people)
                person.Touched = false;
        }
        public void ChildNodes()
        {
            int level = 0;
            Person parent = Root.Mother;
            Root.Mother = null;
            while (Root.GetUnTouchedChildNode() != null)
            {
                if (Root.Touched == false)
                {
                    if (level != 0)
                    {
                        addToDictionary(level, Root);
                        addToDictionary(level, Root.Pair);
                    }
                    Root.Touched = true;
                }
                Person helper = Root.GetUnTouchedChildNode();
                Person.SetPrefixFromParentNode(helper, Root);
                Root = helper;
                --level;
                if(Root.GetUnTouchedChildNode() == null)
                {
                    Root.Touched = true;
                    if (Root.Pair != null)
                    {
                        addToDictionary(level, Root);
                        addToDictionary(level, Root.Pair);
                    }
                    else
                        addToDictionary(level, Root);
                    if (Root.Mother.GetUnTouchedChildNode() != null)
                    {
                        Root = Root.Mother;
                        ++level;
                    }
                    else
                        while (Root.GetUnTouchedChildNode() == null
                            && Root.Mother != null)
                        {
                            Root = Root.Mother;
                            ++level;
                        }
                }
            }
            Root.Mother = parent;
        }
        public int CountNegativeInPeopleMap()
        {
            int sum = 0;
            foreach(var pairs in peopleInLevels)
            {
                if (pairs.Key < 0)
                    ++sum;
            }
            return sum;
        }
    }
}
