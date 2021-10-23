using System.Threading;
using System.Collections.Generic;
using System;
namespace Commercial_Controller
{
    public class Elevator
    {
        public int currentFloor;
        public string status;
        public string direction;
        public string ID;
        public int amountOfFloors;
        public List<int> floorRequestsList = new List<int>();
        public bool overWeight = false;
        public bool obstruction = false;
        public List<int> completedRequestsList = new List<int>();
        public Elevator(string _elevatorID,string _status,int _amountOfFloors,int _currentFloor)
        {
            ID = _elevatorID;
            status = _status;
            currentFloor = _currentFloor;
            amountOfFloors = _amountOfFloors;
            

        }

        
      
        public void move()
        {   
            
            while(floorRequestsList.Count > 0 )
            {
                int destination = floorRequestsList[0];
                status = "moving";
                if(currentFloor < destination)
                {
                    direction = "up";
                    sortFloorList(0);
                    while(currentFloor < destination)
                    {
                        currentFloor++;
                        
                    }

                }else if(currentFloor > destination)
                {
                    direction = "down";
                    sortFloorList(1);
                    while (currentFloor > destination)
                    {
                        currentFloor--;

                    }
                }
                // Console.Write( " "+destination );
                completedRequestsList.Add(currentFloor);
               
                status = "stopped";
                operateDoors();
                floorRequestsList.RemoveAt(0);

            }
            
            // completedRequestsList.ForEach(i => Console.Write( " & "+ i));
            // Console.WriteLine("");
            status = "idle";
            
        }
        public void sortFloorList(int sortFunction)
        {
            if (sortFunction == 0)
            {
                floorRequestsList.Sort();
                
                floorRequestsList.ForEach(i => Console.Write( " & "+ i));
            }
            else if (sortFunction == 1)
            {

                floorRequestsList.Sort();
                floorRequestsList.Reverse();
                floorRequestsList.ForEach(i => Console.Write( " & "+ i));
            }
        }
        public void operateDoors()
        {
            Door door = new Door(int.Parse(ID), "closed");
            door.status = "opened";
           // System.Threading.Thread.Sleep(5000);
            if (overWeight == false)
            {
                if(obstruction == false)
                {
                    door.status = "closed";
                }
                else
                {
                    operateDoors();
                }
            }
            else
            {
                while (overWeight)
                {
                    //alarm
                }
                operateDoors();
            }
        }

        public void addNewRequest(int _requestedFloor)
        { 
            if (floorRequestsList.Contains(_requestedFloor) == false)
            {
            
                floorRequestsList.Insert(0,_requestedFloor);
               
            }
            if(currentFloor < _requestedFloor)
            {
                direction = "up";

            }else if(currentFloor > _requestedFloor)
            {
                direction = "down";
            }

        }

    }
}