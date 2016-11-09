using System;

public enum TStatus { EMPTY, FULL, DELETED }

public class HashTable<TKey, TValue>
{
	
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
		public override string ToString()
		{
			return string.Format("[Entry: Key={0}, Item={1}, Status={2}]", Key, Item, Status);
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
	public void Add(TKey key, TValue item)
	{
		//
		if (count < size)
		{
			if (ContainsKey(key))
			{
				for (int i = 0; i < count; i++)
				{
					if (table[i].Key.Equals(key))
					{
						table[i].Item = item;
						table[i].Status = TStatus.FULL;
					}
				}
			}
			else
			{
				int index;
				// linear probing
				if (scheme == 1)
				{
					index = Linear(0);
				}
				else
				{
					index = Quadratic(0);
				}
				table[index].Key = key;
				table[index].Item = item;
				table[index].Status = TStatus.FULL;
			}
			count++;
		}
	}

	////Remove: removes the item with key from the hash table and returns true if done; false otherwise
	public bool Remove(TKey key)
	{
		if (count > 0)
		{
			for (int i = 0; i < size; i++)
			{
				if (table[i].Key.Equals(key))
				{
					table[i].Status = TStatus.DELETED;
					count--;
					return true;
				}
			}
		}
		return false;
	}

	//ContainsKey: returns true if the key is used in the hash table; false otherwise
	public bool ContainsKey(TKey key)
	{
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				if (table[i].Key.Equals(key))
				{
					return true;
				}
			}
		}
		return false;
	}

	//Retrieve: returns the item with key if found; default(TValue) otherwise
	public TValue Retrieve(TKey key)
	{
		if (count > 0)
		{
			for (int i = 0; i < count; i++)
			{
				if (table[i].Key.Equals(key))
				{
					return table[i].Item;
				}
			}
		}
		return default(TValue);
	}
}

//uses linear probing to return the next availible (EMPTY/DELETED) index in the table
private int Linear(int i)
{
	if (count >= 0.72 * size)
		doubleTableSize();

	for (int index = i; index < size; index++)
	{
		if (table[index].Status == TStatus.EMPTY || table[index].Status == TStatus.DELETED)
			return index;
	}
	return -1;
}

//uses quadratic probing to return the next available (EMPTY/DELETED) index in the table
private int Quadratic(int i)
{
	if (count >= 0.50 * size)
		doubleQuadraticTableSize();

	for (int index = i; index < size; index++)
	{
		if (table[index].Status == TStatus.EMPTY || table[index].Status == TStatus.DELETED)
			return index;
	}
	return -1;
}

private static Coordinate getRandomCoordinate(Random rand)
{
	int lati = 0, longi = 0;

	lati = rand.Next(100);
	longi = rand.Next(100);
	return new Coordinate(longi, lati);
}

public class Coordinate  //used as key
{
	private int longitude;  //from 0...99
	private int latitude;  //from 0...99
	private int size;

    //
	public Coordinate(int longitude, int latitude, int size)
	{
		this.longitude = longitude;
		this.latitude = latitude;
		this.size = size;

	}

	public override bool Equals(object obj)
	{
		var coord = obj as Coordinate;
		return latitude == coord.latitude && longitude == coord.longitude;
	}

	public override int GetHashCode()
	{
		int itemSqrd = longitude * latitude;
		//find square of item
		int hashKey = itemSqrd % size;
		return latitude.GetHashCode() ^ longitude.GetHashCode();
	}
}

	public override string ToString()
	{
		return latitude + "," + longitude;
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
public static class Program
{
	//Main method
	static void Main(string[] args)
	{
		int linearCollisions = 0;
		int sumLinearCollisions = 0;
		int quadraticCollisions = 0;
		int sumQuadraticCollisions = 0;
		int avgLinearCollisions;
		int avgQuadraticCollisions;

		// linear probing table
		HashTable<Coordinate, City> linearHashTable = new HashTable<Coordinate, City>(13, 1);

		// quadratic probing table
		HashTable<Coordinate, City> quadraticHashTable = new HashTable<Coordinate, City>(13, 2);

		Coordinate coordinate;

		// insert 1000 cities
		for (int x = 0; x < 20; x++)
		{
			linearCollisions = 0;
			quadraticCollisions = 0;

			for (int i = 0; i < 1000; i++)
			{
				Random rand = new Random();
				do
				{
					coordinate = getRandomCoordinate(rand);
					linearCollisions++;
				} while (linearHashTable.ContainsKey(coordinate));

				linearHashTable.Add(coordinate, new City("Peterborough", 10845));
			}

			for (int i = 0; i < 1000; i++)
			{
				Random rand = new Random();
				do
				{
					coordinate = getRandomCoordinate(rand);
					quadraticCollisions++;
				} while (quadraticHashTable.ContainsKey(coordinate));

				quadraticHashTable.Add(coordinate, new City("Peterborough", 10845));
				Console.WriteLine(quadraticHashTable);
			}

			sumLinearCollisions = sumLinearCollisions + linearCollisions;
			sumQuadraticCollisions = sumQuadraticCollisions + quadraticCollisions;
		}

		// calculate average collisions
		avgLinearCollisions = sumLinearCollisions / 20;
		avgQuadraticCollisions = sumQuadraticCollisions / 20;

		// display average number of collisions
		Console.WriteLine("Number of average collsions in Linear Probing: " + avgLinearCollisions);
		Console.WriteLine("Number of average collsions in Quadratic Probing: " + avgQuadraticCollisions);
	}
}
		    