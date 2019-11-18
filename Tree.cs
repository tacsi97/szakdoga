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
            SetTouchedToFalse();
            DeletePrefix();
        }
        private void SetPersonLevel(int level, Person person)
        {
            if (!peopleInLevels.ContainsKey(level))
                peopleInLevels.Add(level, new List<Person>());
            if (peopleInLevels[level] != null)
                peopleInLevels[level].Add(person);
        }
        public void ParentNodesToDictionary()
        {
            {
                //var rootGender = Root.Gender;
                //var branch = new Stack<Person>();
                //if (Root.Gender.Equals(Gender.Male))
                //{
                //    Root.Prefix = "2";
                //    if(Root.Pair != null)
                //        Root.Pair.Prefix = "1";
                //}
                //else
                //{
                //    Root.Prefix = "1";
                //    if (Root.Pair != null)
                //        Root.Pair.Prefix = "2";
                //}
                //branch.Push(Root);
                //var level = 0;
                //while(branch.Count != 0)
                //{
                //    Root.Touched = true;
                //    if (Root.Mother != null && Root.Mother.Touched != true)
                //    {
                //        Person.SetPrefixFromChildNode(Root.Mother, Root);
                //        Root = Root.Mother;
                //        branch.Push(Root);
                //        ++level;
                //        addToDictionary(level, Root);
                //    }
                //    else if (Root.Father != null && Root.Father.Touched != true)
                //    {
                //        Person.SetPrefixFromChildNode(Root.Father, Root);
                //        Root = Root.Father;
                //        branch.Push(Root);
                //        ++level;
                //        addToDictionary(level, Root);
                //    }
                //    else if(Root.Pair != null && Root.Pair.Touched != true)
                //    {
                //        branch.Pop();
                //        if(branch.Count != 0)
                //            Person.SetPrefixFromChildNode(Root.Pair, branch.Peek());
                //        Root = Root.Pair;
                //        addToDictionary(level, Root);
                //        branch.Push(Root);
                //    }
                //    else
                //    {
                //        if (level == 0)
                //        {
                //            addToDictionary(level, Root);
                //            if(Root.Pair != null && !peopleInLevels[level].Contains(Root.Pair))
                //            {
                //                peopleInLevels[level].RemoveAt(peopleInLevels[level].Count - 1);
                //                addToDictionary(level, Root.Pair);
                //            }
                //        }
                //        branch.Pop();
                //        --level;
                //        if (branch.Count != 0)
                //        {
                //            Root = branch.Peek();
                //        }
                //    }
                //}
                //if (Root.Pair != null && rootGender.Equals(Gender.Male))
                //    Root = Root.Pair;
            }
            var root = Root;
            var level = 0;
            var branches = new Stack<Person>();
            branches.Push(root);
            SetPersonLevel(level, root);
            root.IsTouched = true;
            if(root.Gender.Equals(Gender.Male))
            {
                root.Prefix = "1";
                if (root.Pair != null)
                    root.Pair.Prefix = "2";
            }
            else
            {
                root.Prefix = "2";
                if (root.Pair != null)
                    root.Pair.Prefix = "1";
            }
            while(branches.Count != 0)
            {
                if(root.Mother != null && !root.Mother.IsTouched)
                {
                    root.Mother.Prefix = root.Prefix + "2";
                    root = root.Mother;
                    level++;
                }
                else if(root.Father != null && !root.Father.IsTouched)
                {
                    root.Father.Prefix = root.Prefix + "1";
                    root = root.Mother;
                    level++;
                }
                else if(root.Pair != null && !root.Pair.IsTouched)
                {
                    branches.Pop();
                    root = root.Pair;
                    if (branches.Count != 0)
                        if (root.Gender.Equals(Gender.Male))
                            root.Prefix = branches.Peek().Prefix + "1";
                        else
                            root.Prefix = branches.Peek().Prefix + "2";
                }
                else
                {
                    if(branches.Count != 1)
                    {
                        branches.Pop();
                        level--;
                    }
                    root = branches.Peek();
                }
                if(!branches.Contains(root))
                {
                    branches.Push(root);
                    SetPersonLevel(level, root);
                    root.IsTouched = true;
                }
                if (
                    (root.Pair == null || root.Pair.IsTouched) &&
                    (root.Mother == null || root.Mother.IsTouched) &&
                    (root.Father == null || root.Father.IsTouched) &&
                    (branches.Count == 1)
                    )
                    branches.Pop();
            }
            SetTouchedToFalse();
        }
        private void SetTouchedToFalse()
        {
            foreach (var person in Program.people)
                person.IsTouched = false;
        }
        public void ChildNodes()
        {
            var root = Root;
            var level = -1;
            var branches = new Stack<Person>();
            var defaultCountOfTabulators = PeopleInLevels.Keys.Count;
            if (root.GetUnTouchedChildNode() != null)
                branches.Push(root);
            else if (root.Pair != null && root.Pair.GetUnTouchedChildNode() != null)
                branches.Push(root);
            while(branches.Count != 0)
            {
                if (root.GetUnTouchedChildNode() != null)
                {
                    root = root.GetUnTouchedChildNode();
                    level++;
                    Console.Write(WriteTabulators(defaultCountOfTabulators));
                    Console.Write(WriteTabulators(level));
                    if (root.Pair != null)
                        Console.WriteLine(root.ToString() + ", " + root.Pair.ToString());
                    else
                        Console.WriteLine(root.ToString());
                }
                else if(root.Pair != null && !root.Pair.IsTouched)
                {
                    branches.Pop();
                    root = root.Pair;
                }
                else
                {
                    if(branches.Count != 1)
                    {
                        branches.Pop();
                        level--;
                    }
                    root = branches.Peek();
                }
                if(!branches.Contains(root))
                {
                    branches.Push(root);
                    root.IsTouched = true;
                }
                if (root.GetUnTouchedChildNode() == null &&
                    (root.Pair == null || root.Pair.IsTouched) &&
                    (branches.Count == 1)
                    )
                    branches.Pop();
            }
            SetTouchedToFalse();
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
        public void DeletePrefix()
        {
            foreach (var person in Program.people)
                person.Prefix = "";
        }
        private string WriteTabulators(int count)
        {
            StringBuilder stringBuilder = new StringBuilder("");
            for(int i = 0; i < count; ++i)
            {
                stringBuilder.Append("\t");
            }
            return stringBuilder.ToString();
        }
    }
}
