using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Lab13
{
    public class Place : ICloneable, IComparable, IComparable<Place>, IComparer<Place>
    {
        internal float latitude,
              longitude;
        public override string ToString()
        {
            string ret = "Координаты: ";
            if (latitude > 0)
            {
                ret += "+" + latitude + "°ш, ";
            }
            else
            {
                ret += latitude + "°ш, ";
            }
            if (latitude > 0)
            {
                ret += "+" + longitude + "°д";
            }
            else
            {
                ret += longitude + "°д";
            }
            return ret;

        }
        public float Latitude
        {
            get { return latitude; }
            set
            {
                if (value >= -90 && value <= 90)
                    latitude = value;
                else
                    Console.WriteLine("Широта может быть больше -180 и меньше 180");
            }
        }
        public float Longitude
        {
            get { return longitude; }
            set
            {
                if (value >= -180 && value <= 180)
                    longitude = value;
                else
                    Console.WriteLine("Долгота может быть больше -180 и меньше 180");
            }
        }
        public Place(float la, float lo)
        {

            latitude = la;
            longitude = lo;

        }
        public Place(Random rnd)
        {
            latitude = rnd.Next(-90, 91);
            longitude = rnd.Next(-180, 181);
        }
        public Place()
        {
            latitude = default;
            longitude = default;
        }
        virtual public int CompareTo(object ex)
        {
            Place pl1 = (Place)this;
            Place pl2 = (Place)ex;
            if (pl1.Latitude > pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 1;
                else
                    return -1;
            }
            else if (pl1.Latitude == pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (pl1.Longitude < pl2.Longitude)
                    return -1;
                else if (pl1.Longitude == pl2.Longitude)
                    return -1;
                else
                    return 1;
            }
        }
        public Place ShallowCopy() //поверхностное копирование
        {
            return (Place)this.MemberwiseClone();
        }
        virtual public object Clone()
        {
            return new Place(this.Latitude, this.Longitude);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Place p = (Place)obj;
            return (latitude == p.Latitude) && (longitude == p.Longitude);
        }
        public static string RandomWord()
        {

            // Создаем массив букв, которые мы будем использовать.
            char[] letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ".ToCharArray();
            char[] letters1 = "abcdefghijklmnopqrstuvwxyz".ToCharArray();
            // Создаем генератор случайных чисел.
            Random rand = new Random();
            int num_letters = rand.Next(0, 15);
            // Делаем слова.
            string word = "" + letters[rand.Next(0, letters.Length - 1)];
            for (int j = 1; j <= num_letters; j++)
            {
                // Выбор случайного числа от 0 до 25
                // для выбора буквы из массива букв.
                int letter_num = rand.Next(0, letters.Length - 1);

                // Добавить письмо.
                word += letters[letter_num];
            }
            return word;
        }
        public int CompareTo([AllowNull] Place other)
        {
            if (other == null)
                return 1;

            else
                return this.CompareTo(other);
        }
        public override int GetHashCode()
        {
            return Tuple.Create(latitude, longitude).GetHashCode();
        }
        public int Compare([AllowNull] Place x, [AllowNull] Place y)
        {
            if (y == null)
                return 1;
            else if (x == null)
                return -1;

            return x.CompareTo(y);
        }


    }
    public class Area : Place
    {
        public Place placeSaver;
        internal string nameOfArea;
        internal int countOfCity;
        internal float squearOfArea;
        internal string nameOfContinetn;
        string[] Continents = new string[] { "Австралия", "Азия", "Африка", "Европа", "Северная Америка", "Южная Америка" };

        public Place BasePlace
        {
            get
            {
                return new Place(latitude, longitude);//возвращает объект базового класса
            }

        }
        public string NameOfArea
        {
            get { return nameOfArea; }
            set { nameOfArea = value; }
        }
        public string NameOfContinetn
        {
            get { return nameOfContinetn; }
            set
            {
                for (int i = 0; i < Continents.Length; i++)
                {
                    if (Continents[i] == value)
                    {
                        nameOfContinetn = value;
                        break;
                    }
                    if (i + 1 == Continents.Length)
                    {
                        Console.WriteLine("Error");
                    }
                }
            }
        }
        public int CountOfCity
        {
            get { return countOfCity; }
            set { countOfCity = value; }
        }
        public float SquearOfArea
        {
            get { return squearOfArea; }
            set
            {
                if (value > 0)
                    squearOfArea = value;
                else
                    Console.WriteLine("Невозможно присвоить отрицательную площадь");
            }
        }

        public Area(Place pl, string nameInCountry, string nameInTheWorld, int co, float sq)
        {
            nameOfArea = nameInCountry;
            nameOfContinetn = nameInTheWorld;
            latitude = pl.latitude;
            longitude = pl.longitude;
            countOfCity = co;
            squearOfArea = sq;
            placeSaver = (Place)pl.Clone();
        }
        public Area(Random rnd)
        {

            nameOfArea = RandomWord();
            countOfCity = rnd.Next(1, 100);
            placeSaver = new Place(rnd);
            latitude = placeSaver.latitude;
            longitude = placeSaver.longitude;
            squearOfArea = rnd.Next(100, 1000000) + ((float)rnd.Next(0, 100)) / 100;

            nameOfContinetn = Continents[rnd.Next(0, 6)];

        }
        public Area()
        {
            nameOfArea = default;
            countOfCity = default;
            placeSaver = new Place();
            latitude = default;
            longitude = default;
            squearOfArea = default;
            nameOfContinetn = default;
        }
        public override int GetHashCode()
        {
            return Tuple.Create(nameOfArea, countOfCity, squearOfArea, nameOfContinetn).GetHashCode();
        }
        public override int CompareTo(object ex)
        {

            Place pl1 = (Place)this;
            Place pl2 = (Place)ex;
            if (pl1.Latitude > pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 1;
                else
                    return -1;
            }
            else if (pl1.Latitude == pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (pl1.Longitude < pl2.Longitude)
                    return -1;
                else if (pl1.Longitude == pl2.Longitude)
                    return -1;
                else
                    return 1;
            }

        }
        public new Area ShallowCopy() //поверхностное копирование
        {
            return (Area)this.MemberwiseClone();
        }
        public override object Clone()
        {
            Place clone = new Place(this.Latitude, this.Longitude);
            return new Area(clone, this.NameOfArea, this.NameOfContinetn, this.CountOfCity, this.SquearOfArea);
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Area p = (Area)obj;
            return (latitude == p.Latitude) && (longitude == p.Longitude);
        }
        public override string ToString()
        {
            return $"Область:{nameOfArea} с площадью {squearOfArea}км^2 содержит {countOfCity} городов и находится на континенте {nameOfContinetn}";
        }
    }
    public class City : Area
    {

        internal string nameOfCity;
        internal int countOfPeople;
        internal float squearOfCity;

        public new Place BasePlace
        {
            get
            {
                return new Place(latitude, longitude);//возвращает объект базового класса
            }

        }
        public Area BaseArea
        {
            get
            {
                return new Area(BasePlace, nameOfArea, nameOfContinetn, countOfCity, squearOfArea);//возвращает объект базового класса
            }

        }
        public float SquearOfCity
        {
            get
            {
                return squearOfCity;
            }
            set
            {

                if (value > 0)
                    squearOfCity = value;
                else
                    Console.WriteLine("Невозможно присвоить отрицательную площадь");
            }
        }
        public string NameOfCity
        {
            get { return nameOfCity; }
            set { nameOfCity = value; }
        }
        public int CountOfPeople
        {
            get { return countOfPeople; }
            set { countOfPeople = value; }
        }

        public City(Area ar, string name, int count, float squear)
        {
            latitude = ar.Latitude;
            longitude = ar.Longitude;
            nameOfArea = ar.NameOfArea;
            nameOfContinetn = ar.NameOfContinetn;
            countOfCity = ar.CountOfCity;
            squearOfArea = ar.SquearOfArea;
            nameOfCity = name;
            countOfPeople = count;
            squearOfCity = squear;
            placeSaver = (Place)ar.placeSaver.Clone();
        }
        public City(Random rnd)
        {
            Area ar = new Area(rnd);
            placeSaver = (Place)ar.placeSaver.Clone();
            latitude = placeSaver.Latitude;
            longitude = placeSaver.Longitude;
            nameOfArea = ar.NameOfArea;
            countOfCity = ar.CountOfCity;
            squearOfArea = ar.SquearOfArea;
            nameOfCity = RandomWord();
            countOfPeople = rnd.Next(0, 99999999);
            squearOfCity = rnd.Next(1, 1000) + ((float)rnd.Next(0, 100)) / 100;
        }
        public City()
        {
            Area ar = new Area();
            placeSaver = new Place();
            latitude = default;
            longitude = default;
            nameOfArea = default;
            countOfCity = default;
            squearOfArea = default;
            nameOfCity = default;
            countOfPeople = default;
            squearOfCity = default;
        }
        public override int GetHashCode()
        {
            return Tuple.Create(countOfPeople, squearOfCity, nameOfCity).GetHashCode();
        }
        public override string ToString()
        {
            return $"Город {nameOfCity} с площадью {squearOfCity}км^2 содержит {countOfPeople} людей";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            City p = (City)obj;
            return (latitude == p.Latitude) && (longitude == p.Longitude);
        }
        public override int CompareTo(object ex)
        {
            Place pl1 = (Place)this;
            Place pl2 = (Place)ex;
            if (pl1.Latitude > pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 1;
                else
                    return -1;
            }
            else if (pl1.Latitude == pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (pl1.Longitude < pl2.Longitude)
                    return -1;
                else if (pl1.Longitude == pl2.Longitude)
                    return -1;
                else
                    return 1;
            }

        }
        public new City ShallowCopy() //поверхностное копирование
        {
            return (City)this.MemberwiseClone();
        }
        public override object Clone()
        {
            Place clone = new Place(this.Latitude, this.Longitude);
            Area clone1 = new Area(clone, this.NameOfArea, this.NameOfContinetn, this.CountOfCity, this.SquearOfArea);
            return new City(clone1, this.NameOfCity, this.CountOfPeople, this.SquearOfCity);
        }

    }
    public class Megapolice : City
    {
        internal int countOfAglommeration;

        public override string ToString()
        {
            return $"Мегаполис {nameOfCity} с площадью {squearOfCity}км^2 содержит {countOfPeople} людей а также {countOfAglommeration}";
        }
        public new Place BasePlace
        {
            get
            {
                return new Place(latitude, longitude);//возвращает объект базового класса
            }

        }
        public new Area BaseArea
        {
            get
            {
                return new Area(BasePlace, nameOfArea, nameOfContinetn, countOfCity, squearOfArea);//возвращает объект базового класса
            }

        }
        public City BaseCity
        {
            get
            {
                return new City(BaseArea, nameOfCity, countOfPeople, squearOfCity);//возвращает объект базового класса
            }

        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Megapolice p = (Megapolice)obj;
            return (latitude == p.Latitude) && (longitude == p.Longitude);
        }

        public Megapolice(City city, int count)
        {
            latitude = city.latitude;
            longitude = city.longitude;
            nameOfArea = city.nameOfArea;
            countOfCity = city.countOfCity;
            nameOfContinetn = city.nameOfContinetn;
            squearOfArea = city.squearOfArea;
            countOfPeople = city.countOfPeople;
            nameOfCity = city.nameOfCity;
            squearOfCity = city.squearOfCity;
            countOfAglommeration = count;
            placeSaver = (Place)city.placeSaver.Clone();
        }
        public Megapolice(Random rnd)
        {
            City city = new City(rnd);
            placeSaver = (Place)city.placeSaver.Clone();
            latitude = placeSaver.latitude;

            longitude = placeSaver.longitude;
            nameOfArea = city.nameOfArea;
            countOfCity = city.countOfCity;
            squearOfArea = city.squearOfArea;
            countOfPeople = city.countOfPeople;
            nameOfCity = city.nameOfCity;
            squearOfCity = city.squearOfCity;
            countOfAglommeration = rnd.Next(1, 20);

        }
        public Megapolice()
        {
            City city = new City();
            placeSaver = new Place();
            latitude = default;
            longitude = default;
            nameOfArea = default;
            countOfCity = default;
            squearOfArea = default;
            countOfPeople = default;
            nameOfCity = default;
            squearOfCity = default;
            countOfAglommeration = default;
        }
        public override int GetHashCode()
        {
            return Tuple.Create(countOfAglommeration, nameOfCity).GetHashCode();
        }
        public int CountOfAglommeration
        {
            get { return countOfAglommeration; }
            set { countOfAglommeration = value; }
        }
        public override int CompareTo(object ex)
        {
            Place pl1 = (Place)this;
            Place pl2 = (Place)ex;
            if (pl1.Latitude > pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 1;
                else
                    return -1;
            }
            else if (pl1.Latitude == pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (pl1.Longitude < pl2.Longitude)
                    return -1;
                else if (pl1.Longitude == pl2.Longitude)
                    return -1;
                else
                    return 1;
            }
        }
        public new Megapolice ShallowCopy() //поверхностное копирование
        {
            return (Megapolice)this.MemberwiseClone();
        }
        public override object Clone()
        {
            Place clone = new Place(this.Latitude, this.Longitude);
            Area clone1 = new Area(clone, this.NameOfArea, this.NameOfContinetn, this.CountOfCity, this.SquearOfArea);
            City clone2 = new City(clone1, this.NameOfCity, this.CountOfPeople, this.SquearOfCity);
            return new Megapolice(clone2, this.CountOfAglommeration);
        }
    }
    public class Address : Megapolice
    {
        string streatName;
        int numberOfStreat;

        public new Place BasePlace
        {
            get
            {
                return new Place(latitude, longitude);//возвращает объект базового класса
            }

        }
        public new Area BaseArea
        {
            get
            {
                return new Area(BasePlace, nameOfArea, nameOfContinetn, countOfCity, squearOfArea);//возвращает объект базового класса
            }

        }
        public new City BaseCity
        {
            get
            {
                return new City(BaseArea, nameOfCity, countOfPeople, squearOfCity);//возвращает объект базового класса
            }

        }
        public Megapolice BaseMegapolice
        {
            get
            {
                return new Megapolice(BaseCity, countOfAglommeration);//возвращает объект базового класса
            }

        }

        public Address(Megapolice megapolice, string name, int num)
        {
            latitude = megapolice.Latitude;
            longitude = megapolice.Longitude;
            nameOfContinetn = megapolice.NameOfContinetn;
            nameOfArea = megapolice.NameOfArea;
            countOfCity = megapolice.CountOfCity;
            squearOfArea = megapolice.SquearOfArea;
            countOfPeople = megapolice.CountOfPeople;
            nameOfCity = megapolice.NameOfCity;
            squearOfCity = megapolice.SquearOfCity;
            countOfAglommeration = megapolice.CountOfAglommeration;
            placeSaver = (Place)megapolice.placeSaver.Clone();
            streatName = name;
            numberOfStreat = num;
        }
        public Address(City city, string name, int num)
        {
            placeSaver = (Place)city.placeSaver.Clone();
            latitude = city.Latitude;
            nameOfContinetn = city.NameOfContinetn;
            longitude = city.Longitude;
            nameOfArea = city.NameOfArea;
            countOfCity = city.CountOfCity;
            squearOfArea = city.SquearOfArea;
            countOfPeople = city.CountOfPeople;
            nameOfCity = city.NameOfCity;
            squearOfCity = city.SquearOfCity;
            countOfAglommeration = -1;
            streatName = name;
            numberOfStreat = num;
        }
        public Address(Random rnd)
        {

            City city = new City(rnd);
            placeSaver = (Place)city.placeSaver.Clone();
            latitude = placeSaver.Latitude;
            longitude = placeSaver.Longitude;
            nameOfArea = city.NameOfArea;
            countOfCity = city.CountOfCity;
            squearOfArea = city.SquearOfArea;
            countOfPeople = city.CountOfPeople;
            nameOfCity = city.NameOfCity;
            squearOfCity = city.SquearOfCity;
            countOfAglommeration = -1;
            streatName = RandomWord();
            numberOfStreat = rnd.Next(1, 500);
        }
        public Address()
        {

            City city = new City();
            placeSaver = new Place();
            latitude = default;
            longitude = default;
            nameOfArea = default;
            countOfCity = default;
            squearOfArea = default;
            countOfPeople = default;
            nameOfCity = default;
            squearOfCity = default;
            countOfAglommeration = default;
            streatName = default;
            numberOfStreat = default;
        }

        public string StreatName
        {
            get { return streatName; }
            set { streatName = value; }
        }
        public override string ToString()
        {
            return $"ул. {streatName} д. {numberOfStreat}";
        }
        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            Address p = (Address)obj;
            return (latitude == p.Latitude) && (longitude == p.Longitude);
        }
        public override int GetHashCode()
        {
            return Tuple.Create(streatName, numberOfStreat).GetHashCode();
        }
        public override int CompareTo(object ex)
        {
            Place pl1 = (Place)this;
            Place pl2 = (Place)ex;
            if (pl1.Latitude > pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 1;
                else
                    return -1;
            }
            else if (pl1.Latitude == pl2.Latitude)
            {
                if (pl1.Longitude > pl2.Longitude)
                    return 1;
                else if (pl1.Longitude == pl2.Longitude)
                    return 0;
                else
                    return -1;
            }
            else
            {
                if (pl1.Longitude < pl2.Longitude)
                    return -1;
                else if (pl1.Longitude == pl2.Longitude)
                    return -1;
                else
                    return 1;
            }
        }
        public new Address ShallowCopy() //поверхностное копирование
        {
            return (Address)this.MemberwiseClone();
        }
        public override object Clone()
        {
            Place clone = new Place(this.Latitude, this.Longitude);
            Area clone1 = new Area(clone, this.NameOfArea, this.NameOfContinetn, this.CountOfCity, this.SquearOfArea);
            City clone2 = new City(clone1, this.NameOfCity, this.CountOfPeople, this.SquearOfCity);
            Megapolice clone3 = new Megapolice(clone2, this.CountOfAglommeration);
            return new Address(clone3, this.streatName, this.numberOfStreat);
        }
    }
}
