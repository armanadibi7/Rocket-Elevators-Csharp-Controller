using System;
using System.Collections.Generic;
using System.Collections;
using System.Dynamic;

namespace Commercial_Controller
{
    public class Column
    {
        public string ID;
        public int amountOfElevators;
        public int amountOfFloors;
        public List<int> servedFloorsList = new List<int>();
        public Boolean isBasement;
        public List<Elevator> elevatorsList = new List<Elevator>();
        public List<CallButton> callButtonList = new List<CallButton>();

        public Column(string _ID, int _amountOfElevators, List<int> _servedFloors, bool _isBasement)
        {

            ID = _ID;
            amountOfElevators = _amountOfElevators;
            servedFloorsList = _servedFloors;
            isBasement = _isBasement;

            
            createElevators(servedFloorsList,amountOfElevators);
            createCallButtons(servedFloorsList,isBasement);
        }

        public void createCallButtons(List<int> servedFloorsList, bool _isBasement)
        {
            if (_isBasement)
            {
                int buttonFloor = -1;
                for (int i = 0; i < servedFloorsList.Count; i++)
                {
                    CallButton callbutton = new CallButton(i, buttonFloor, "down");
                    callButtonList.Add(callbutton);
                    buttonFloor--;
                }
            }
            else
            {
                int buttonFloor = 1;

                for (int i = 0; i < servedFloorsList.Count; i++)
                {
                    CallButton callbutton = new CallButton(i, buttonFloor, "up");
                    callButtonList.Add(callbutton);
                    buttonFloor++;
                }
            }


        }

        public void createElevators(List<int> servedFloorsList, int _amountOfElevators)
        {   
            
            for (int i = 0; i < _amountOfElevators; i++)
            {   
                Elevator elevator = new Elevator(i.ToString(), "idle", servedFloorsList.Count, 1);
                

                elevatorsList.Add(elevator);

            }
        }

       
        public Elevator findElevator(int _requestedFloor,string _requestedDirection)
        {
            Elevator bestElevator = null;
            int bestScore = 6;
            int referenceGap = 10000000;
            Tuple<Elevator,int,int> bestElevatorInformation;
            
            if (_requestedFloor == 1)
            {
                foreach(Elevator elevator in elevatorsList)
                {
                    if(elevator.currentFloor == 1 && elevator.status == "stopped")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(1, elevator,bestScore,referenceGap, bestElevator, _requestedFloor);


                    }else if (elevator.currentFloor == 1 && elevator.status == "idle")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(2, elevator,bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else if (elevator.currentFloor < 1 && elevator.direction == "up")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(3, elevator, bestScore,referenceGap, bestElevator,  _requestedFloor);


                    }
                    else if (elevator.currentFloor > 1 && elevator.direction == "down")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(3, elevator, bestScore,referenceGap, bestElevator,  _requestedFloor);


                    }
                    else if ( elevator.status == "idle")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(4, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else 
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(5, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    bestElevator = bestElevatorInformation.Item1;
                    bestScore = bestElevatorInformation.Item2;
                    referenceGap = bestElevatorInformation.Item3;
                }
            }
            else
            {
                foreach (Elevator elevator in elevatorsList)
                {
                    if (elevator.currentFloor == _requestedFloor && elevator.status == "stopped" && elevator.direction == _requestedDirection)
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(1, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else if (elevator.currentFloor < _requestedFloor  && elevator.direction=="up" && _requestedDirection == "up")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(2, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else if (elevator.currentFloor  > _requestedFloor && elevator.direction == "down" && _requestedDirection == "down")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(2, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else if (elevator.status == "idle")
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(4, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    else
                    {
                        bestElevatorInformation = checkIfElevatorIsBetter(5, elevator, bestScore,referenceGap, bestElevator, _requestedFloor);


                    }
                    bestElevator = bestElevatorInformation.Item1;
                    bestScore = bestElevatorInformation.Item2;
                    referenceGap = bestElevatorInformation.Item3;
                }
            }




            return bestElevator;
        }


      Tuple<Elevator,int,int> checkIfElevatorIsBetter(int scoreToCheck,Elevator newElevator,int bestScore, int referenceGap, Elevator bestElevator ,int floor)
        {
            if(scoreToCheck < bestScore)
            {
                
                bestScore = scoreToCheck;
                bestElevator = newElevator;
                referenceGap = Math.Abs(newElevator.currentFloor - floor);

            }else if(bestScore == scoreToCheck)
            {
                int gap = Math.Abs(newElevator.currentFloor - floor);
                if(referenceGap > gap)
                {
                    bestElevator = newElevator;
                    referenceGap = gap;
                }
            }
            Tuple<Elevator,int,int> bestElevatorInformation = new Tuple<Elevator,int,int>(bestElevator,bestScore,referenceGap);
            return bestElevatorInformation;

        }
        
        //Simulate when a user press a button on a floor to go back to the first floor
        public Elevator requestElevator(int userPosition, string direction)
        {
            Elevator elevator = findElevator(userPosition, direction);
            elevator.addNewRequest(userPosition);
            elevator.move();

            elevator.completedRequestsList.Add(userPosition);
            // this.servedFloorsList.Add(userPosition);
            
            elevator.addNewRequest(1);
            elevator.move();
            return elevator;
        }

    }
}