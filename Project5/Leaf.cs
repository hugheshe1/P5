//	Project:		Project 5 - BTree
//	File Name:		Leaf.cs
//	Description:	
//	Course:			CSCI 2210-001 - Data Structures
//	Authors:		Reed Jackson, reedejackson@gmail.com, jacksonre@etsu.edu
//                  Haley Hughes, hugheshe1@etsu.edu
//                  John Burdette, burdettj@etsu.edu
//	Created:		11/23/2016
//	Copyright:		Reed Jackson, Haley Hughes, John Burdette, 2016

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project5
{
    /// <summary>
    /// Class for Leaves on the B Tree
    /// </summary>
    class Leaf : Node
    {
        #region Constructors
        /// <summary>
        /// Default constructor
        /// </summary>
        public Leaf() { }

        /// <summary>
        /// Parameterized constructor
        /// </summary>
        /// <param name="nodeSize">The number of elements to be contained in a leaf</param>
        public Leaf(int nodeSize)
        {
            NodeSize = nodeSize;
        }

        /// <summary>
        /// Copy Constructor
        /// </summary>
        /// <param name="CopyLeaf">The leaf to be copied</param>
        public Leaf(Leaf CopyLeaf)
        {
            NodeSize = CopyLeaf.NodeSize;
            Items = new List<int>(CopyLeaf.Items);
        }

        #endregion

        #region Insertion Methods
        /// <summary>
        /// Method for inserting a new value into a leaf
        /// </summary>
        /// <param name="value">The value to be added</param>
        /// <returns>enum telling whether the insertion was a success</returns>
        public INSERT Insert(int value)
        {
            if (Items.Count == 0)
            {
                Items.Add(value);
                return INSERT.SUCCESS;
            }
            else
            {
                //Initial Values
                int i;
                Items.Add(value);

                //Position temp to the smallest place it 
                //can go
                for (i = Items.Count - 1; (i > 0 && value <= Items[i - 1]); i--)
                {
                    //This prevents duplicates from being added
                    if (Items[i - 1] == value)
                    {
                        //Undo changes to Items List
                        for (int j = i; j < Items.Count - 2; j++)
                        {
                            Items[i + 1] = Items[i + 2];
                        }

                        //Remove top value
                        Items.RemoveAt(Items.Count - 1);
                        return INSERT.DUPLICATE;
                    }
                    else
                    {
                        Items[i] = Items[i - 1];
                    }
                }


                //Insert the value to the selected position
                Items[i] = value;

                //Set return message
                if (Items.Count > NodeSize)
                {
                    return INSERT.NEEDSPLIT;
                }
                else
                {
                    return INSERT.SUCCESS;
                }
            }
        }

        #endregion

        #region ToString
        /// <summary>
        /// ToString override for the leaf
        /// </summary>
        /// <returns>the string containing relevant information</returns>
        public override string ToString ( )
        {
            string result = "\n\nNode type: Leaf";
            result += base.ToString();
            foreach (int i in Items)
                result += (i + " ");

            return result;
        }
        #endregion
    }
}
