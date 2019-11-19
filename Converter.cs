using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsaladFaTxt
{
    class Converter
    {
        private Dictionary<int, KeyValuePair<Person, string>> people = new Dictionary<int, KeyValuePair<Person, string>>();
        public Converter(string path)
        {
            people.Add(0, new KeyValuePair<Person, string>(null, ""));
            var lines = File.ReadAllLines(path);
            foreach(var line in lines)
            {
                //id;vezeték név;kereszt név;Születési dátum;halálozási dátum;nem;anya;apa;társak
                var parameters = line.Split(';');
                var id = int.Parse(parameters[0].Trim());
                var lastName = parameters[1].Trim();
                var firstName = parameters[2].Trim();
                var birthDate = DateTime.Parse(parameters[3].Trim());
                DateTime.TryParse(parameters[4].Trim(), out DateTime deathDate);
                var gender = (int.Parse(parameters[5].Trim()) == 1) ? Gender.Male : Gender.Female;
                var motherID = parameters[6].Trim();
                var fatherID = parameters[7].Trim();
                var pairIDs = parameters[8].Trim();
                people.Add(id, 
                    new KeyValuePair<Person, string>(
                        new Person(firstName, lastName, birthDate, deathDate, gender),
                        $"{motherID};{fatherID};{pairIDs}"
                        )
                    );
            }
        }
        public List<Person> CreateList()
        {
            var personModels = new List<Person>();
            foreach (var person in people)
            {
                if (person.Key != 0)
                {
                    var per = person.Value.Key;
                    int.TryParse(person.Value.Value.Split(';')[0], out int motherID);
                    int.TryParse(person.Value.Value.Split(';')[1], out int fatherID);
                    int.TryParse(person.Value.Value.Split(';')[2], out int pairID);
                    per.Mother = people[motherID].Key;
                    per.Father = people[fatherID].Key;
                    per.Pair = people[pairID].Key;
                    personModels.Add(per);
                }
            }
            return personModels;
        }
    }
}
