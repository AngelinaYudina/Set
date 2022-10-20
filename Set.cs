using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Class_Set
{
    internal class Set<T> : IEnumerable<T>
    {
        public List<T> Elements { get; private set; } = new List<T>();
        public int Length
        {
            get { return Elements.Count; }
        }

        #region Constructors
        // Standart constructor
        public Set(List<T> Elements)
        {
            this.Elements = UniqueFilter(Elements); ;
        }

        // Private constuctor for collections with unique elements only
        private Set(List<T> Elements, bool Flag)
        {
            this.Elements = Elements;
        }

        // Empty construstor
        public Set()
        {
        }
        #endregion

        #region Functions
        // Finds unique elements from the given collection (forming standard set)
        private List<T> UniqueFilter(List<T> InputCollection)
        {
            return (from el in InputCollection.Distinct()
                    select el).ToList();
        }

        // Adds element to the set
        public void Add (T element)
        {
            this.Elements.Add(element);
            this.Elements = UniqueFilter(this.Elements);
        }

        // Removes element from set
        public void Remove (T element)
        {
            if (this.Elements.Contains(element))
            {
                this.Elements.Remove(element);
            }
        }

        // Prepares a string to output a set in a standart format
        public String PrintElements()
        {
            this.Elements.Sort();
            return "{" + string.Join(", ", this.Elements) + "}";
        }

        // Returns the union of two given sets (set1 \cup set2)
        public static Set<T> Union(Set<T> Set1, Set<T> Set2)
        {
            List<T> SetUnion = (from el in Set1.Elements.Union(Set2.Elements)
                                select el).ToList();
            return new Set<T>(SetUnion, true);
        }

        // Returns the intersection of two given sets (set1 \cap set2)
        public static Set<T> Intersection(Set<T> Set1, Set<T> Set2)
        {
            List<T> SetIntersection = (from el in Set1.Elements.Intersect(Set2.Elements)
                                select el).ToList();
            return new Set<T>(SetIntersection, true);
        }

        // Returns the difference of two given sets (set1 \ set2)
        public static Set<T> Difference(Set<T> Set1, Set<T> Set2)
        {
            List<T> SetDifference = (from el in Set1.Elements.Except(Set2.Elements)
                                     select el).ToList();
            return new Set<T>(SetDifference, true);
        }

        // Checks if set1 is a subset of set2 or is it equal to set2 (returns true if set1 \subseteq set2 and false otherwise)
        public static bool SubsetOrEqual(Set<T> Set1, Set<T> Set2)
        {
            return Set1.Elements.All(el => Set2.Elements.Contains(el));
        }

        // Checks if set1 is  equal to set2 (returns true if set1 \subseteq set2 and false otherwise)
        public static bool Equal(Set<T> Set1, Set<T> Set2)
        {
            return Set1.Elements.All(el => Set2.Elements.Contains(el)) && Set2.Elements.All(el => Set1.Elements.Contains(el));
        }

        // Checks if set contains a given element
        public bool FindElement(T element)
        {
            return this.Elements.Contains(element);
        }
        #endregion

        #region Interface
        public IEnumerator<T> GetEnumerator()
        {
            throw new NotImplementedException();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
