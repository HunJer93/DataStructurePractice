using System;
using System.Collections.Generic;
using System.IO;

namespace HuntonProgramTwo
{
    class PatientData
    {
        private LinkedList<Patient>[] list;

        /// <summary>
        /// Constructor.  Instantiates the array based on the size passed to the constructor.
        /// </summary>
        /// <param name="size">The size of the array.</param>
        public PatientData(int size)
        {
            list = new LinkedList<Patient>[size];
        }

        /// <summary>
        /// Loads patients into the data structure from the filename passed as a parameter.
        /// </summary>
        /// <param name="filename">Path to the file where the patient data is kept.  The format is: <para>first,last,procedureCost</para></param>
        public void LoadPatients(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Open);
            StreamReader input = new StreamReader(fs);

            while (!input.EndOfStream)
            {
                string line = input.ReadLine();
                string[] fields = line.Split(",".ToCharArray());
                // TODO create the new patient
                Patient patient = new Patient(fields[0], fields[1], double.Parse(fields[2]));
                // TODO add the patient to the list
                AddPatient(patient);
            }//end while loop 
        }//end LoadPatients

        /// <summary>
        /// Adds a patient to the data structure.
        /// </summary>
        /// <param name="patient">The patient to be added.</param>
        public void AddPatient(Patient patient)
        {
            // TODO add code needed to add a patient

            // Find the correct index
            // Add the patient to the linked list
            // Create a new list if there isn't one at this index

            //set the index by getting the mod of the id and the list length
            int index = patient.Id % list.Length;

            //check if the cell with the patient id index is null in the array. If null, create a new linked list pointing to that array cell
            if (list[index] == null)
            {
                //create a new linked list for the indexed value that will hold linked lists
                LinkedList<Patient> idList = new LinkedList<Patient>();

                //set the idList linked list to the array @ the patient id
                list[index] = idList;

            }//end new linked list created for the indexed value in the array

            //add the patient obj to the end of the linked list at the index of the patient.
            list[index].AddLast(patient);
        }//end AddPatient

        public Patient FindPatientByName(string first, string last)
        {
            Patient pat = null;
            // TODO
            // create a patient using the names; the cost can be zero since it isn't used in the comparison
            // use the patient you created to get the id, slot in the array and finally the data in the linked list

            //create a "dummy" patient obj using the first and last name (cost isn't needed, so it is zero)
            Patient dummyPat = new Patient(first, last, 0);

            //get the index of the dummyPat so we can get the correct list to search
            int index = dummyPat.Id % list.Length;

            //set the LinkedListNode to be the first node in the LinkedList @ the index value
            LinkedListNode<Patient> node = list[index].First;

            //search as long as the node is not null
            while(node != null)
            {               
                //if the CompareTo is 0, node is found
                if(dummyPat.CompareTo(node.Value) == 0)
                {
                    //assign pat value and return
                    pat = node.Value;
                    return pat;
                }//end assign pat value and return

                //set the current node to the next node to keep searching
                node = node.Next;
            }//end while loop searching the LinkedList @ list[index]

            return pat;
        }//end FindPatientByName

        public int FindLongestLength()
        {
            int max = 0;
            int listLength = 0;
            //cycle the list at each point in the array
            for(int arrayLength = 0; arrayLength < list.Length; arrayLength++)
            {
                //reset the listLength
                listLength = 0;
                
                //make sure the array cell isn't null
                if(list[arrayLength] != null)
                {
                    //declare the first node of the list in the cell
                    LinkedListNode<Patient> currentNode = list[arrayLength].First;

                    //cycle each list in each cell when there is a node in the cell
                    while (currentNode != null)
                    {
                        //increment the list length and move to the next node
                        listLength++;
                        currentNode = currentNode.Next;
                    }//end while the array cell has a first node

                    //selection structure checking if the list length is larger than the max.
                    if (listLength > max)
                    {
                        max = listLength;
                    }//end list length comparison to max
                }//end skip because array cell is null              
            }//end cycling the array cells
            return max;
        }//end FindLongestLength

        //CalculateAvgLength calculates the average length of each list
        public double CalculateAvgLength()
        {
            //declare local variable for the total number of items in the array
            double totalPats = 0.0;
            //cycle the array using a for loop
            for(int arrayLength = 0; arrayLength < list.Length; arrayLength++)
            {
                //check to make sure the array isnt null
                if(list[arrayLength] != null)
                {
                    //declare the first node of the list in the cell
                    LinkedListNode<Patient> currentNode = list[arrayLength].First;
                    //cycle each list in each cell when there is a node in the cell
                    while (currentNode != null)
                    {
                        //increment the total patients
                        totalPats++;
                        //move to the next node in the list
                        currentNode = currentNode.Next;
                    }//end cycle counting each patient in a list
                }//end skip segment because the array cell was null
            }//end for loop cycling the array

            //to get the average, divide the total patients by the CalculateCellsUsed number
            return totalPats / CalculateCellsUsed();
        }//end CalculateAvgLength

        public Patient RemovePatientByName(string first, string last)
        {
            Patient pat = null;
            // TODO for 'A' grade
            // remove the patient
            // return the data

            //create a "dummy" patient obj using the first and last name (cost isn't needed, so it is zero)
            Patient dummyPat = new Patient(first, last, 0);
           
            //get the index of the dummyPat so we can get the correct list to search
            int index = dummyPat.Id % list.Length;

            //set the LinkedListNode to be the first node in the LinkedList @ the index value
            LinkedListNode<Patient> node = list[index].First;

            //search as long as the node is not null
            while (node != null)
            {
                //selection structure checking if the node has the value of the dummy patient
                if(dummyPat.CompareTo(node.Value) == 0)
                {
                    //assign pat to the node value
                    pat = node.Value;

                    //cut out the node
                    list[index].Remove(node);

                    //return the pat
                    return pat;
                }//end the node has been found and needs to be skipped
                //set the current node to the next node to keep searching
                node = node.Next;
            }//end while loop searching the LinkedList @ list[index]

            //return the null pat 
            return pat;
        }//end RemovePatientByName

        //CalculatePercentArrayUsed tests what percentage of the array list slots are used and returns it as a percentage
        public double CalculatePercentArrayUsed()
        {
            //divide the CalculateCellsUsed number by the list length multiplied by 100 to get the correct percentage
            return (CalculateCellsUsed() / list.Length) *100;
        }//end CalculatePercentArrayUsed

        //CalculateCellsUsed determines how many cells are being used and returns the number
        private double CalculateCellsUsed()
        {
            //declare local variables
            int cellUsedCount = 0;
            //for loop cycling the array list
            for (int arrayCount = 0; arrayCount < list.Length; arrayCount++)
            {
                //if the array cell has a node, add to the used count
                if (list[arrayCount] != null)
                {
                    cellUsedCount++;
                }//end cell is populated
                
            }//end for loop cycling the array
            return cellUsedCount;
        }//end CalculateCellsUsed
    }//end PaitentData
}
