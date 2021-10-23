using System;
using System.Collections.Generic;

namespace Commercial_Controller
{
    public class Battery
    {
        public int columnID = 1;
        public int elevatorID = 1;
        public int floorRequestButtonID = 1;
        public int callButtonID = 1;
        public int ID;
        public string status;
        
        public List<Column> columnsList = new List<Column>();
        public List<FloorRequestButton> floorRequestButtonList = new List<FloorRequestButton>();
        public Battery(int _ID, int _amountOfColumns, int _amountOfFloors, int _amountOfBasements, int _amountOfElevatorPerColumn)
        {
            ID = _ID;
            status = "online";
            int amountOfColumns = _amountOfColumns;
            int amountOfFloors = _amountOfFloors;
            int amountOfBasement = _amountOfBasements;
            int amountOfElevatorPerColumn = _amountOfElevatorPerColumn;
            
            
            if(amountOfBasement > 0)
            {
              createBasementFloorRequestButtons(amountOfBasement);
              createBasementColumn(amountOfBasement, amountOfElevatorPerColumn);
                amountOfColumns--;
            }

            createFloorRequestButtons(amountOfFloors);
            createColumns(amountOfColumns, amountOfFloors, amountOfElevatorPerColumn);


            
    }

        public void createBasementColumn(int _amountOfBasement,int amountOfElevatorPerColumn)
        {
            List<int> servedFloors = new List<int>();
            
            int floor = -1;

            for(int i = 0; i < _amountOfBasement; i++)
            {
                servedFloors.Add(floor);
                floor--;
            }

            Column column = new Column(columnID.ToString(), amountOfElevatorPerColumn, servedFloors, true);
            
             
            columnsList.Add(column);
            
            columnID++;
        }



        public void createColumns(int _amountOfColumns, int _amountOfFloors , int _amountOfElevatorPerColumn)
        {
           
            int floor = 1;
            int amountOfFloorsPerColumn = (int)Math.Ceiling(Convert.ToDecimal(_amountOfFloors / _amountOfColumns));
           
            for(int i = 0;i < _amountOfColumns; i++)
            {   
                 List<int> servedFloors = new List<int>();

                for(int j = 0; j < amountOfFloorsPerColumn; j++)
                {
                    if(floor <= _amountOfFloors)
                    {
                        servedFloors.Add(floor);
                        floor++;
                    }
                }
                
                Column column = new Column(columnID.ToString(), _amountOfElevatorPerColumn, servedFloors, false);
               
              
                columnsList.Add(column);
               
                
                columnID++;
                

            }
              
        }



        public void createBasementFloorRequestButtons (int amountOfBasement)
        {   
            int buttonFloor = -1;
            for(int i = 0; i < amountOfBasement; i++)
            {
                FloorRequestButton floorRequest = new FloorRequestButton(floorRequestButtonID, buttonFloor, "down");
                floorRequestButtonList.Add(floorRequest);
                buttonFloor--;
                floorRequestButtonID++;

            }
        }




        public void createFloorRequestButtons(int amountOfFloors)
        {
            int buttonFloor = 1;

            for(int i = 0; i < amountOfFloors; i++)
            {
                FloorRequestButton floorRequest = new FloorRequestButton(i, buttonFloor, "up");
                floorRequestButtonList.Add(floorRequest);
                buttonFloor++;


            }
        }
        public Column findBestColumn(int _requestedFloor)
        {   
            
            Column selectedColumn = null;
            foreach (Column column in columnsList)
            {
                if (column.servedFloorsList.Contains(_requestedFloor))
                {
                    
                    selectedColumn = column;
                }
                
            }
            return selectedColumn;
        }
        //Simulate when a user press a button at the lobby
        public (Column, Elevator) assignElevator(int _requestedFloor, string _direction)
        {
            
            Column column = findBestColumn(_requestedFloor);
            Elevator elevator = column.findElevator(1,_direction);
        
            
            elevator.addNewRequest(1);
            elevator.move();
            

            elevator.addNewRequest(_requestedFloor);
            elevator.move();
            

           return (column, elevator) ;
        }
    }
}

