using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaladFaTxt
{
    class Person
    {
        private List<Person> exPairs = new List<Person>();
        private bool touched = false;
        private Gender gender;
        private string prefix;
        private Person pair;
        private Person father;
        private Person mother;
        private List<Person> children;
        private string firstName;
        private string lastName;
        private DateTime birthDate;
        private int age;
        private DateTime deathDate;
        public string Prefix
        {
            get { return prefix; }
            set { prefix = value; }
        }
        public string FullName
        {
            get { return firstName + " " + lastName; }
        }
        public Person Father
        {
            get { return father; }
            set
            {
                father = value;
                if (father != null && !father.Children.Contains(this))
                    father.Children.Add(this);
            }
        }
        public Person Mother
        {
            get { return mother; }
            set {
                mother = value;
                if (mother != null && !mother.Children.Contains(this))
                    mother.Children.Add(this);
            }
        }
        public List<Person> Children
        {
            get { return children; }
            set { children = value; }
        }
        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = (DateTime) value; }
        }
        public DateTime DeathDate
        {
            get { return deathDate; }
            set { deathDate = (DateTime) value; }
        }
        public Gender Gender
        {
            get { return gender; }
            set { gender = value; }
        }
        public List<Person> ExPairs
        {
            get { return exPairs; }
            set { exPairs = value; }
        }
        public int Age
        {
            get
            {
                if (BirthDate > DateTime.Today.AddYears(-age))
                    return age--;
                else
                    return age;
            }
            set { age = value; }
        }
        public Person Pair
        {
            get => pair;
            set
            {
                pair = value;
                if (value != null)
                    value.pair = this;
            }
        }
        public bool IsTouched
        {
            get => touched;
            set => touched = value;
        }
        public Person GetUnTouchedChildNode()
        {
            if (Children != null)
                return this.Children.FirstOrDefault(
                    Node => Node.IsTouched == false);
            else return null;
        }
        public static void SetPrefixFromChildNode(Person parentNode, Person childNode)
        {
            if (parentNode.Gender.Equals(Gender.Male))
                parentNode.Prefix = childNode.Prefix + "2";
            else
                parentNode.Prefix = childNode.Prefix + "1";
        }
        public static void SetPrefixFromParentNode(Person childNode, Person parentNode)
        {
            if (childNode.Gender.Equals(Gender.Male))
                childNode.Prefix = parentNode.Prefix + "2";
            else
                childNode.Prefix = parentNode.Prefix + "1";
        }
        public Person(string firstName, string lastName, DateTime birthDate, Person mother, Person father, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Mother = mother;
            Children = new List<Person>();
            Father = father;
            Gender = gender;
        }
        public Person(string firstName, string lastName, DateTime birthDate, DateTime deathDate, Person mother, Person father, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Mother = mother;
            Children = new List<Person>();
            DeathDate = deathDate;
            Father = father;
            Gender = gender;
        }
        public Person(string firstName, string lastName, DateTime birthDate, DateTime deathDate, Gender gender)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
            Children = new List<Person>();
            DeathDate = deathDate;
            Gender = gender;
        }
        public override string ToString()
        {
            var basic = new StringBuilder();
            basic.Append($"{prefix} {lastName} {firstName}");
            if (birthDate != DateTime.Parse("0001-01-01"))
                basic.Append($" ({BirthDate.ToString("yyyy-MM-dd")}) ");
            if (exPairs.Count != 0)
            {
                basic.Append("(volt párok: ");
                exPairs.ForEach((item) => {
                    if (item != null)
                        if (item != exPairs[ExPairs.Count - 1])
                            basic.Append($"{item.FullName},");
                        else
                            basic.Append($"{item.FullName})");
                });
            }
            return basic.ToString();
        }

    }
}
