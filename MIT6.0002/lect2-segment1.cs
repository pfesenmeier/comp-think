using System;
using System.Collections.Generic;
using System.Linq;

namespace MIT6._0002
{
    public class Food
    {

        string Name;
        int Value;
        int Calories;

        public Food(string name, int value, int calories) 
        {
            Name = name;
            Value = value;
            Calories = calories;
        }


        public int GetValue() => Value;
        public int GetCost() => Calories;
        public float Density() => GetValue() / GetCost();

        public static int CompareByLowestCost(Food food1, Food food2)
        // When passed into Sort(), results in list with Cheapest Cost first.
        {
            return food1.GetCost().CompareTo(food2.GetCost());
        }

        public static int CompareByHighestValue(Food food1, Food food2)
        // When passed into Sort(), results in list with Highest Value first.
        {
            return food2.GetValue().CompareTo(food1.GetValue());
        }

        public static int CompareByDensity(Food food1, Food food2)
        // When passed into Sort(), result in a list with Highest Density first.
        {
            return food1.Density().CompareTo(food2.Density());
        }

        public override string ToString()
        {
            return Name + ": <" + GetValue().ToString() + ", " + GetCost().ToString() + '>';
        }

        public static List<Food> BuildMenu(List<string> names, List<int> values, List<int> calories)
        //assumes three lists of same length
        {
            var menu = new List<Food>();
            for (var i = 0; i < values.Count; i++)
            {
                menu.Add(new Food(names[i], values[i], calories[i]));
            }

            return menu;
        }
    }

    public class GreedyAlgorithms
    {
        public Tuple<List<Food>,float> Greedy(IEnumerable<Food> items, int maxCost, Comparison<Food> comparer)
        //assumes items and compare are of same type, maxCost >=0, that comparer maps items to numbers
        {
            var itemsCopy = new List<Food>(items);
            itemsCopy.Sort(comparer);
            var result = new List<Food>();
            float totalValue = 0.0f;
            float totalCost = 0.0f;
            for (var i = 0; i < itemsCopy.Count; i++)
            {
                if (totalCost + itemsCopy[i].GetCost() <= maxCost)
                {
                    result.Add(itemsCopy[i]);
                    totalCost += itemsCopy[i].GetCost();
                    totalValue += itemsCopy[i].GetValue();
                }
            }
            return new Tuple<List<Food>, float>(result, totalValue);
        }

        public void TestGreedy(IEnumerable<Food> items, int constraint, Comparison<Food> comparer)
        {
            Tuple<List<Food>, float> tuple = Greedy(items, constraint, comparer);
            // Composite string formation.
            Console.WriteLine("Total value of items takes = {0}", tuple.Item2.ToString());
            foreach (var item in tuple.Item1)
            {
                Console.WriteLine("    " + item.ToString());
            }
        }
        
        public void TestGreedys(IEnumerable<Food> foods, int maxUnits)
        {
            // String interpolation.
            Console.WriteLine($"Use greedy by value to allocate {maxUnits} calories.");
            TestGreedy(foods, maxUnits, Food.CompareByHighestValue);
            Console.WriteLine($"Use greedy by cost to allocate {maxUnits} calories.");
            TestGreedy(foods, maxUnits, Food.CompareByLowestCost);
            Console.WriteLine($"Use greedy by density to allocta {maxUnits} calories.");
            TestGreedy(foods, maxUnits, Food.CompareByDensity);
        }

        public void RunTest()
        {
            var names = new List<string>(9);
            names.Add("wine");
            names.Add("beer");
            names.Add("pizza");
            names.Add("burger");
            names.Add("fries");
            names.Add("cola");
            names.Add("apple");
            names.Add("donut");
            names.Add("cake");

            var values = new List<int>(9);
            values.Add(89);
            values.Add(90);
            values.Add(95);
            values.Add(100);
            values.Add(90);
            values.Add(79);
            values.Add(50);
            values.Add(10);
            values.Add(75);

            var calories = new List<int>(9);
            calories.Add(123);
            calories.Add(154);
            calories.Add(258);
            calories.Add(354);
            calories.Add(365);
            calories.Add(150);
            calories.Add(95);
            calories.Add(195);
            calories.Add(125);

            var foods = Food.BuildMenu(names, values, calories);
            TestGreedys(foods, 1000);
        }

    }

    public class SearchTreeForKnapsackProblem
    {
        public (int, List<Food>) MaxValue(List<Food> toConsider, int available)
        {
            (int, List<Food>) result;
            Food NextItem;
            if (toConsider.Count == 0 || available == 0)
                result = (0, new List<Food>());
            else if (toConsider[0].GetCost() > available)
                // Explore right branch only
                result = MaxValue(toConsider.Skip(1).ToList(), available);
            else
            {
                NextItem = toConsider[0];
                // Explore left branch
                ( int WithVal, List<Food> WithToTake ) = MaxValue(toConsider.Skip(1).ToList(), available - NextItem.GetCost());
                WithVal += NextItem.GetValue();
                // Explore right branch
                (int WithoutVal, List<Food> WithoutToTake) = MaxValue(toConsider.Skip(1).ToList(), available);
                // Choose better branch
                if (WithVal > WithoutVal)
                    result = (WithVal, WithoutToTake.Append(NextItem).ToList());
                else
                    result = (WithoutVal, WithoutToTake);
            }
            return result;
        }
        public void TestMaxVal(List<Food> foods, int maxUnits, bool printItems = true)
        {
            Console.WriteLine($"Use search tree to allocate {maxUnits} calories.");
            (int Value, List<Food> Taken) = MaxValue(foods, maxUnits);
            Console.WriteLine($"Total value of items taken = {Value}");
            Food Food;
            if (printItems)
                foreach (var item in Taken)
                {
                    Console.WriteLine($"    {item}");
                }
            if (foods.)
        }
    }


    public class WorkingWithTuples
    {
        public void CreateTupleWithHelperFunction()
        {
            var tuple = Tuple.Create("foo", "bar", "baz");
            var baz = tuple.Item3;
        }
    }

    public class ListAndArraySortingAndCopying
    {
        public void ArraySort(Food[] items, IComparer<Food> comparison)
        {
            Array.Sort(items, comparison);
            
        }

        public void ListSort(List<Food> items, int maxCost, IComparer<Food> comparison)
        {
            items.Sort(comparison);     
        }

        public Food[] CopyToArray(List<Food> foods)
        {
            Food[] array = new Food[foods.Count];
            foods.CopyTo(array);
            return array;
        }

        public Food[] CopyToArrayShorter(List<Food> foods) => foods.ToArray();

        public List<Food> CopyWithLinq(List<Food> foods) => foods.ToList();

        public List<Food> CopyWithConstructorOverload(List<Food> foods) => new List<Food>(foods);

    }
}
