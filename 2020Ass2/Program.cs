using System;


public enum TStatus { EMPTY, FULL, DELETED }

public class HashTable<TKey, TValue>
{
	//	public override string ToString()
	//{

	//}
	private class Entry  //stores the key, item, and status of a particular entry of the hash table
	{
		public TKey Key { get; set; }
		public TValue Item { get; set; }
		public TStatus Status { get; set; }
		public Entry(TKey key, TValue item, TStatus status)
		{
			Key = key;
			Item = item;
			Status = status;
		}

	}

	private Entry[] table;  //array of entries
	private int size;  //capcity of the hash table
	private int count;  //number of entries in the hash table
	private int scheme;  //1 for linear, 2 for quadratic


	//HashTable: creates an empty hash table of size using the resolution scheme 1 for linear and 2 for quadratic
	public HashTable(int size, int scheme)
	{
		//uses linear resolution scheme and double table size
		if (scheme == 1)
			size = size * 2;
		//uses quardratic resolution scheme and doubles table size to next prime number
		else if (scheme == 2)
			size = nextPrime(size);

		//creates new array of type Entry
		table = new Entry[size];

		//tests to see if size was given to new array
		Console.WriteLine(table.Length);

		count = -1;
	}


	//method to find next prime number when using quadratic probing
	public static int nextPrime(int number)
	{
		//double number
		number *= 2;

		while (true)
		{
			bool isPrime = true;
			//increment the number by 1 each time
			number = number + 1;

			//find square root of number
			int root = (int)Math.Sqrt(number);

			//start at 2 and increment by 1 until it gets to the square root of the number
			for (int i = 2; i <= root; i++)
			{
				//if it is divisible by i, it is not a prime number, break and add 1
				if (number % i == 0)
				{
					isPrime = false;
					break;
				}
			}
			//return prime number
			if (isPrime)
				return number;
		}
	}


	//Add: adds an item with key to hash table
	public void Add(TKey key, TValue item, int pos)
	{
		if (count + 1 < size)
			table[++count] = new Entry(key, item, TStatus.FULL);
	}

	//Remove: removes the item with key from the hash table and returns true if done; false otherwise
	public bool Remove(TKey key)
	{
		if (table[key].Item = null)
			return false;
		else if (table[key].Item != null)
		{
			table.RemoveAt(key);
			table[key].Status = TStatus.DELETED;
			return true;
		}
	}

	//ContainsKey: returns true if the key is used in the hash table; false otherwise
	public bool ContainsKey(TKey key)
	{
		if (table[key].Key = null)
			return false;
		else if (table[key].Key != null)
			return true;
	}

	//Retrieve: returns the item with key if found; default(TValue) otherwise
	public TValue Retrieve(TKey key)
	{
		int retrivedKey = table.GetValue(table[key].Item);
		return retrivedKey;
	}

	public int Linear(int i)  //uses linear probing to return the next availible (EMPTY/DELETED) index in the table
	{
		//for (int j = i; i < size; i++)
		return 0;
	}

	private int Quadratic(int i)  //uses quadratic probing to return the next available (EMPTY/DELETED) index in the table
	{

	}
	public int Size()
	{
		return size;
	}
}

public class Coordinate  //used as key
{
	private int longitude = 18;  //from 0...99
	private int latitude = 19;  //from 0...99
	private int size;

	public Coordinate(int longitude, int latitude, int size)
	{
		this.longitude = longitude;
		this.latitude = latitude;
		this.size = size;
	}

	public override bool Equals(object obj)  //from the Object class
	{
		Coordinate other = obj as Coordinate;
		if (obj == null)
			return false;
		else
			return longitude.Equals(other.longitude) && latitude.Equals(other.latitude);
	}

	public override int GetHashCode()  //from the Object class
	{
		int itemSqrd = longitude * latitude;
		//find square of item
		int hashKey = itemSqrd % size;
		return hashKey;
	}

}

public class City
{
	private string name;
	private int population;

	public City(string name, int population)
	{
		this.name = name;
		this.population = population;
	}
}
public static class Test
{
	public static void Main()
	{
		//creates new instance of the HashTable class with paramters (size & scheme)
		HashTable<Coordinate, City> test = new HashTable<Coordinate, City>(7, 1);
		Coordinate coordinate = new Coordinate(5, 2, test.Size());
		test.Add(coordinate, new City("Boston", 90), coordinate.GetHashCode());
	}
}