using System;
using System.Collections;
using System.Collections.Generic;
 

namespace Lab13
{
    class Program
    {
        static void Main(string[] args)
        {
            Random rnd = new Random();
            NewMyCollection  test1 = new NewMyCollection(rnd);
            NewMyCollection test2 = new NewMyCollection(rnd);
            Journal  testJ1 = new Journal();
            Journal testJ2 = new Journal();
            test1.CollectionCountChanged += new CollectionHandler(testJ1.CollectionCountChanged);
            test1.CollectionReferenceChanged += new CollectionHandler(testJ1.CollectionReferenceChanged);
            test1.CollectionCountChanged += new CollectionHandler(testJ2.CollectionCountChanged);
            test1.CollectionReferenceChanged += new CollectionHandler(testJ2.CollectionReferenceChanged); 
            test2.CollectionCountChanged += new CollectionHandler(testJ2.CollectionCountChanged); 
            test2.CollectionReferenceChanged += new CollectionHandler(testJ2.CollectionReferenceChanged);
            int howMuch = rnd.Next(1, 5);
            for (int i = 0; i < howMuch;i++) 
            {
                test1.Add(new Place(rnd));
                test1.Remove(rnd.Next(0, test1.Length - 1));
                test1[rnd.Next(0, test1.Length - 1)] = new Area(rnd);
                test2.Add(new Place(rnd));
                test2.Remove(rnd.Next(0, test1.Length - 1));
                test2[rnd.Next(0, test1.Length - 1)] = new Area(rnd); 
            }
            Console.WriteLine(testJ1);
            Console.WriteLine();
            Console.WriteLine("_________________________________________________________");
            Console.WriteLine();
            Console.WriteLine(testJ2);
        }
    }
    
    public class MyCollection<T> : List<T>, IEnumerator<Place>, IEnumerable<Place>
       where T : Place
    {
        

        public int Length
        {
            get
            {
                return list.Count;
            }
        }
        protected int numNow = 0;
        public Place Current 
        {
            get
            {
                if (numNow < Length)
                {
                    return list[numNow];
                }
                else
                {
                    numNow = 0;
                    return list[numNow];
                }
            }
            protected set { }
        }
        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }
        protected List<Place> list = new List<Place>();
        public virtual new Place this[int index]
        {
            get
            {
                try
                {
                    return list[index];
                }
                catch
                {
                    return list[0];
                }
            }
            set
            {
                try
                {
                    list[index] = value;
                }
                catch
                {
                    Console.WriteLine("error");
                }
            }
        }
        public MyCollection(params Place[] mas)
        {
            foreach (Place add in mas)
            {
                list.Add(add);
            }
            numNow = 0;
        }
        public MyCollection(Random Rnd)
        {
            int len = Rnd.Next(1, 100);
            for (int i = 0; i < len; i++)
            {
                switch (Rnd.Next(1, 5))
                {
                    case 1:
                        Place ad = new Place(Rnd);
                        list.Add(ad);
                        break;
                    case 2:
                        Area add = new Area(Rnd);
                        list.Add(add);
                        break;
                    case 3:
                        City addd = new City(Rnd);
                        list.Add(addd);
                        break;
                    case 4:
                        Megapolice adddd = new Megapolice(Rnd);
                        list.Add(adddd);
                        break;
                    case 5:
                        Address addddd = new Address(Rnd);
                        list.Add(addddd);
                        break;
                }
            }
            numNow = 0;
        }
        public MyCollection()
        {
            list = new List<Place>();
            Current = default;
            numNow = default;
        }
        public virtual int Add(Place some)
        {
            list.Add(some);
            return Length - 1;
        }
        public virtual int Add(Place[] some)
        {
            foreach (Place add in some)
            {
                list.Add(add);
            }
            numNow = 0;
            return Length - 1;
        }

        public virtual int AddDefaults()
        {
            list.Add(default);

            return Length - 1;
        }
        public virtual bool Remove(int i)
        {
            if (i >= 0 && i < Length)
            {
                list.Remove(list[i]);
                 
                return true;
            }
            else
            {
                 
                return false;
            }
        }
        public virtual bool Remove(Place k)
        {

            bool c = list.Remove(k);
            if (c)
            {
 
                return c;
            }
            else
            {
               
 
                return c;
            }
        }
        public new void Sort() 
        {
            list.Sort();
        }
        public void Sort(Place place)
        {
            list.Sort(place);
        }
        public void Delete()
        {

            list = new List<Place>();
        }

        public bool MoveNext()
        {
            if (numNow < Length - 1)
            {
                numNow++;
                return true;
            }
            else
            {
                Reset();
                return false;
                  
            }
        }

        public void Reset()
        {
            numNow = 0;
        }

        public void Dispose()
        {
            numNow = default;
            list = null;
        }

        IEnumerator<Place> IEnumerable<Place>.GetEnumerator()
        {
            foreach(Place ret in list)
            {
                yield return ret;
            }
        }
    }

    public class CollectionHandlerEventArgs:System.EventArgs
    {
        public string Name { get; set; }
        public string Event { get; set; }
        public Place Link { get; set; }
        public Place[] mLink { get; set; }
        public CollectionHandlerEventArgs(string name, string mes)
        {
            Name = name;
            Event = mes;
            Link = null;
            mLink = null;
        }
        public CollectionHandlerEventArgs(string name , string mes, Place sum)
        {
            Name = name;
            Event = mes;
            Link = sum;
            mLink = null;
 
        }
        public CollectionHandlerEventArgs(string name, string mes, Place[] sum)
        {
            Name = name;
            Event = mes;
            mLink = sum;
            Link = null;
        }
        public override string ToString()
        {
            return "Name: " + Name + " Event: " + Event + " Content: " + Link;
        }
    }
    public delegate void CollectionHandler(object list, CollectionHandlerEventArgs args);//
    public class NewMyCollection : MyCollection<Place>
    {
        string name;
        public string Name
        {
            get;
            set;    
        }
        public MyCollection<Place> places;
        public NewMyCollection(string nme, params Place[] mas)
        {
            foreach (Place add in mas)
            {
                list.Add(add);
            }
            numNow = 0;
            Name = nme;
        }
        public NewMyCollection(Random Rnd)
        {
            Name = Place.RandomWord();
            int len = Rnd.Next(1, 100);
            for (int i = 0; i < len; i++)
            {
                switch (Rnd.Next(1, 5))
                {
                    case 1:
                        Place ad = new Place(Rnd);
                        list.Add(ad);
                        break;
                    case 2:
                        Area add = new Area(Rnd);
                        list.Add(add);
                        break;
                    case 3:
                        City addd = new City(Rnd);
                        list.Add(addd);
                        break;
                    case 4:
                        Megapolice adddd = new Megapolice(Rnd);
                        list.Add(adddd);
                        break;
                    case 5:
                        Address addddd = new Address(Rnd);
                        list.Add(addddd);
                        break;
                }
            }
            numNow = 0;
        }
        public NewMyCollection()
        {
            list = new List<Place>();
            Current = default;
            numNow = default;
            Name = Place.RandomWord();
        }
        public event CollectionHandler CollectionCountChanged;
        public event CollectionHandler CollectionReferenceChanged;
        public virtual void OnCollectionCountChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionCountChanged != null)
                CollectionCountChanged(source, args);
        }
        public virtual void OnCollectionReferenceChanged(object source, CollectionHandlerEventArgs args)
        {
            if (CollectionReferenceChanged != null)
                CollectionReferenceChanged(source, args);
        }
        public override bool Remove(int position)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Delete", list[position]));
            return base.Remove(position);
        }
        public override int Add(Place p)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add element", p));
            return base.Add(p);
        }
        public override int AddDefaults()
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "AddDefaults"));
            return base.AddDefaults();
        }
        public override int Add(Place[] places)
        {
            OnCollectionCountChanged(this, new CollectionHandlerEventArgs(this.Name, "Add mass", places));
            return base.Add(places);
        }
        public override Place this[int index]
        {
            get
            {
                return base[index];
            }
            set
            {
                OnCollectionReferenceChanged(this, new CollectionHandlerEventArgs(this.Name, "Changed", list[index]));
                base[index] = value;
            }
        }

    }
    public class Journal
    {
        List<JournalEntry> jEntry = new List<JournalEntry>();
        public void CollectionCountChanged(object sourse, CollectionHandlerEventArgs add)
        {
            if (add.Link == null && add.mLink == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event));
            else if (add.Link == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event, add.mLink));
            else if (add.mLink == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event, add.Link));

        }
        public void CollectionReferenceChanged(object sourse, CollectionHandlerEventArgs add)
        {
            if (add.Link == null && add.mLink == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event));
            else if (add.Link == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event, add.mLink));
            else if (add.mLink == null)
                jEntry.Add(new JournalEntry(add.Name, add.Event, add.Link));
        }
        public override string ToString()
        {
            string content = "";
            foreach (JournalEntry add in jEntry)
            {
                content += add + "\n";
            }
            return content;
        }
    }
    public class JournalEntry  
    {
        public string Name { get; set; }
        public string Event { get; set; }
        public Place Link { get; set; }
        public Place[] mLink { get; set; }

         
        public JournalEntry(string name, string msg)
        {
            Name = name;
            Event = msg;
            Link = null;
            mLink = null;
        }
        public JournalEntry(string nm, string evnt,Place lnk)
        {
            Name = nm;
            Event = evnt;
            Link = lnk;
            mLink = null;
        }
        public JournalEntry(string name, string mes, Place[] sum)
        {
            Name = name;
            Event = mes;
            mLink = sum;
            Link = null;
        }
        public override string ToString()
        {
            if(mLink == null && Link ==null)
                return "Name: " + Name + " Event: " + Event + " Content: null ";
            else if(mLink == null )
                  return "Name: " + Name + " Event: " + Event + " Content: " + Link +" ";
            else
            {
                string Content = "";
                foreach(Place k in mLink)
                {
                    if(k is Address)
                    {
                        Address address = (Address)k.Clone();
                        Content += address;
                    }else if (k is Megapolice)
                    {
                        Megapolice megapolice = (Megapolice)k.Clone();
                        Content += megapolice;
                    }
                    else if (k is City)
                    {
                        City city = (City)k.Clone();
                        Content += city;
                    }
                    else if (k is Area)
                    {
                        Area city = (Area)k.Clone();
                        Content += city;
                    }
                    else if (k is Place)
                    {
                        Place place = (Place)k.Clone();
                        Content += place;
                    }
                }
                return "Name: " + Name + " Event: " + Event + " Content: " + Content + " ";
            }
        }   
    }
    
}

