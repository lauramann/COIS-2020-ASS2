﻿using System;
public static class Ass2
{
	public static void Main()
	{
		
	}

	public enum TStatus { EMPTY, FULL, DELETED }

	public class HashTable<TKey, TValue>
	{
		private class Entry  //stores the key, item, and status of a particular entry of the hash table
		{
			public TKey Key { get; set; }
			public TValue Item { get; set; }
			public TStatus Status { get; set; }
		}

		private Entry[] table;  //array of entries
		public static int size { get; set;}  //capcity of the hash table
		private int count;  //number of entries in the hash table
		private int scheme;  //1 for linear, 2 for quadratic


		//HashTable: creates an empty hash table of size using the resolution scheme 1 for linear and 2 for quadratic
		public HashTable(int size, int scheme)
		{
			if (scheme == 1)
				table = new Entry[size * 2];
			else if (scheme == 2)
				table = new Entry[nextPrime(size)];
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

		//Add: adds an item with key to hash table (keys must be unique)
		public void Add(TKey key, TValue item)
		{
			table[key] = item;
			Entry.Status = TStatus.FULL;

		}

		//Remove: removes the item with key from the hash table and returns true if done; false otherwise
		public bool Remove(TKey key)
		{
			
		}

		//ContainsKey: returns true if the key is used in the hash table; false otherwise
		public bool ContainsKey(TKey key)
		{
		}

		//Retrieve: returns the itme with key if dound; default(TValue) otherwise
		public TValue Retrieve(TKey key)
		{
		}

		public int Linear(int i)  //uses linear probing to return the next availible (EMPTY/DELETED) index in the table
		{
		}

		private int Quadratic(int i)  //uses quadratic probing to return the next available (EMPTY/DELETED) index in the table
		{
		}
	}

	public class Coordinate  //used as key
	{
		private int longitude=18;  //from 0...99
		private int latitude=19;  //from 0...99

		public Coordinate(int longitude, int latitude)
		{
			
		}

		public override bool Equals(object obj)  //from the Object class
		{
			return base.Equals(obj);
		}

		public override int GetHashCode()  //from the Object class
		{
			int itemSqrd;
			itemSqrd = longitude * latitude;
			//find square of item
			int hashKey = itemSqrd % HashTable.size;
			return hashKey;
		}

	}

	public class City
	{
		private string name;
		private int population;

		public City(string name, int population)
		{
		}
	}
}